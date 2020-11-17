using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using Ninject;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.IO;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.ConsoleApp
{
    public static class NinjectUtils_Console
    { 
        public static IKernel NinjectKernelContainer { get; private set; }


        public static void CreateKernel()
        {
            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            RegisterServices();

            NinjectUtils.SetKernelInstance(NinjectKernelContainer);

            ComposeObjects();
        }

        private static void RegisterServices()
        {
            NinjectKernelContainer.Bind<IConsoleExtended>().To<ConsoleExtended>();
            NinjectKernelContainer.Bind<IConsoleProcessMessages>().To<ConsoleProcessMessages>();
        }

        private static void ComposeObjects()
        {
        }


    }
}
