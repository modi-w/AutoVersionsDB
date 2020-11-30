using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class NewIdCLIOption : Option<string>
    {
        public NewIdCLIOption()
            : base(new string[] { "--new-id", "-nid" }, "The new project id")
        {
            IsRequired = true;
        }
    }
}
