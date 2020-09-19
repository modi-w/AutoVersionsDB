using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DevEnironmentCLIOption : Option<bool>
    {
        public DevEnironmentCLIOption()
            : base(new string[] { "--dev-environment", "-dev" }, "Is the project run on dev environment (allow to use dummy data scripts files)")
        {
        }
    }
}
