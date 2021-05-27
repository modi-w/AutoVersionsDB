using AutoVersionsDB.DB;
using Ninject;

namespace AutoVersionsDB.Core
{
    public static class DIConfig
    {
        public static IKernel Kernel { get; private set; }

        public static void SetKernelInstance(IKernel kernel)
        {
            Kernel = kernel;

            RegisterServices();

            ComposeObjects();
        }

        public static void RegisterServices()
        {
            Kernel.Bind<DBCommandsFactory>().To<DBCommandsFactory>().InSingletonScope();
        }

        private static void ComposeObjects()
        {
        }
    }
}
