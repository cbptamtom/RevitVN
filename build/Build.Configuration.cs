using Nuke.Common.ProjectModel;

sealed partial class Build
{
    const string Version = "1.0.3";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    protected override void OnBuildInitialized()
    {
        Configurations =
        [
            "Release*",
            "Installer*"
        ];

        Bundles =
        [
            Solution.RevitAddinManager
        ];

        InstallersMap = new Dictionary<Project, Project>
        {
            { Solution.Installer, Solution.RevitAddinManager }
        };
    }
}