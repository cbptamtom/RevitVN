using Module2.Views;

namespace Module2.Commands;

/// <summary>
///     Command entry point invoked from the Revit AddIn Application
/// </summary>
public class ShowWindowComponent(Module2View view)
{
    public void Execute()
    {
        view.ShowDialog();
    }
}