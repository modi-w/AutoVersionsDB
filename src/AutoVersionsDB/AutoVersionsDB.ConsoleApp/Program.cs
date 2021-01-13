using AutoVersionsDB.CLI;
using System;

namespace AutoVersionsDB.ConsoleApp
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            DIConfig.CreateKernel();

            Console.Title = "AutoVersionsDB";


            return CLIRunner.CLIRun(args);

            //    return CLIRunner.CLIRun("sync -id=testP2");

            //
            //return CLIRunner.CLIRun("recreate -id=Bandana");


            //  return CLIRunner.CLIRun("-h");
            // return CLIRunner.CLIRun("dbtypes");
            //return CLIRunner.CLIRun("config -id=testp1 -desc=\"Test project 2\" --db-type=SqlServer -connstr=aaaa -connstrm=bbb -buf=ccc -dsf=ddd -def=eee -darf=fff");
            //  return CLIRunner.CLIRun("config environment -id=testp1 -dev=true");
            //return CLIRunner.CLIRun("recreate -id=Bandana");
            //    return CLIRunner.CLIRun("validate -id=testp");
            //   return CLIRunner.CLIRun("files -id=testp");
            //     return CLIRunner.CLIRun("config -id=testp -desc=\"Test project 1\"");

            //return CLIRunner.CLIRun("list");
            //    return CLIRunner.CLIRun("info -id=rvp");

            //   return CLIRunner.CLIRun("new incremental -id=testp --sn bbbb");

        }





    }
}
