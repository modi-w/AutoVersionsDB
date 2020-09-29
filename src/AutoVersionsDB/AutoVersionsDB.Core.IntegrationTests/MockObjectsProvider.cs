using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
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
        public static Mock<IConsole> MockConsole { get; private set; }
        public static Mock<IConsoleProcessMessages> MockConsoleProcessMessages { get; private set; }


        public static void Init(IKernel kernel)
        {
            MockProjectConfigsStorage = new Mock<ProjectConfigsStorage>();
            MockProjectConfigsStorage.Setup(m => m.IsIdExsit(IntegrationTestsConsts.TestProjectId)).Returns(true);

            kernel.Bind<ProjectConfigsStorage>().ToConstant(MockProjectConfigsStorage.Object);


            MockConsoleProcessMessages = new Mock<IConsoleProcessMessages>();
            kernel.Bind<IConsoleProcessMessages>().ToConstant(MockConsoleProcessMessages.Object);


            Mock<IStandardStreamWriter> mockConsoleError = new Mock<IStandardStreamWriter>();
            Mock<IStandardStreamWriter> mockConsoleOut = new Mock<IStandardStreamWriter>();

            MockConsole = new Mock<IConsole>();
            MockConsole.Setup(m => m.Error).Returns(mockConsoleError.Object);
            MockConsole.Setup(m => m.Out).Returns(mockConsoleOut.Object);
            kernel.Bind<IConsole>().ToConstant(MockConsole.Object);

        }



        public static void SetProcessCompleteCallbackToSetProcessResultsInTestContext(TestContext testContext)
        {
            MockConsoleProcessMessages
             .Setup(m => m.ProcessComplete(It.IsAny<ProcessResults>()))
             .Callback<ProcessResults>((processResults) =>
             {
                 testContext.ProcessResults = processResults;
             });
        }
    }
}
