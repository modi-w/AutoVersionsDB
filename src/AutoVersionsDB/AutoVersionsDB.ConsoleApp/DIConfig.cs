using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using Ninject;
using System.Reflection;

namespace AutoVersionsDB.ConsoleApp
{
    public static class DIConfig
    {
        public static IKernel Kernel { get; private set; }


        public static void CreateKernel()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices();

            Core.DIConfig.SetKernelInstance(Kernel);

            ComposeObjects();
        }

        private static void RegisterServices()
        {
            Kernel.Bind<IConsoleExtended>().To<ConsoleExtended>();
            Kernel.Bind<IConsoleProcessMessages>().To<ConsoleProcessMessages>();
        }

        private static void ComposeObjects()
        {
        }


    }
}
