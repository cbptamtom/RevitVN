using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;

namespace RevitVNServices;


[PublicAPI]
public static class Context
{
    public static UIApplication UiApplication { get; }

    public static Application Application => UiApplication.Application;

    [CanBeNull]
    public static UIDocument UiDocument => UiApplication.ActiveUIDocument;

    public static Document Document => UiApplication.ActiveUIDocument.Document;

    public static View ActiveView
    {
        get => UiApplication.ActiveUIDocument.ActiveView;
        set => UiApplication.ActiveUIDocument.ActiveView = value;
    }

    public static View ActiveGraphicalView => UiApplication.ActiveUIDocument.ActiveGraphicalView;

    static Context()
    {
        const BindingFlags staticFlags = BindingFlags.NonPublic | BindingFlags.Static;
        var dbAssembly = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(ass => ass.GetName().Name == "RevitDBAPI");
        ThrowIfNotSupported(dbAssembly);

        var getApplicationMethod = dbAssembly?.ManifestModule.GetMethods(staticFlags)
            .FirstOrDefault(info => info.Name == "RevitApplication.getApplication_");
        ThrowIfNotSupported(getApplicationMethod);

        var proxyType = dbAssembly?.DefinedTypes.FirstOrDefault(info => info.FullName == "Autodesk.Revit.Proxy.ApplicationServices.ApplicationProxy");
        ThrowIfNotSupported(proxyType);

        const BindingFlags internalFlags = BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance;
        var proxyConstructor = proxyType?.GetConstructor(internalFlags, null, [getApplicationMethod?.ReturnType], null);
        ThrowIfNotSupported(proxyConstructor);

        var proxy = proxyConstructor?.Invoke([getApplicationMethod?.Invoke(null, null)]);
        ThrowIfNotSupported(proxy);

        var applicationType = typeof(Application);
        var applicationConstructor = applicationType.GetConstructor(internalFlags, null, [proxyType], null);
        ThrowIfNotSupported(applicationConstructor);

        var application = (Application) applicationConstructor?.Invoke([proxy]);
        ThrowIfNotSupported(proxy);

        UiApplication = new UIApplication(application);
    }

    private static void ThrowIfNotSupported([CanBeNull]object argument)
    {
        if (argument == null)
        {
            throw new NotSupportedException(
                "The operation is not supported by current Revit API version. Failed to retrieve application context.");
        }
    }
} 