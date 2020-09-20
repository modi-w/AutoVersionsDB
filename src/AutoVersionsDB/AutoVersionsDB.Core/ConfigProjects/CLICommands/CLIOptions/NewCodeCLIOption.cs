using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class NewCodeCLIOption : Option<string>
    {
        public NewCodeCLIOption()
            : base(new string[] { "--new-code", "-ncode" }, "The new project code")
        {
            IsRequired = true;
        }
    }
}
