using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class BackupFolderPathCLIOption : Option<string>
    {
        public BackupFolderPathCLIOption()
            : base(new string[] { "--backup-folder-path", "-buf" }, CLITextResources.BackupFolderPathCLIOptionDescription)
        {
        }
    }
}
