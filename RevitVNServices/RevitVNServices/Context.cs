using Autodesk.Revit.UI;
using JetBrains.Annotations;

namespace RevitVNServices;


[PublicAPI]
public class Context
{
    public static UIApplication UiApplication { get; }
}