using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

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
