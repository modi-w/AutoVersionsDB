using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class NewIdCLIOption : Option<string>
    {
        public NewIdCLIOption()
            : base(new string[] { "--new-id", "-nid" }, "The new project id")
        {
            IsRequired = true;
        }
    }
}
