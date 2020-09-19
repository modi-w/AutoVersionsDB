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


            return AutoVersionsDbAPI.CLIRun(args);
        }





       


     







    }
}
