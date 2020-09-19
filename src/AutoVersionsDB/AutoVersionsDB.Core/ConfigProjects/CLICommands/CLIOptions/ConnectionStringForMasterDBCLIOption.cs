using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class ConnectionStringForMasterDBCLIOption : Option<string>
    {
        public ConnectionStringForMasterDBCLIOption()
            : base(new string[] { "--connection-string-master", "-connstrm" }, "Connection String for the master Database (with dbowner privileges)")
        {
        }
    }
}
