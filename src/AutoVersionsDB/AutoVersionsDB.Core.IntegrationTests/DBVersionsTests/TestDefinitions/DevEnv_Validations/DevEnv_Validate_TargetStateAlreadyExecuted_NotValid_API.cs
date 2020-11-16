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
    public class DevEnv_Validate_TargetStateAlreadyExecuted_NotValid_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;
        private readonly ProcessAsserts _processAsserts;

        public DevEnv_Validate_TargetStateAlreadyExecuted_NotValid_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert,
                                                                    ProcessAsserts processAsserts)
        {
            _projectConfigWithDBArrangeAndAssert = projectConfigWithDBArrangeAndAssert;
            _processAsserts = processAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            return _projectConfigWithDBArrangeAndAssert.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValdiateTargetStateAlreadyExecuted(testContext.ProjectConfig.Id, IntegrationTestsConsts.TargetStateFile_MiddleState, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, false);

            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "TargetScriptFileAlreadyExecuted");

        }



        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);
        }
    }
}
