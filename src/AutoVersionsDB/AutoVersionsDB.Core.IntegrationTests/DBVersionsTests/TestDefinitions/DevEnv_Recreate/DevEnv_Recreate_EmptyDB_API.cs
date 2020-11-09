using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate
{
    public class DevEnv_Recreate_EmptyDB_API : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Recreate_EmptyDB_API(DBVersionsTestHelper dbVersionsTestHelper,
                                            ScriptFilesAsserts scriptFilesAsserts,
                                            DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.RecreateDBFromScratch(testContext.ProjectConfig.Id,null, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _dbAsserts.AssertDbInFinalState_DevEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
