using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.Common.CLI
{
    public class CodeCLIOption : Option<string>
    {
        public CodeCLIOption()
            : base(new string[] { "--code", "-c" }, "The project code whitch you want to sync")
        {
            IsRequired = true;
        }
    }
}
