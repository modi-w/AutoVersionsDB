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
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config
{
    public class Config_DevEnv_CLI : TestDefinition<CLITestContext>
    {
        private readonly Config_DevEnv_API config_API;

        public Config_DevEnv_CLI(Config_DevEnv_API init_AllProperties_API)
        {
            config_API = init_AllProperties_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(config_API.Arrange(testArgs));

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
            args += $"-dsf={IntegrationTestsConsts.DummyProjectConfigValid.DevScriptsBaseFolderPath} ";
            args += $"-def={IntegrationTestsConsts.DummyProjectConfigValid.DeployArtifactFolderPath} ";

            CLIRunner.CLIRun($"config {args}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            config_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "config").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);

        }


        public override void Release(CLITestContext testContext)
        {
            config_API.Release(testContext);
        }

    }
}
