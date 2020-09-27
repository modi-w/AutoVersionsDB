using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DBNameCLIOption : Option<string>
    {
        public DBNameCLIOption()
            : base(new string[] { "--db-name", "-db" }, "Data Base Name")
        {
        }
    }
}
