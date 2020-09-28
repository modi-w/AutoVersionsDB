using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading;

namespace AutoVersionsDB.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            NinjectUtils_Console.CreateKernel();

            Console.Title = "AutoVersionsDB";


            //return AutoVersionsDbAPI.CLIRun(args);
            //  return AutoVersionsDbAPI.CLIRun("-h");
            //return AutoVersionsDbAPI.CLIRun("config -id=testp1 -desc=\"Test project 2\" --db-type=SqlServer -connstr=aaaa -connstrm=bbb -buf=ccc -dsf=ddd -def=eee -darf=fff");
            //  return AutoVersionsDbAPI.CLIRun("config environment -id=testp1 -dev=true");
            //return AutoVersionsDbAPI.CLIRun("sync -id=testp");
            //return AutoVersionsDbAPI.CLIRun("recreate -id=testp");
            return AutoVersionsDbAPI.CLIRun("deploy -id=testp");
            //     return AutoVersionsDbAPI.CLIRun("config -id=testp -desc=\"Test project 1\"");

            //return AutoVersionsDbAPI.CLIRun("list");
            //    return AutoVersionsDbAPI.CLIRun("info -id=rvp");

            //   return AutoVersionsDbAPI.CLIRun("new incremental -id=testp --sn bbbb");

        }





    }
}
