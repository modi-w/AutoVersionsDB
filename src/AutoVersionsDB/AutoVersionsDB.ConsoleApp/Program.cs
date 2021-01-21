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

               //return CLIRunner.CLIRun("list");



        }





    }
}
