using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.Common.CLI
{
    public class IdCLIOption : Option<string>
    {
        public IdCLIOption()
            : base(new string[] { "--id", "-id" }, "The project id")
        {
            IsRequired = true;
        }
    }
}
