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

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById
{
    public class GetProjectConfigById_DevEnv_CLI : TestDefinition
    {
        private readonly GetProjectConfigById_DevEnv_API _getProjectConfigById_API;

        public GetProjectConfigById_DevEnv_CLI(GetProjectConfigById_DevEnv_API getProjectConfigById_API)
        {
            _getProjectConfigById_API = getProjectConfigById_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _getProjectConfigById_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"info -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(TestContext testContext)
        {
            _getProjectConfigById_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,15);
            assertTextByLines.AssertLineMessage("> Run 'info' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage($"Id                                 : {IntegrationTestsConsts.DummyProjectConfig.Id}", true);
            assertTextByLines.AssertLineMessage($"Description                        : {IntegrationTestsConsts.DummyProjectConfig.Description}", true);
            assertTextByLines.AssertLineMessage($"DBType                             : {IntegrationTestsConsts.DummyProjectConfig.DBType}", true);
            assertTextByLines.AssertLineMessage($"ServerInstance                     : {IntegrationTestsConsts.DummyProjectConfig.Server}", true);
            assertTextByLines.AssertLineMessage($"DataBaseName                       : {IntegrationTestsConsts.DummyProjectConfig.DBName}", true);
            assertTextByLines.AssertLineMessage($"DBUsername                         : {IntegrationTestsConsts.DummyProjectConfig.Username}", true);
            assertTextByLines.AssertLineMessage($"DBPassword                         : {IntegrationTestsConsts.DummyProjectConfig.Password}", true);
            assertTextByLines.AssertLineMessage($"Backup Folder Path                 : {IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath}", true);
            assertTextByLines.AssertLineMessage($"Dev Environment                    : {IntegrationTestsConsts.DummyProjectConfig.DevEnvironment}", true);
            assertTextByLines.AssertLineMessage( $"Scripts Base Folder                : {IntegrationTestsConsts.DummyProjectConfig.ScriptsBaseFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Incremental Scripts Folder        : {IntegrationTestsConsts.DummyProjectConfig.IncrementalScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Repeatable Scripts Folder         : {IntegrationTestsConsts.DummyProjectConfig.RepeatableScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $" Dev Dummy Data Scripts Folder     : {IntegrationTestsConsts.DummyProjectConfig.DevDummyDataScriptsFolderPath}", true);
            assertTextByLines.AssertLineMessage( $"Deploy Artifact Folder             : {IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath}", true);

        }


        public override void Release(TestContext testContext)
        {
            _getProjectConfigById_API.Release(testContext);

        }

    }
}
