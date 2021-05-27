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
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init
{
    public class Init_DeliveryEnv_AllProperties_CLI : TestDefinition<CLITestContext>
    {
        private readonly Init_DeliveryEnv_AllProperties_API _init_AllProperties_API;

        public Init_DeliveryEnv_AllProperties_CLI(Init_DeliveryEnv_AllProperties_API init_AllProperties_API)
        {
            _init_AllProperties_API = init_AllProperties_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_init_AllProperties_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            string args = $"-id={IntegrationTestsConsts.DummyProjectConfigValid.Id} ";
            args += $"-desc={IntegrationTestsConsts.DummyProjectConfigValid.Description} ";
            args += $"-dbt={IntegrationTestsConsts.DummyProjectConfigValid.DBType} ";
            args += $"-ser={IntegrationTestsConsts.DummyProjectConfigValid.Server} ";
            args += $"-db={IntegrationTestsConsts.DummyProjectConfigValid.DBName} ";
            args += $"-un={IntegrationTestsConsts.DummyProjectConfigValid.Username} ";
            args += $"-pass={IntegrationTestsConsts.DummyProjectConfigValid.Password} ";
            args += $"-buf={IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath} ";
            args += $"-dev=false ";
            args += $"-darf={IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath} ";

            CLIRunner.CLIRun($"init {args}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _init_AllProperties_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "init").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);

        }


        public override void Release(CLITestContext testContext)
        {
            _init_AllProperties_API.Release(testContext);

        }

    }
}
