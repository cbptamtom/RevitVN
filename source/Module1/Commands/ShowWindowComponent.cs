using System.Windows;
using Module1.Views;

namespace Module1.Commands;

/// <summary>
///     Command entry point invoked from the Revit AddIn Application
/// </summary>
public class ShowWindowComponent(Module1View view)
{
    public void Execute()
    {
        MessageBox.Show("Test");
        view.ShowDialog();
    }
}