﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_ArtifactFile_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_Validate_ArtifactFile_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                                    ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _processAsserts = processAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ITestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);

            testContext.ProjectConfig.DeliveryArtifactFolderPath += "_NotExistFolderSuffix";

            if (Directory.Exists(testContext.ProjectConfig.DeliveryArtifactFolderPath))
            {
                Directory.Delete(testContext.ProjectConfig.DeliveryArtifactFolderPath);
            }

            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);


            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateDBVersions(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "ArtifactFile");

        }


        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
