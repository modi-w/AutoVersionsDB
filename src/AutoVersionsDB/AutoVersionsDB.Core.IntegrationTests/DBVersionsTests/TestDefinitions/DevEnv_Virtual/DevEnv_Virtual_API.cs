using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual
{
    public class DevEnv_Virtual_API : ITestDefinition
    {
        private readonly DBVersionsValidTest _dbVersionsValidTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Virtual_API(DBVersionsValidTest dbVersionsValidTest,
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
            if (testContext.DBBackupFileType == DBBackupFileType.MiddleState)
            {
                testContext.ProcessResults = AutoVersionsDBAPI.SetDBStateByVirtualExecution(testContext.ProjectConfig.Id, IntegrationTestsConsts.TargetStateFile_FinalState, null);
            }
            else
            {
                testContext.ProcessResults = AutoVersionsDBAPI.SetDBStateByVirtualExecution(testContext.ProjectConfig.Id, IntegrationTestsConsts.TargetStateFile_MiddleState, null);
            }
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsValidTest.Asserts(testContext);

            if (testContext.DBBackupFileType == DBBackupFileType.MiddleState)
            {
                _dbAsserts.AssertDbInMiddleState(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            }
            else
            {
                _dbAsserts.AssertDbInEmptyStateExceptSystemTables(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
                _dbAsserts.AssertThatDbExecutedFilesAreInMiddleState(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            }

            _scriptFilesAsserts.AssertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(GetType().Name, testContext.ProjectConfig);

        }


        public void Release(TestContext testContext)
        {
            _dbVersionsValidTest.Release(testContext);
        }
    }
}
