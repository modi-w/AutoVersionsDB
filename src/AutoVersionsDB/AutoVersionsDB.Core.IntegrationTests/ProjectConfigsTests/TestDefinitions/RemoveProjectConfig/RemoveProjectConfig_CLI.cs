using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.RemoveProjectConfig;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.RemoveProjectConfig
{
    public class RemoveProjectConfig_CLI : TestDefinition
    {
        private readonly RemoveProjectConfig_API _removeProjectConfig_API;

        public RemoveProjectConfig_CLI(RemoveProjectConfig_API removeProjectConfig_API)
        {
            _removeProjectConfig_API = removeProjectConfig_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _removeProjectConfig_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"remove -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(TestContext testContext)
        {
            _removeProjectConfig_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'remove' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);

        }


        public override void Release(TestContext testContext)
        {
            _removeProjectConfig_API.Release(testContext);

        }

    }
}
