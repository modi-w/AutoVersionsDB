using AutoVersionsDB.Helpers;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Main;
using AutoVersionsDB.UI.Notifications;
using Ninject;

namespace AutoVersionsDB.UI
{
    public static class DIConfig
    {
        public static IKernel Kernel { get; private set; }

        public static void SetKernelInstance(IKernel kernel)
        {
            RegisterServices(kernel);

            Kernel = kernel;
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel.ThrowIfNull(nameof(kernel));



            kernel.Bind<NotificationsControls>().To<NotificationsControls>().InSingletonScope();

            kernel.Bind<NotificationsViewModelData>().To<NotificationsViewModelData>().InSingletonScope();

            //kernel.Bind<INotificationsViewModel>().To<INotificationsViewModel>().InSingletonScope();


            kernel.Bind<MainControls>().To<MainControls>().InSingletonScope();

            kernel.Bind<MainViewModelData>().To<MainViewModelData>().InSingletonScope();

            kernel.Bind<MainViewModel>().To<MainViewModel>().InSingletonScope();


            kernel.Bind<ChooseProjectViewModelData>().To<ChooseProjectViewModelData>().InSingletonScope();

            kernel.Bind<ChooseProjectViewModel>().To<ChooseProjectViewModel>().InSingletonScope();

            kernel.Bind<EditProjectControls>().To<EditProjectControls>().InSingletonScope();

            kernel.Bind<ProjectConfigErrorMessages>().To<ProjectConfigErrorMessages>().InSingletonScope();

            kernel.Bind<EditProjectViewModel>().To<EditProjectViewModel>().InSingletonScope();


            kernel.Bind<DBVersionsViewModelData>().To<DBVersionsViewModelData>().InSingletonScope();

            kernel.Bind<DBVersionsControls>().To<DBVersionsControls>().InSingletonScope();

            //kernel.Bind<IDBVersionsViewSateManager>().To<IDBVersionsViewSateManager>().InSingletonScope();


            kernel.Bind<DBVersionsViewModel>().To<DBVersionsViewModel>().InSingletonScope();




        }
    }
}
