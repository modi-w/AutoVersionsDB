using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DeliveryArtifactFolderPathCLIOption : Option<string>
    {
        public DeliveryArtifactFolderPathCLIOption()
            : base(new string[] { "--delivery-artifact-folder-path", "-darf" }, CLITextResources.DeliveryArtifactFolderPathCLIOptionDescription)
        {
        }
    }
}
