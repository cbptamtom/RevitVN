using Nice3point.Revit.Toolkit.External;
using RevitAddinManager.Commands;

namespace RevitAddinManager;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        Host.Start();
        CreateRibbon();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Commands", "RevitAddinManager");

        panel.AddPushButton<StartupCommand>("Execute")
            .SetImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon32.png");
    }
}