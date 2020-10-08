﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_API : TestDefinition<DBVersionsTestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInMiddleState_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                        ScriptFilesAsserts scriptFilesAsserts,
                                                        DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _dbAsserts.AssertDbInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            if (testContext.ScriptFilesStateType == ScriptFilesStateType.RepeatableChanged)
            {
                _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(GetType().Name, testContext.ProjectConfig);
            }
            else
            {
                _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
            }
        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}