using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId
{
    public class ChangeProjectId_CLI : TestDefinition
    {
        private readonly ChangeProjectId_API _changeProjectId_API;

        public ChangeProjectId_CLI(ChangeProjectId_API changeProjectId_API)
        {
            _changeProjectId_API = changeProjectId_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _changeProjectId_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"config change-id -id={ChangeProjectId_API.OldProjectId} -nid={ChangeProjectId_API.NewProjectId}");
        }


        public override void Asserts(TestContext testContext)
        {
            _changeProjectId_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'change-id' for 'TestProject_Old'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);

        }


        public override void Release(TestContext testContext)
        {
            _changeProjectId_API.Release(testContext);

        }

    }
}
