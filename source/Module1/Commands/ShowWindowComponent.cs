using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Module1.Utils;
using Module1.ViewModels;
using Module1.Views;
using Context = RevitVNServices.Context;

namespace Module1.Commands;

/// <summary>
///     Command entry point invoked from the Revit AddIn Application
/// </summary>
public class ShowWindowComponent(IServiceProvider serviceProvider)
{
    public void Execute()
    {
        if(WindowController.Focus<Module1View>()) return;

        var view = serviceProvider.GetService<Module1View>();

        WindowController.Show(view, Context.UiApplication.MainWindowHandle);
    }
}   