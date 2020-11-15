using AutoVersionsDB;
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
    public class Init_DevEnv_AllProperties_CLI : TestDefinition<CLITestContext>
    {
        private readonly Init_DevEnv_AllProperties_API _init_AllProperties_API;

        public Init_DevEnv_AllProperties_CLI(Init_DevEnv_AllProperties_API init_AllProperties_API)
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
            string args = $"-id={IntegrationTestsConsts.DummyProjectConfig.Id} ";
            args+= $"-desc={IntegrationTestsConsts.DummyProjectConfig.Description} ";
            args += $"-dbt={IntegrationTestsConsts.DummyProjectConfig.DBType} ";
            args += $"-ser={IntegrationTestsConsts.DummyProjectConfig.Server} ";
            args += $"-db={IntegrationTestsConsts.DummyProjectConfig.DBName} ";
            args += $"-un={IntegrationTestsConsts.DummyProjectConfig.Username} ";
            args += $"-pass={IntegrationTestsConsts.DummyProjectConfig.Password} ";
            args += $"-buf={IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath} ";
            args += $"-dev=true ";
            args += $"-dsf={IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath} ";
            args += $"-def={IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath} ";

            AutoVersionsDBAPI.CLIRun($"init {args}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _init_AllProperties_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);
            assertTextByLines.AssertLineMessage("> Run 'init' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);

        }


        public override void Release(CLITestContext testContext)
        {
            _init_AllProperties_API.Release(testContext);

        }

    }
}
