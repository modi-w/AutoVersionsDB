using AutoVersionsDB.Core;
using Ninject;

namespace AutoVersionsDB.CLI
{
    public static class CLIRunner
    {
        private static AutoVersionsDBCLI GetNewInstanceForAutoVersionsDBCLI()
        {
            return DIConfig.Kernel.Get<AutoVersionsDBCLI>();
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
