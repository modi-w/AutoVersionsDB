using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class ServerCLIOption : Option<string>
    {
        public ServerCLIOption()
            : base(new string[] { "--server", "-ser" }, CLITextResources.ServerCLIOptionDescription)
        {
        }
    }
}
