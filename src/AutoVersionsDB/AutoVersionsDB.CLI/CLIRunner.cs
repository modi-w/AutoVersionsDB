using AutoVersionsDB.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.CLI
{
    public static class CLIRunner
    {
        private static AutoVersionsDBCLI GetNewInstanceForAutoVersionsDBCLI()
        {
            return NinjectUtils.KernelInstance.Get<AutoVersionsDBCLI>();
        }


        public static int CLIRun(string[] args)
        {
            return GetNewInstanceForAutoVersionsDBCLI().Run(args);
        }
        public static int CLIRun(string args)
        {
            return GetNewInstanceForAutoVersionsDBCLI().Run(args);
        }
    }
}
