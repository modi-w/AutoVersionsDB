using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DevEnvironmentCLIOption : Option<bool>
    {
        public DevEnvironmentCLIOption()
            : base(new string[] { "--dev-environment", "-dev" }, "Is the project run on dev environment (use scripts files) or on a delivery environment (use artifact file)")
        {
            IsRequired = true;
        }
    }
}
