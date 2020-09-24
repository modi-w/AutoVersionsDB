using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.CLICommands
{
    public class TargetCLIOption : Option<string>
    {
        public TargetCLIOption()
            : base(new string[] { "--target", "-t" }, "The project code whitch you want to sync")
        {
        }
    }
}
