using System.Windows;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.Extensions.Logging;
using Module1.Utils;
using Module1.Views;
using RevitVNServices.External.Handlers;
using RevitVNServices.Options;

namespace Module1.ViewModels;

public sealed partial class Module1ViewModel(ILogger<Module1ViewModel> logger) : ObservableObject
{
    private readonly ActionEventHandler _externalHandler = new();
    private readonly AsyncEventHandler _asyncExternalHandler = new();
    private readonly AsyncEventHandler<ElementId> _asyncIdExternalHandler = new();

    [ObservableProperty] private string _element;
    [ObservableProperty] private string _category;
    [ObservableProperty] private string _status;

    
    [RelayCommand]
    private void ShowSummary()
    {
        _externalHandler.Raise(application =>
        {
            var selectionConfiguration = new SelectionConfiguration();
            var reference = application.ActiveUIDocument.Selection.PickObject(ObjectType.Element, selectionConfiguration.Filter);
            var element = reference.ElementId.ToElement(application.ActiveUIDocument.Document)!;

            Element = element.Name;
            Category = element.Category.Name;
            
            logger.LogInformation("Selection successful");
        });
    }
    
    [RelayCommand]
    private async Task SelectDelayedElementAsync()
    {
        // SelectDelayedElementCommand
        Status = "Wait 2 second...";
        await Task.Delay(TimeSpan.FromSeconds(2));
        await _asyncExternalHandler.RaiseAsync(app =>
        {
            var selectionConfiguration = new SelectionConfiguration();
            WindowController.Hide<Module1View>();

            var reference =
                app.ActiveUIDocument.Selection.PickObject(ObjectType.Element, selectionConfiguration.Filter);
            var element = reference.ElementId.ToElement(app.ActiveUIDocument.Document)!;
            WindowController.Show<Module1View>();

            Element = element.Name;
            Category = element.Category.Name;
            logger.LogInformation("Selection successful");
        });
        Status = string.Empty;
    }
    
    
    [RelayCommand]
    private async Task DeleteElementAsync()
    {
        // DeleteElementCommand
        var deletedId = await _asyncIdExternalHandler.RaiseAsync(app =>
        {
            var document = app.ActiveUIDocument.Document;
            var selectionConfiguration = new SelectionConfiguration();
            
            
            var reference = app.ActiveUIDocument.Selection.PickObject(ObjectType.Element,selectionConfiguration.Filter);
            // var element = reference.ElementId.ToElement(document)!;
            
            var  trans = new Transaction(document);
            trans.Start("Delete element");
            document.Delete(reference.ElementId);
            trans.Commit();
            return reference.ElementId;

        });
        
        TaskDialog.Show("Deleted element", $"ID: {deletedId}");
        logger.LogInformation("Deletion successful");
    }
    
    
    
}