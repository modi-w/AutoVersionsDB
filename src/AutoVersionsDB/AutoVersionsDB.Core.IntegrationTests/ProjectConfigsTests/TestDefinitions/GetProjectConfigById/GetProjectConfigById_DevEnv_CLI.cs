using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById
{
    public class GetProjectConfigById_DevEnv_CLI : TestDefinition<CLITestContext>
    {
        private readonly GetProjectConfigById_DevEnv_API _getProjectConfigById_API;

        public GetProjectConfigById_DevEnv_CLI(GetProjectConfigById_DevEnv_API getProjectConfigById_API)
        {
            _getProjectConfigById_API = getProjectConfigById_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_getProjectConfigById_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"info -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _getProjectConfigById_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,15);
            assertTextByLines.AssertLineMessage("> Run 'info' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage($"Id                                 : {IntegrationTestsConsts.DummyProjectConfigValid.Id}", true);
            assertTextByLines.AssertLineMessage($"Description                        : {IntegrationTestsConsts.DummyProjectConfigValid.Description}", true);
            assertTextByLines.AssertLineMessage($"DBType                             : {IntegrationTestsConsts.DummyProjectConfigValid.DBType}", true);
            assertTextByLines.AssertLineMessage($"ServerInstance                     : {IntegrationTestsConsts.DummyProjectConfigValid.Server}", true);
            assertTextByLines.AssertLineMessage($"DataBaseName                       : {IntegrationTestsConsts.DummyProjectConfigValid.DBName}", true);
            assertTextByLines.AssertLineMessage($"DBUsername                         : {IntegrationTestsConsts.DummyProjectConfigValid.Username}", true);
            assertTextByLines.AssertLineMessage($"DBPassword                         : {IntegrationTestsConsts.DummyProjectConfigValid.Password}", true);
            assertTextByLines.AssertLineMessage($"Backup Folder Path                 : {IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath}", true);
            assertTextByLines.AssertLineMessage($"Dev Environment                    : {IntegrationTestsConsts.DummyProjectConfigValid.DevEnvironment}", true);
            assertTextByLines.AssertLineMessage( $"Scripts Base Folder                : {IntegrationTestsConsts.DummyProjectConfigValid.ScriptsBaseFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Incremental Scripts Folder        : {IntegrationTestsConsts.DummyProjectConfigValid.IncrementalScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Repeatable Scripts Folder         : {IntegrationTestsConsts.DummyProjectConfigValid.RepeatableScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Dev Dummy Data Scripts Folder     : {IntegrationTestsConsts.DummyProjectConfigValid.DevDummyDataScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $"Deploy Artifact Folder             : {IntegrationTestsConsts.DummyProjectConfigValid.DeployArtifactFolderPath}", true);

        }


        public override void Release(CLITestContext testContext)
        {
            _getProjectConfigById_API.Release(testContext);

        }

    }
}
