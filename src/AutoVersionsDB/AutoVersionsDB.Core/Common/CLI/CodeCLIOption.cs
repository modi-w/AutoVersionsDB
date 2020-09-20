using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.Common.CLI
{
    public class CodeCLIOption : Option<string>
    {
        public CodeCLIOption()
            : base(new string[] { "--code", "-code" }, "The project code")
        {
            IsRequired = true;
        }
    }
}
