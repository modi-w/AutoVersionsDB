using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.CLICommands
{
    public class TargetCLIOption : Option<string>
    {
        public TargetCLIOption()
            : base(new string[] { "--target", "-t" }, "The target file script name that set the db in the desired state")
        {
        }
    }
}
