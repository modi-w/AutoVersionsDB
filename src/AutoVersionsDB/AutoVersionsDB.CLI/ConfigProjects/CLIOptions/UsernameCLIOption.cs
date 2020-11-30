using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class UsernameCLIOption : Option<string>
    {
        public UsernameCLIOption()
            : base(new string[] { "--username", "-un" }, "DB Username")
        {
        }
    }
}
