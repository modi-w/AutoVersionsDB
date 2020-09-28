using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
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

        public static Mock<ProjectConfigsStorage> MockProjectConfigsStorage { get; private set; }
        public static Mock<IConsole> MockConsole { get; private set; }
        public static Mock<IConsoleProcessMessages> MockConsoleProcessMessages { get; private set; }

        public static void CreateKernel()
        {
            NinjectKernelContainer = new StandardKernel();
            NinjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            RegisterServices(NinjectKernelContainer);

            NinjectUtils.SetKernelInstance(NinjectKernelContainer);
        }

        private static void RegisterServices(IKernel kernel)
        {
            MockProjectConfigsStorage = new Mock<ProjectConfigsStorage>();
            MockProjectConfigsStorage.Setup(m => m.IsIdExsit(IntegrationTestsSetting.TestProjectId)).Returns(true);
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




        public static IEnumerable<TestDefinition> GetTestDefinitions<T1, T2>()
            where T1 : TestDefinition
            where T2 : TestDefinition
        {
            List<TestDefinition> testDefinitions = new List<TestDefinition>();

            testDefinitions.Add(NinjectKernelContainer.Get<T1>());
            testDefinitions.Add(NinjectKernelContainer.Get<T2>());

            return testDefinitions;
        }




    }
}
