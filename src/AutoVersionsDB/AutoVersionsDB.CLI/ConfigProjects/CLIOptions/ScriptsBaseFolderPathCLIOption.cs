using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class ScriptsBaseFolderPathCLIOption : Option<string>
    {
        public ScriptsBaseFolderPathCLIOption()
            : base(new string[] { "--dev-scripts-base-folder-path", "-dsf" }, CLITextResources.ScriptsBaseFolderPathCLIOptionDescription)
        {
        }
    }
}
