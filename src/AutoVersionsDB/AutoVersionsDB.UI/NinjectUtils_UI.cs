using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.Main;
using AutoVersionsDB.UI.Notifications;

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



            var notificationsControls = kernel.Get<NotificationsControls>();
            kernel.Bind<NotificationsControls>().ToConstant(notificationsControls);

            var notificationsViewModelData = kernel.Get<NotificationsViewModelData>();
            kernel.Bind<NotificationsViewModelData>().ToConstant(notificationsViewModelData);

            //var notificationsViewModel = kernel.Get<NotificationsViewModel>();
            //kernel.Bind<NotificationsViewModel>().ToConstant(notificationsViewModel);


            var mainControls = kernel.Get<MainControls>();
            kernel.Bind<MainControls>().ToConstant(mainControls);

            var mainViewModelData = kernel.Get<MainViewModelData>();
            kernel.Bind<MainViewModelData>().ToConstant(mainViewModelData);

            var mainViewModel = kernel.Get<MainViewModel>();
            kernel.Bind<MainViewModel>().ToConstant(mainViewModel);


            var chooseProjectViewModelData = kernel.Get<ChooseProjectViewModelData>();
            kernel.Bind<ChooseProjectViewModelData>().ToConstant(chooseProjectViewModelData);

            var chooseProjectViewModel = kernel.Get<ChooseProjectViewModel>();
            kernel.Bind<ChooseProjectViewModel>().ToConstant(chooseProjectViewModel);

            var editProjectControls = kernel.Get<EditProjectControls>();
            kernel.Bind<EditProjectControls>().ToConstant(editProjectControls);

            var projectConfigErrorMessages = kernel.Get<ProjectConfigErrorMessages>();
            kernel.Bind<ProjectConfigErrorMessages>().ToConstant(projectConfigErrorMessages);

            var editProjectViewModel = kernel.Get<EditProjectViewModel>();
            kernel.Bind<EditProjectViewModel>().ToConstant(editProjectViewModel);


            var dbVersionsViewModelData = kernel.Get<DBVersionsViewModelData>();
            kernel.Bind<DBVersionsViewModelData>().ToConstant(dbVersionsViewModelData);

            var dbVersionsControls = kernel.Get<DBVersionsControls>();
            kernel.Bind<DBVersionsControls>().ToConstant(dbVersionsControls);

            var dbVersionsViewModel = kernel.Get<DBVersionsViewModel>();
            kernel.Bind<DBVersionsViewModel>().ToConstant(dbVersionsViewModel);


            ViewRouter viewRouter = kernel.Get<ViewRouter>();
            kernel.Bind<ViewRouter>().ToConstant(viewRouter);


        }
    }
}
