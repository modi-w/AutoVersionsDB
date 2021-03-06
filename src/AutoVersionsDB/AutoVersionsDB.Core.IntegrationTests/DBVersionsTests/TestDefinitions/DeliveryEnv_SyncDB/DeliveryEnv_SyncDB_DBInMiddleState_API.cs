﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInMiddleState_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert,
                                                        ScriptFilesAsserts scriptFilesAsserts,
                                                        DBAsserts dbAsserts)
        {
            _projectConfigWithDBArrangeAndAssert = projectConfigWithDBArrangeAndAssert;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _projectConfigWithDBArrangeAndAssert.Arrange(testArgs, false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, true);

            _dbAsserts.AssertDBInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            if (testContext.ScriptFilesStateType == ScriptFilesStateType.RepeatableChanged)
            {
                _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDB_RunAgainAfterRepetableFilesChanged(GetType().Name, testContext.ProjectConfig);
            }
            else
            {
                _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDB_FinalState(GetType().Name, testContext.ProjectConfig);
            }
        }


        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);
        }
    }
}
