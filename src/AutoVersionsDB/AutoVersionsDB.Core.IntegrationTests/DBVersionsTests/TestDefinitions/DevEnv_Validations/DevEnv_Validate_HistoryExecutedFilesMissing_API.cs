using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_HistoryExecutedFilesMissing_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ProcessAsserts _processAsserts;

        public DevEnv_Validate_HistoryExecutedFilesMissing_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper, 
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _processAsserts = processAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.MissingFile);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateDBVersions(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(GetType().Name, testContext, false);

            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "HistoryExecutedFilesChanged");

        }

        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }
    }
}
