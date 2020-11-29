using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DBTypeCLIOption : Option<string>
    {
        public DBTypeCLIOption()
            : base(new string[] { "--db-type", "-dbt" }, "Database Type (SqlServer)")
        {
        }
    }
}
