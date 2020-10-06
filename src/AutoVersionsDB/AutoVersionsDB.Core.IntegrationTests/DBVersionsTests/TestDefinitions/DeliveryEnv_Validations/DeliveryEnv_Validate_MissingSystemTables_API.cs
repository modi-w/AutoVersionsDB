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
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_MissingSystemTables_API : ITestDefinition
    {
        private readonly DBVersionsNotValidTest _dbVersionsNotValidTest;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_Validate_MissingSystemTables_API(DBVersionsNotValidTest dbVersionsNotValidTest, 
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsNotValidTest = dbVersionsNotValidTest;
            _processAsserts = processAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            return _dbVersionsNotValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
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
            _dbVersionsNotValidTest.Asserts(testContext);

            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "SystemTables");

        }
    }
}
