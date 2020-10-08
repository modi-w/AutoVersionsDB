﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API : TestDefinition<DBVersionsTestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SetDBToSpecificState(testContext.ProjectConfig.Id, IntegrationTestsConsts.TargetStateFile_FinalState, false, null);
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DeliveryEnvironment");

        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}
