using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DevEnvironmentCLIOption : Option<bool>
    {
        public DevEnvironmentCLIOption()
            : base(new string[] { "--dev-environment", "-dev" }, CLITextResources.DevEnvironmentCLIOptionDescription)
        {
            IsRequired = true;
        }
    }
}
