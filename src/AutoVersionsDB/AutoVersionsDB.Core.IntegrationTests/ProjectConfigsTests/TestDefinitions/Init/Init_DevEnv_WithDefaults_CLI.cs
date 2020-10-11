﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init
{
    public class Init_DevEnv_WithDefaults_CLI : TestDefinition
    {
        private readonly Init_DevEnv_WithDefaults_API _init_WithDefaults_API;

        public Init_DevEnv_WithDefaults_CLI(Init_DevEnv_WithDefaults_API init_WithDefaults_API)
        {
            _init_WithDefaults_API = init_WithDefaults_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _init_WithDefaults_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            string args = $"-id={IntegrationTestsConsts.DummyProjectConfig.Id} ";
            args+= $"-desc={IntegrationTestsConsts.DummyProjectConfig.Description} ";
            args += $"-db={IntegrationTestsConsts.DummyProjectConfig.DBName} ";
            args += $"-un={IntegrationTestsConsts.DummyProjectConfig.Username} ";
            args += $"-pass={IntegrationTestsConsts.DummyProjectConfig.Password} ";
            args += $"-dev=true ";
            args += $"-dsf={IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath} ";
            args += $"-def={IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath} ";

            AutoVersionsDBAPI.CLIRun($"init {args}");
        }


        public override void Asserts(TestContext testContext)
        {
            _init_WithDefaults_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'init' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);

        }


        public override void Release(TestContext testContext)
        {
            _init_WithDefaults_API.Release(testContext);

        }

    }
}
