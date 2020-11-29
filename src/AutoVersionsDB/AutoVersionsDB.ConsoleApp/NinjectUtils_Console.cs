using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using Ninject;
using System.Reflection;

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
