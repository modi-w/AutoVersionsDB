using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Restore
{
    public class DeliveryEnv_Restore_API : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_Restore_API(DBVersionsTestHelper dbVersionsTestHelper,
                                        DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _dbAsserts = dbAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.MiddleState, ScriptFilesStateType.ScriptError);
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _dbAsserts.AssertRestore(GetType().Name, testContext.ProjectConfig.DBConnectionInfo, testContext.DBBackupFileType, testContext.ProcessResults.Trace);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}
