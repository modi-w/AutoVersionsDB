﻿using AutoVersionsDB;
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
    public class GetProjectConfigById_DeliveryEnv_CLI : TestDefinition
    {
        private readonly GetProjectConfigById_DeliveryEnv_API _getProjectConfigById_API;

        public GetProjectConfigById_DeliveryEnv_CLI(GetProjectConfigById_DeliveryEnv_API getProjectConfigById_API)
        {
            _getProjectConfigById_API = getProjectConfigById_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _getProjectConfigById_API.Arrange(testArgs);
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"info -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(TestContext testContext)
        {
            _getProjectConfigById_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'info' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, $"Id                                 : {IntegrationTestsConsts.DummyProjectConfig.Id}", true);
            assertTextByLines.AssertLineMessage(2, $"Description                        : {IntegrationTestsConsts.DummyProjectConfig.Description}", true);
            assertTextByLines.AssertLineMessage(3, $"DBType                             : {IntegrationTestsConsts.DummyProjectConfig.DBType}", true);
            assertTextByLines.AssertLineMessage(4, $"ServerInstance                     : {IntegrationTestsConsts.DummyProjectConfig.Server}", true);
            assertTextByLines.AssertLineMessage(5, $"DataBaseName                       : {IntegrationTestsConsts.DummyProjectConfig.DBName}", true);
            assertTextByLines.AssertLineMessage(6, $"DBUsername                         : {IntegrationTestsConsts.DummyProjectConfig.Username}", true);
            assertTextByLines.AssertLineMessage(7, $"DBPassword                         : {IntegrationTestsConsts.DummyProjectConfig.Password}", true);
            assertTextByLines.AssertLineMessage(8, $"Backup Folder Path                 : {IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath}", true);
            assertTextByLines.AssertLineMessage(9, $"Dev Environment                    : {false}", true);
            assertTextByLines.AssertLineMessage(10, $"Delivery Artifact Folder Path      : {IntegrationTestsConsts.DummyProjectConfig.DeliveryArtifactFolderPath}", true);

        }


        public override void Release(TestContext testContext)
        {
            _getProjectConfigById_API.Release(testContext);

        }

    }
}
