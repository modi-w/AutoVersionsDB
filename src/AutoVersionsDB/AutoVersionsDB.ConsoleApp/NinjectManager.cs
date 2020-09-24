using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.ConsoleApp
{
    public static class NinjectManager
    { 
        /* https://github.com/ninject/Ninject.Web/blob/master/src/Ninject.Web/WebServiceBase.cs
         * 
         * https://stackoverflow.com/questions/14127763/dependency-injection-in-winforms-using-ninject-and-entity-framework
         * https://gist.github.com/dkellycollins/9c3fecaedd830094d7f2
         */


        // TODO: When using winapp -> call to CreateKernel(), when using unit tests -> call to CreateKenrelForTests

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
            kernel.Bind<IConsoleHandler>().To<ConsoleHandler>().InSingletonScope();

        }


        public static void CreateKernelForTests()
        {
            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            RegisterServicesForTests(NinjectKernelContainer);
        }

        private static void RegisterServicesForTests(IKernel kernel)
        {
           // kernel.ThrowIfNull(nameof(kernel));

            //TODO: register Mock services

        }
    }
}
