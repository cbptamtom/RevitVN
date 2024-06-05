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
    public override void OnShutdown()
    {
        Host.Stop();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Commands", "RevitVN");

        panel.AddPushButton<RunModule1Command>("Execute1")
            .SetImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon32.png");
        
        
        panel.AddPushButton<RunModule2Command>("Execute2")
            .SetImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon32.png");
        
        panel.AddPushButton<RunModule3Command>("Execute3")
            .SetImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/RevitAddinManager;component/Resources/Icons/RibbonIcon32.png");
    }
    
}