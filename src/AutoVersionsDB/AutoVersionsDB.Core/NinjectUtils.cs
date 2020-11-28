using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;

namespace AutoVersionsDB.Core
{
    public static class NinjectUtils
    {
        public static IKernel KernelInstance { get; private set; }

        public static void SetKernelInstance(IKernel kernel)
        {
            KernelInstance = kernel;

            RegisterServices();
            
            ComposeObjects();
        }

        public static void RegisterServices()
        {
            KernelInstance.Bind<DBCommandsFactory>().To<DBCommandsFactory>().InSingletonScope();
        }

        private static void ComposeObjects()
        {
        }
    }
}
