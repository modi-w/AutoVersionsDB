using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.NotificationableEngine;
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
    public static class MockObjectsProvider
    {
        private static StandardKernel _ninjectKernelContainer;

        public static Mock<ProjectConfigsStorage> MockProjectConfigsStorage { get; private set; }

        public static Mock<IStandardStreamWriter> MockConsoleError { get; private set; }
        public static Mock<IStandardStreamWriter> MockConsoleOut { get; private set; }
        public static Mock<IConsoleExtended> MockConsole { get; private set; }

        public static Mock<ConsoleProcessMessagesForTests> MockConsoleProcessMessages { get; private set; }

        public static void Init(IKernel kernel)
        {
            MockProjectConfigsStorage = new Mock<ProjectConfigsStorage>();
            MockProjectConfigsStorage.Setup(m => m.IsIdExsit(IntegrationTestsConsts.TestProjectId)).Returns(true);

            kernel.Bind<ProjectConfigsStorage>().ToConstant(MockProjectConfigsStorage.Object);


            MockConsoleError = new Mock<IStandardStreamWriter>();
            MockConsoleOut = new Mock<IStandardStreamWriter>();

            MockConsole = new Mock<IConsoleExtended>();
            MockConsole.Setup(m => m.Error).Returns(MockConsoleError.Object);
            MockConsole.Setup(m => m.Out).Returns(MockConsoleOut.Object);
            kernel.Bind<IConsoleExtended>().ToConstant(MockConsole.Object);


            ConsoleProcessMessages internalConsoleProcessMessages = kernel.Get<ConsoleProcessMessages>();

            MockConsoleProcessMessages = new Mock<ConsoleProcessMessagesForTests>(MockBehavior.Strict, internalConsoleProcessMessages);
            kernel.Bind<IConsoleProcessMessages>().ToConstant(MockConsoleProcessMessages.Object);

        }



        public static void SetProcessResultsToTestContext(TestContext testContext)
        {
            MockConsoleProcessMessages
             .Setup(m => m.ProcessCompleteForMockSniffer(It.IsAny<ProcessResults>()))
             .Callback<ProcessResults>((processResults) =>
             {
                 testContext.ProcessResults = processResults;
             });
        }

        public static void SetConsoleOutputToTestContext(TestContext testContext)
        {
            MockConsole
             .Setup(m => m.Out.Write(It.IsAny<string>()))
             .Callback<string>((str) =>
             {
                 testContext.AppendToConsoleOut(str);
             });

            //MockConsole
            // .Setup(m => m.Out.WriteLine(It.IsAny<string>()))
            // .Callback<string>((str) =>
            // {
            //     testContext.AppendLineToConsoleOut(str);
            // });


            MockConsole
             .Setup(m => m.SetCursorPosition(It.IsAny<int>(), It.IsAny<int>()))
             .Callback<int,int>((left,top) =>
             {
                 testContext.ClearCurrentMessage(left);
             });

            
        }
    }
}
