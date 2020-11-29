using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class PasswordCLIOption : Option<string>
    {
        public PasswordCLIOption()
            : base(new string[] { "--password", "-pass" }, "DB Password")
        {
        }
    }
}
