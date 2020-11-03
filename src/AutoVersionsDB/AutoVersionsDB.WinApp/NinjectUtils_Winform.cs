using AutoVersionsDB.Core;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.Notifications;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.WinApp
{
    public static class NinjectUtils_Winform
    {
        /* https://github.com/ninject/Ninject.Web/blob/master/src/Ninject.Web/WebServiceBase.cs
         * 
         * https://stackoverflow.com/questions/14127763/dependency-injection-in-winforms-using-ninject-and-entity-framework
         * https://gist.github.com/dkellycollins/9c3fecaedd830094d7f2
         */



        public static IKernel NinjectKernelContainer { get; private set; }


        public static void CreateKernel()
        {
            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            var notificationsViewModel = NinjectKernelContainer.Get<NotificationsViewModel>();
            NinjectKernelContainer.Bind<INotificationsViewModel>().ToConstant(notificationsViewModel);


            NinjectUtils.SetKernelInstance(NinjectKernelContainer);
            NinjectUtils_UI.SetKernelInstance(NinjectKernelContainer);

            RegisterServices(NinjectKernelContainer);
        }

        private static void RegisterServices(IKernel kernel)
        {
            //  kernel.ThrowIfNull(nameof(kernel));

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
