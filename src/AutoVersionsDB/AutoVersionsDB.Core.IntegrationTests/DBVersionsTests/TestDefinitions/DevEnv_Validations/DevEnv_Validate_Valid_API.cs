using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
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

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_Valid_API : ITestDefinition
    {
        private readonly DBVersionsValidTest _dbVersionsValidTest;

        public DevEnv_Validate_Valid_API(DBVersionsValidTest dbVersionsValidTest)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            return _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateDBVersions(testContext.ProjectConfig.Id, null);
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
