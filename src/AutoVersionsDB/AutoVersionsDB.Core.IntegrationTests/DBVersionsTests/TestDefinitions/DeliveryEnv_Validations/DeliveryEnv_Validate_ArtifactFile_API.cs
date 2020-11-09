using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;



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
    public class DeliveryEnv_Validate_ArtifactFile_API : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_Validate_ArtifactFile_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                    ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts) as DBVersionsAPITestContext;

            testContext.ProjectConfig.DeliveryArtifactFolderPath += "_NotExistFolderSuffix";

            if (Directory.Exists(testContext.ProjectConfig.DeliveryArtifactFolderPath))
            {
                Directory.Delete(testContext.ProjectConfig.DeliveryArtifactFolderPath);
            }

            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);


            return testContext;
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateDBVersions(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "ArtifactFile");

        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
