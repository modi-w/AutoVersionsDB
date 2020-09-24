using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class ConnectionStringCLIOption : Option<string>
    {
        public ConnectionStringCLIOption()
            : base(new string[] { "--connection-string", "-connstr" }, "Connection String for the Database")
        {
        }
    }
}
