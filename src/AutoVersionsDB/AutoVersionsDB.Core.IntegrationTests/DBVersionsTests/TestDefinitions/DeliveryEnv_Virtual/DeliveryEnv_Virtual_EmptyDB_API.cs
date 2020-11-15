using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual
{
    public class DeliveryEnv_Virtual_EmptyDB_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_Virtual_EmptyDB_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                                ScriptFilesAsserts scriptFilesAsserts,
                                                DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SetDBStateByVirtualExecution(testContext.ProjectConfig.Id, IntegrationTestsConsts.TargetStateFile_MiddleState, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _dbAsserts.AssertDbInEmptyStateExceptSystemTables(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            _dbAsserts.AssertThatDbExecutedFilesAreInMiddleState(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            _scriptFilesAsserts.AssertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(GetType().Name, testContext.ProjectConfig);
        }


        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
