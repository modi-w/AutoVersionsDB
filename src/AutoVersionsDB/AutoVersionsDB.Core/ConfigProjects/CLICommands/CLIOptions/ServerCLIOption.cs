using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class ServerCLIOption : Option<string>
    {
        public ServerCLIOption()
            : base(new string[] { "--server", "-ser" }, "DB Server Instance Name")
        {
        }
    }
}
