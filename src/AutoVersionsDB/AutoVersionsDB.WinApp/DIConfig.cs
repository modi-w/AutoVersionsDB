using AutoVersionsDB.Core;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Notifications;
using Ninject;
using System.Reflection;

namespace AutoVersionsDB.WinApp
{
    public static class DIConfig
    {
        /* https://github.com/ninject/Ninject.Web/blob/master/src/Ninject.Web/WebServiceBase.cs
         * 
         * https://stackoverflow.com/questions/14127763/dependency-injection-in-winforms-using-ninject-and-entity-framework
         * https://gist.github.com/dkellycollins/9c3fecaedd830094d7f2
         */



        public static IKernel Kernel { get; private set; }


        public static void CreateKernel()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());

            //var notificationsViewModel = NinjectKernelContainer.Get<NotificationsViewModel>();
            //NinjectKernelContainer.Bind<INotificationsViewModel>().ToConstant(notificationsViewModel);

            //var dbVersionsViewSateManager = NinjectKernelContainer.Get<DBVersionsViewSateManager>();
            //NinjectKernelContainer.Bind<IDBVersionsViewSateManager>().ToConstant(dbVersionsViewSateManager);


            RegisterServices();

            Core.DIConfig.SetKernelInstance(Kernel);
            UI.DIConfig.SetKernelInstance(Kernel);

            ComposeObjects();
        }

        private static void RegisterServices()
        {
            Kernel.Bind<INotificationsViewModel>().To<NotificationsViewModel>().InSingletonScope();
            Kernel.Bind<IDBVersionsViewSateManager>().To<DBVersionsViewSateManager>().InSingletonScope();
            Kernel.Bind<IEditProjectViewSateManager>().To<EditProjectViewSateManager>().InSingletonScope();

        }


        private static void ComposeObjects()
        {
            ViewRouter viewRouter = Kernel.Get<ViewRouter>();
            Kernel.Bind<ViewRouter>().ToConstant(viewRouter);

        }

        //public static void CreateKernelForTests()
        //{
        //    NinjectKernelContainer = new StandardKernel();
        //    NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

        //    RegisterServicesForTests(NinjectKernelContainer);
        //}

        //private static void RegisterServicesForTests(IKernel kernel)
        //{
        //   // kernel.ThrowIfNull(nameof(kernel));

        //    //TODO: register Mock services

        //}
    }
}
