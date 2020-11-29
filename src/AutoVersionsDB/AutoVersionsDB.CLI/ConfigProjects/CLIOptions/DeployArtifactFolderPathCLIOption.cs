using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DeployArtifactFolderPathCLIOption : Option<string>
    {
        public DeployArtifactFolderPathCLIOption()
            : base(new string[] { "--deploy-artifact-folder-path", "-def" }, "Deploy folder path for exporting the artifact file")
        {
        }
    }
}
