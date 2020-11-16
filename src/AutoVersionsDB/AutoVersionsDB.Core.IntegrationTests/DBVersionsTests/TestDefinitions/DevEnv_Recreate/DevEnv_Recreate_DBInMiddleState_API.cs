using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate
{
    public class DevEnv_Recreate_DBInMiddleState_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Recreate_DBInMiddleState_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                            ScriptFilesAsserts scriptFilesAsserts,
                                            DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.RecreateDBFromScratch(testContext.ProjectConfig.Id, null, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(GetType().Name, testContext, true);

            _dbAsserts.AssertDbInFinalState_DevEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
        }



        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
