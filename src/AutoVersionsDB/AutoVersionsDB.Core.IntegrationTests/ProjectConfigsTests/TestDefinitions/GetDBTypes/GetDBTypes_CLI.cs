using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.DB.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes
{
    public class GetDBTypes_CLI : TestDefinition<CLITestContext>
    {

        public GetDBTypes_CLI()
        {
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(new ProcessTestContext(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"dbtypes");
        }


        public override void Asserts(CLITestContext testContext)
        {

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 5);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageNoArgs.Replace("[processName]", "dbtypes"), true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("  Code       |  Name", true);
            assertTextByLines.AssertLineMessage("--------------------", true);
            assertTextByLines.AssertLineMessage($"+ {SqlServerDBTypeObjectsFactory.DBTypeCode}  | Sql Server", true);

        }


        public override void Release(CLITestContext testContext)
        {
        }

    }
}
