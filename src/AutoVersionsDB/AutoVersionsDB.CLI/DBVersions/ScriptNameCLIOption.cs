using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class ScriptNameCLIOption : Option<string>
    {
        public ScriptNameCLIOption()
            : base(new string[] { "--script-name", "-sn" }, "New script name")
        {
            IsRequired = true;
        }
    }
}
