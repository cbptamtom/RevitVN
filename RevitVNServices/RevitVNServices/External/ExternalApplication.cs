using System.ComponentModel;
using Autodesk.Revit.UI;
using RevitVNServices.Helpers;

namespace RevitVNServices.External;

public abstract class ExternalApplication:IExternalApplication
{
    private Result Result { get; set; } = Result.Succeeded;

    public UIControlledApplication Application { get; private set; }
    public UIApplication UiApplication => Context.UiApplication;
    
    
    public Result OnStartup(UIControlledApplication application)
    {
        Application = application;
        try
        {
            ResolveHelper.BeginAssemblyResolve(GetType());
            OnStartup();
        }
        finally
        {
            ResolveHelper.EndAssemblyResolve();
        }

        return Result;
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Result OnShutdown(UIControlledApplication application)
    {
        try
        {
            ResolveHelper.BeginAssemblyResolve(GetType());
            OnShutdown();
        }
        finally
        {
            ResolveHelper.EndAssemblyResolve();
        }

        return Result.Succeeded;
    }

    protected abstract void OnStartup();

    protected virtual void OnShutdown()
    {
        
    }
}