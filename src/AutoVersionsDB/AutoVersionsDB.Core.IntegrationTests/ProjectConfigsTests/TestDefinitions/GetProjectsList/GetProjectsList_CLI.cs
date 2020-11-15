using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList
{
    public class GetProjectsList_CLI : TestDefinition<CLITestContext>
    {
        private readonly GetProjectsList_API _getProjectsList_API;

        public GetProjectsList_CLI(GetProjectsList_API getProjectsList_API)
        {
            _getProjectsList_API = getProjectsList_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_getProjectsList_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"list");
        }


        public override void Asserts(CLITestContext testContext)
        {

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,6);
            assertTextByLines.AssertLineMessage("> Run 'list' (no params)", true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("  Id                            |  Description", true);
            assertTextByLines.AssertLineMessage("-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage(" TestProject1                   | Test Project 1", true);
            assertTextByLines.AssertLineMessage(" TestProject2                   | Test Project 2", true);

        }


        public override void Release(CLITestContext testContext)
        {
            _getProjectsList_API.Release(testContext);
        }

    }
}
