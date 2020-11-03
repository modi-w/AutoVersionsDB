using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes
{
    public class GetDBTypes_CLI : TestDefinition
    {

        public GetDBTypes_CLI()
        {
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = new TestContext(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"dbtypes");
        }


        public override void Asserts(TestContext testContext)
        {

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,5);
            assertTextByLines.AssertLineMessage("> Run 'dbtypes' (no params)", true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("  Code       |  Name", true);
            assertTextByLines.AssertLineMessage("--------------------", true);
            assertTextByLines.AssertLineMessage("+ SqlServer  | Sql Server", true);

        }


        public override void Release(TestContext testContext)
        {
        }

    }
}
