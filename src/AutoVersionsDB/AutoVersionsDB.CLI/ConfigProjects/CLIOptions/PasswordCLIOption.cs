using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class PasswordCLIOption : Option<string>
    {
        public PasswordCLIOption()
            : base(new string[] { "--password", "-pass" }, "DB Password")
        {
        }
    }
}
