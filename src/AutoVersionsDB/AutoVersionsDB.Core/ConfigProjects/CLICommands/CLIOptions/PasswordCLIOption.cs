using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class PasswordCLIOption : Option<string>
    {
        public PasswordCLIOption()
            : base(new string[] { "--password", "-pass" }, "DB Password")
        {
        }
    }
}
