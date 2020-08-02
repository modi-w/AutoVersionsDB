using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
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
            RegisterServices(kernel) ;

            KernelInstance = kernel;
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel.ThrowIfNull(nameof(kernel));

            kernel.Bind<DBCommandsFactoryProvider>().To<DBCommandsFactoryProvider>().InSingletonScope();


            kernel.Bind<ScriptFilesComparersManager>().To<ScriptFilesComparersManager>().InSingletonScope();
            
        }
    }
}
