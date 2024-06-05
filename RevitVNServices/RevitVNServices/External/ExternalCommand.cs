using Autodesk.Revit.UI;

namespace RevitVNServices.External;

public class ExternalCommand :IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        throw new NotImplementedException();
    }
}