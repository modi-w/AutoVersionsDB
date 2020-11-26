using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class ServerCLIOption : Option<string>
    {
        public ServerCLIOption()
            : base(new string[] { "--server", "-ser" }, "DB Server Instance Name")
        {
        }
    }
}
