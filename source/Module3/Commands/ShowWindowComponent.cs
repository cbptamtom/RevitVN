using Module3.Views;

namespace Module3.Commands;

/// <summary>
///     Command entry point invoked from the Revit AddIn Application
/// </summary>
public class ShowWindowComponent(Module3View view)
{
    public void Execute()
    {
        view.ShowDialog();
    }
}