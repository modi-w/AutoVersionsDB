using AutoVersionsDB;
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

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config
{
    public class Config_DevEnv_CLI : TestDefinition
    {
        private readonly Config_DevEnv_API config_API;

        public Config_DevEnv_CLI(Config_DevEnv_API init_AllProperties_API)
        {
            config_API = init_AllProperties_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = config_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            string args = $"-id={IntegrationTestsConsts.DummyProjectConfig.Id} ";
            args += $"-desc={IntegrationTestsConsts.DummyProjectConfig.Description} ";
            args += $"-dbt={IntegrationTestsConsts.DummyProjectConfig.DBType} ";
            args += $"-ser={IntegrationTestsConsts.DummyProjectConfig.Server} ";
            args += $"-db={IntegrationTestsConsts.DummyProjectConfig.DBName} ";
            args += $"-un={IntegrationTestsConsts.DummyProjectConfig.Username} ";
            args += $"-pass={IntegrationTestsConsts.DummyProjectConfig.Password} ";
            args += $"-buf={IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath} ";
            args += $"-dsf={IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath} ";
            args += $"-def={IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath} ";

            AutoVersionsDBAPI.CLIRun($"config {args}");
        }


        public override void Asserts(TestContext testContext)
        {
            config_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);
            assertTextByLines.AssertLineMessage("> Run 'config' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);

        }


        public override void Release(TestContext testContext)
        {
            config_API.Release(testContext);
        }

    }
}
