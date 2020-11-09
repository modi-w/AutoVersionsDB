using AutoVersionsDB.Core.ConfigProjects;


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                                        ScriptFilesAsserts scriptFilesAsserts,
                                                                        DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.RepeatableChanged);
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _dbAsserts.AssertDbInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(GetType().Name, testContext.ProjectConfig);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}
