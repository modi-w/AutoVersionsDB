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


            //     return AutoVersionsDBAPI.CLIRun(args);

            return AutoVersionsDBAPI.CLIRun("recreate -id=Bandana");


            //  return AutoVersionsDBAPI.CLIRun("-h");
            // return AutoVersionsDBAPI.CLIRun("dbtypes");
            //return AutoVersionsDBAPI.CLIRun("config -id=testp1 -desc=\"Test project 2\" --db-type=SqlServer -connstr=aaaa -connstrm=bbb -buf=ccc -dsf=ddd -def=eee -darf=fff");
            //  return AutoVersionsDBAPI.CLIRun("config environment -id=testp1 -dev=true");
            //return AutoVersionsDBAPI.CLIRun("sync -id=testp");
            //return AutoVersionsDBAPI.CLIRun("recreate -id=Bandana");
            //    return AutoVersionsDBAPI.CLIRun("validate -id=testp");
            //   return AutoVersionsDBAPI.CLIRun("files -id=testp");
            //     return AutoVersionsDBAPI.CLIRun("config -id=testp -desc=\"Test project 1\"");

            //return AutoVersionsDBAPI.CLIRun("list");
            //    return AutoVersionsDBAPI.CLIRun("info -id=rvp");

            //   return AutoVersionsDBAPI.CLIRun("new incremental -id=testp --sn bbbb");

        }





    }
}
