using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
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
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class MockObjectsProvider
    {
        private static StandardKernel _ninjectKernelContainer;


        public static Mock<IStandardStreamWriter> MockConsoleError { get; private set; }
        public static Mock<IStandardStreamWriter> MockConsoleOut { get; private set; }
        public static Mock<IConsoleExtended> MockConsole { get; private set; }

        public static Mock<ConsoleProcessMessagesForTests> MockConsoleProcessMessages { get; private set; }

        public static Mock<NotificationsViewModelForTests> MockNotificationsViewModel { get; private set; }

        public static Mock<DBVersionsViewSateManagerForTests> MockDBVersionsViewSateManagerFotTests { get; private set; }

        public static Mock<EditProjectViewSateManagerForTests> MockEditProjectViewSateManagerFotTests { get; private set; }
        public static Mock<OsProcessUtils> MockOsProcessUtils { get; private set; }


        public static void Init(IKernel kernel)
        {
            AutoVersionsDBSettings setting = new AutoVersionsDBSettings(@"[CommonApplicationData]\AutoVersionsDB.IntegrationTests");
            kernel.Bind<AutoVersionsDBSettings>().ToConstant(setting);


            MockConsoleError = new Mock<IStandardStreamWriter>();
            MockConsoleOut = new Mock<IStandardStreamWriter>();

            MockConsole = new Mock<IConsoleExtended>();
            MockConsole.Setup(m => m.Error).Returns(MockConsoleError.Object);
            MockConsole.Setup(m => m.Out).Returns(MockConsoleOut.Object);
            kernel.Bind<IConsoleExtended>().ToConstant(MockConsole.Object);


            ConsoleProcessMessages internalConsoleProcessMessages = kernel.Get<ConsoleProcessMessages>();
            MockConsoleProcessMessages = new Mock<ConsoleProcessMessagesForTests>(MockBehavior.Strict, internalConsoleProcessMessages);
            kernel.Rebind<IConsoleProcessMessages>().ToConstant(MockConsoleProcessMessages.Object);


            NotificationsViewModel internalNotificationsViewModel = kernel.Get<NotificationsViewModel>();
            MockNotificationsViewModel = new Mock<NotificationsViewModelForTests>(MockBehavior.Strict, internalNotificationsViewModel);
            kernel.Rebind<INotificationsViewModel>().ToConstant(MockNotificationsViewModel.Object);

            DBVersionsViewSateManager internalDBVersionsViewSateManager = kernel.Get<DBVersionsViewSateManager>();
            MockDBVersionsViewSateManagerFotTests = new Mock<DBVersionsViewSateManagerForTests>(MockBehavior.Strict, internalDBVersionsViewSateManager);
            kernel.Rebind<IDBVersionsViewSateManager>().ToConstant(MockDBVersionsViewSateManagerFotTests.Object);

            EditProjectViewSateManager internalEditProjectViewSateManager = kernel.Get<EditProjectViewSateManager>();
            MockEditProjectViewSateManagerFotTests = new Mock<EditProjectViewSateManagerForTests>(MockBehavior.Strict, internalEditProjectViewSateManager);
            kernel.Rebind<IEditProjectViewSateManager>().ToConstant(MockEditProjectViewSateManagerFotTests.Object);



            MockOsProcessUtils = new Mock<OsProcessUtils>();
            kernel.Bind<OsProcessUtils>().ToConstant(MockOsProcessUtils.Object);

            MockOsProcessUtils
                .Setup(m => m.StartOsProcess(It.IsAny<string>()))
             .Callback<string>((filename) =>
             {
                 //Do nothing
             });

            UIGeneralEvents.OnException += UIGeneralEvents_OnException;

        }



        private static void UIGeneralEvents_OnException(object sender, string exceptionMessage)
        {
            Console.WriteLine(exceptionMessage);
            Debug.WriteLine(exceptionMessage);

            throw new Exception(exceptionMessage);
        }

        public static void SetTestContextDataByMockCallbacksForUI(ITestContext testContext)
        {
            MockNotificationsViewModel
             .Setup(m => m.AfterCompleteForMockSniffer(It.IsAny<ProcessResults>()))
             .Callback<ProcessResults>((processResults) =>
             {
                 testContext.ProcessResults = processResults;
             });

            if (testContext is DBVersionsUITestContext)
            {
                DBVersionsUITestContext dbVersionsTestContext = testContext as DBVersionsUITestContext;

                MockDBVersionsViewSateManagerFotTests
                 .Setup(m => m.ChangeViewStateForMockSniffer(It.IsAny<DBVersionsViewStateType>()))
                 .Callback<DBVersionsViewStateType>((viewType) =>
                 {
                     dbVersionsTestContext.ViewStatesHistory.Add(viewType);
                 });

            }

            if (testContext is EditProjectUITestContext)
            {
                EditProjectUITestContext editProjectAPITestContext = testContext as EditProjectUITestContext;

                MockEditProjectViewSateManagerFotTests
                 .Setup(m => m.ChangeViewStateForMockSniffer(It.IsAny<EditProjectViewStateType>()))
                 .Callback<EditProjectViewStateType>((viewType) =>
                 {
                     editProjectAPITestContext.ViewStatesHistory.Add(viewType);
                 });

            }
        }





        public static void SetTestContextDataByMockCallbacksForCLI(CLITestContext testContext)
        {
            MockObjectsProvider.SetProcessResultsToTestContextForCLI(testContext);
            MockObjectsProvider.SetConsoleOutputToTestContext(testContext);

        }



        private static void SetProcessResultsToTestContextForCLI(CLITestContext testContext)
        {
            MockConsoleProcessMessages
             .Setup(m => m.ProcessCompleteForMockSniffer(It.IsAny<ProcessResults>()))
             .Callback<ProcessResults>((processResults) =>
             {
                 testContext.ProcessResults = processResults;
             });
        }

        private static void SetConsoleOutputToTestContext(CLITestContext testContext)
        {
            MockConsole
             .Setup(m => m.Out.Write(It.IsAny<string>()))
             .Callback<string>((str) =>
             {
                 testContext.AppendToConsoleOut(str);
             });


            MockConsole
             .Setup(m => m.SetCursorPosition(It.IsAny<int>(), It.IsAny<int>()))
             .Callback<int, int>((left, top) =>
              {
                  testContext.ClearCurrentMessage(left);
              });


            MockConsole
             .Setup(m => m.Error.Write(It.IsAny<string>()))
             .Callback<string>((str) =>
             {
                 testContext.AppendToConsoleError(str);
             });

        }
    }
}
