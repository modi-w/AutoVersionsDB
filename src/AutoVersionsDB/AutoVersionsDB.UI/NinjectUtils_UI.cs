using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.DBVersions;

namespace AutoVersionsDB.UI
{
    public static class NinjectUtils_UI
    {
        public static IKernel KernelInstance { get; private set; }

        public static void SetKernelInstance(IKernel kernel)
        {
            RegisterServices(kernel);

            KernelInstance = kernel;
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel.ThrowIfNull(nameof(kernel));

            kernel.Bind<ViewRouter>().To<ViewRouter>().InSingletonScope();
            kernel.Bind<NotificationsViewModel>().To<NotificationsViewModel>().InSingletonScope();
            kernel.Bind<TextInputViewModel>().To<TextInputViewModel>().InSingletonScope();
            

            kernel.Bind<EditProjectControls>().To<EditProjectControls>().InSingletonScope();
            kernel.Bind<ProjectConfigErrorMessages>().To<ProjectConfigErrorMessages>().InSingletonScope();

            kernel.Bind<DBVersionsViewModelData>().To<DBVersionsViewModelData>().InSingletonScope();
            kernel.Bind<DBVersionsControls>().To<DBVersionsControls>().InSingletonScope();

        }
    }
}
