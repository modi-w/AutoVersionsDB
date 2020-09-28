using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DeliveryEnv_SyncDB_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DeliveryEnv_SyncDB_DBInMiddleState()
        {
            RunTest<DeliveryEnv_SyncDB_DBInMiddleState_API, DeliveryEnv_SyncDB_DBInMiddleState_CLI>(false, ScriptFilesStateType.WithDevDummyDataFiles);
        }

        protected void RunTest<T1, T2>(bool devEnvironment, ScriptFilesStateType scriptFilesStateType)
            where T1 : TestDefinition
            where T2 : TestDefinition
        {
            List<ProjectConfigItem> projectConfigs = ProjectConfigsFactory.Create(devEnvironment, scriptFilesStateType);

            foreach (var projectConfig in projectConfigs)
            {
                var tests = NinjectUtils_IntegrationTests.GetTestDefinitions<T1, T2>();

                foreach (var test in tests)
                {
                    TestContext testContext = test.Arrange(projectConfig);

                    test.Act(testContext);

                    test.Assert(testContext);
                }
            }
        }

    }
}
