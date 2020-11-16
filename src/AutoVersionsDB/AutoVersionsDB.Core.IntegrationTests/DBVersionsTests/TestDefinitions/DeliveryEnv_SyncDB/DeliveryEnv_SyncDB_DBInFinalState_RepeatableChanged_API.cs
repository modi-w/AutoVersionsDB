using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                                                        ScriptFilesAsserts scriptFilesAsserts,
                                                                        DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.RepeatableChanged);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(GetType().Name, testContext, true);

            _dbAsserts.AssertDbInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(GetType().Name, testContext.ProjectConfig);
        }


        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}
