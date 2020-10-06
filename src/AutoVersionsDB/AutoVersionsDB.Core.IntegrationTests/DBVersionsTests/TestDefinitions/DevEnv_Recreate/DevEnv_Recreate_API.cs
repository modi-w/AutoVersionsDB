using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate
{
    public class DevEnv_Recreate_API : ITestDefinition
    {
        private readonly DBVersionsValidTest _dbVersionsValidTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Recreate_API(DBVersionsValidTest dbVersionsValidTest,
                                        ScriptFilesAsserts scriptFilesAsserts,
                                        DBAsserts dbAsserts)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            return _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.RecreateDBFromScratch(testContext.ProjectConfig.Id,null, null);
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsValidTest.Asserts(testContext);

            _dbAsserts.AssertDbInFinalState_DevEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
        }



        public void Release(TestContext testContext)
        {
            _dbVersionsValidTest.Release(testContext);
        }

    }
}
