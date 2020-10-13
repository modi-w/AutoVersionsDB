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

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"dbtypes");
        }


        public override void Asserts(TestContext testContext)
        {

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'dbtypes' (no params)", true);
            assertTextByLines.AssertLineMessage(1, "", true);
            assertTextByLines.AssertLineMessage(2, "  Code       |  Name", true);
            assertTextByLines.AssertLineMessage(3, "--------------------", true);
            assertTextByLines.AssertLineMessage(4, "+ SqlServer  | Sql Server", true);

        }


        public override void Release(TestContext testContext)
        {
        }

    }
}
