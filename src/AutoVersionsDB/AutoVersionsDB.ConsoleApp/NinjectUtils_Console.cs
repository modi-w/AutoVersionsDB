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

            RegisterServices(NinjectKernelContainer);
            NinjectUtils.SetKernelInstance(NinjectKernelContainer);
        }

        private static void RegisterServices(IKernel kernel)
        {
            //  kernel.ThrowIfNull(nameof(kernel));
            kernel.Bind<IConsoleProcessMessages>().To<ConsoleProcessMessages>().InSingletonScope();
            kernel.Bind<IConsole>().To<SystemConsole>();
        }


        

    }
}
