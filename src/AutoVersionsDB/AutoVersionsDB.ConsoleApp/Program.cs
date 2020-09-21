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
            NinjectManager.CreateKernel();

            Console.Title = "AutoVersionsDB";


            //return AutoVersionsDbAPI.CLIRun(args);
            //  return AutoVersionsDbAPI.CLIRun("-h");
            //return AutoVersionsDbAPI.CLIRun("config -code=testp1 -desc=\"Test project 2\" --db-type=SqlServer -connstr=aaaa -connstrm=bbb -buf=ccc -dsf=ddd -def=eee -darf=fff");
            //  return AutoVersionsDbAPI.CLIRun("config environment -code=testp1 -dev=true");
            // return AutoVersionsDbAPI.CLIRun("sync -code=testp1");
       //     return AutoVersionsDbAPI.CLIRun("config -code=testp -desc=\"Test project 2\"");

            //return AutoVersionsDbAPI.CLIRun("list");
            return AutoVersionsDbAPI.CLIRun("info -code=rvp");
            
        }





    }
}
