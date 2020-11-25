using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class DBNameCLIOption : Option<string>
    {
        public DBNameCLIOption()
            : base(new string[] { "--db-name", "-db" }, "Data Base Name")
        {
        }
    }
}
