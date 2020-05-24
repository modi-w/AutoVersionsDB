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
            RegisterServices(kernel);

            KernelInstance = kernel;
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DBCommands_FactoryProvider>().To<DBCommands_FactoryProvider>().InSingletonScope();


            kernel.Bind<NotifictionStatesHistoryManager>().To<NotifictionStatesHistoryManager>().InSingletonScope();
            kernel.Bind<NotificationExecutersFactoryManager>().To<NotificationExecutersFactoryManager>().InSingletonScope();
            
        }
    }
}
