using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DescriptionCLIOption : Option<string>
    {
        public DescriptionCLIOption()
            : base(new string[] { "--description", "-desc" }, "Description for the project")
        {
        }
    }
}
