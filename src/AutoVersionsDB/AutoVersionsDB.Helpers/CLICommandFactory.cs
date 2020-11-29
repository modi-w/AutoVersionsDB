using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Helpers
{
    public abstract class CLICommandFactory
    {
        
        public abstract Command Create();
    }
}
