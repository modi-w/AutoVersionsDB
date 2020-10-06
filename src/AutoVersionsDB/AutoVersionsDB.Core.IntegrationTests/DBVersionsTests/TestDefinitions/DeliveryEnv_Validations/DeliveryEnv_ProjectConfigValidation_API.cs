using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_ProjectConfigValidation_API : ITestDefinition
    {
        private readonly DBVersionsValidTest _dbVersionsValidTest;

        public DeliveryEnv_ProjectConfigValidation_API(DBVersionsValidTest dbVersionsValidTest)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            return _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateProjectConfig(testContext.ProjectConfig.Id, null);
        }


        public void Asserts(TestContext testContext)
        {
            //Comment: When we implement the  _dbAsserts.AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(), we should not call this method here.
            //          Because in this process we dont create a backup file.
            //          The above method is called on DBVersionsTest.Asserts()
            _dbVersionsValidTest.Asserts(testContext);
        }


        public void Release(TestContext testContext)
        {
            _dbVersionsValidTest.Release(testContext);
        }

    }
}
