using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Notifications;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.IO;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class NinjectUtils_IntegrationTests
    {
        public static IKernel NinjectKernelContainer { get; private set; }


        public static void CreateKernel()
        {

            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());


            RegisterServices();

            NinjectUtils.SetKernelInstance(NinjectKernelContainer);

            NinjectUtils_UI.SetKernelInstance(NinjectKernelContainer);



            ComposeObjects();
        }

        private static void RegisterServices()
        {

            NinjectKernelContainer.Bind<IConsoleProcessMessages>().To<ConsoleProcessMessagesForTests>().InSingletonScope();
            NinjectKernelContainer.Bind<INotificationsViewModel>().To<NotificationsViewModelForTests>().InSingletonScope();
            NinjectKernelContainer.Bind<IDBVersionsViewSateManager>().To<DBVersionsViewSateManagerForTests>().InSingletonScope();
            NinjectKernelContainer.Bind<IEditProjectViewSateManager>().To<EditProjectViewSateManagerForTests>().InSingletonScope();
            
        }

        private static void ComposeObjects()
        {
            MockObjectsProvider.Init(NinjectKernelContainer);

            ViewRouter viewRouter = NinjectKernelContainer.Get<ViewRouter>();
            NinjectKernelContainer.Bind<ViewRouter>().ToConstant(viewRouter);


        }




        public static IEnumerable<TestDefinition> GetTestDefinitions<T1>()
                where T1 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());

            return testDefinitions;
        }
        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2>()
            where T1 : TestDefinition
            where T2 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());

            return testDefinitions;
        }

        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());

            return testDefinitions;
        }

        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3, T4>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());
            testDefinitions.Add(NinjectKernelContainer.Get<T4>());

            return testDefinitions;
        }


        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3, T4, T5>()
            where T1 : TestDefinition
            where T2 : TestDefinition
            where T3 : TestDefinition
            where T4 : TestDefinition
            where T5 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());
            testDefinitions.Add(NinjectKernelContainer.Get<T4>());
            testDefinitions.Add(NinjectKernelContainer.Get<T5>());

            return testDefinitions;
        }

        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2, T3, T4, T5,T6>()
          where T1 : TestDefinition
          where T2 : TestDefinition
          where T3 : TestDefinition
          where T4 : TestDefinition
          where T5 : TestDefinition
          where T6 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());
            testDefinitions.Add(NinjectKernelContainer.Get<T3>());
            testDefinitions.Add(NinjectKernelContainer.Get<T4>());
            testDefinitions.Add(NinjectKernelContainer.Get<T5>());
            testDefinitions.Add(NinjectKernelContainer.Get<T6>());

            return testDefinitions;
        }


    }
}
