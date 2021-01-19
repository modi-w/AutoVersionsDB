using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DBNameCLIOption : Option<string>
    {
        public DBNameCLIOption()
            : base(new string[] { "--db-name", "-db" }, CLITextResources.DBNameCLIOptionDescription)
        {
        }
    }
}
