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
    public class DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert,
                                                                            ScriptFilesAsserts scriptFilesAsserts,
                                                                            DBAsserts dbAsserts)
        {
            _projectConfigWithDBArrangeAndAssert = projectConfigWithDBArrangeAndAssert;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _projectConfigWithDBArrangeAndAssert.Arrange(testArgs, false, DBBackupFileType.MiddleState, ScriptFilesStateType.WithDevDummyDataFiles);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, true);

            _dbAsserts.AssertDbInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);

            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
        }


        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);
        }
    }
}
