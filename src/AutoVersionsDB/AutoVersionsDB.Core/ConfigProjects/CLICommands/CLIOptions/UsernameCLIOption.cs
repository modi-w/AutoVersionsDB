using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class UsernameCLIOption : Option<string>
    {
        public UsernameCLIOption()
            : base(new string[] { "--username", "-un" }, "DB Username")
        {
        }
    }
}
