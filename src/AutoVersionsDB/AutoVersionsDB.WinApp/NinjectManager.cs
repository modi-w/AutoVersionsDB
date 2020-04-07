using AutoVersionsDB.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.WinApp
{
    public class NinjectManager
    { /* https://github.com/ninject/Ninject.Web/blob/master/src/Ninject.Web/WebServiceBase.cs
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

            registerServices(NinjectKernelContainer);
            NinjectUtils.SetKernelInstance(NinjectKernelContainer);
        }

        private static void registerServices(IKernel kernel)
        {
        }


        public static void CreateKernelForTests()
        {
            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            registerServicesForTests(NinjectKernelContainer);
        }

        private static void registerServicesForTests(IKernel kernel)
        {
            //TODO: register Mock services

        }
    }
}
