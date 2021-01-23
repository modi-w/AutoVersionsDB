using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations
{
    public class ProjectConfigValidation_DeliveryEnv_NotValid_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProcessAsserts _processAsserts;

        public ProjectConfigValidation_DeliveryEnv_NotValid_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                                                ProcessAsserts processAsserts)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _processAsserts = processAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DevEnvironment = false,
            };

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);
            ITestContext testContext = new ProcessTestContext(overrideTestArgs as ProjectConfigTestArgs, DBBackupFileType.None, ScriptFilesStateType.None);
            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);

            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateProjectConfig(testContext.ProjectConfig, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            //_processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, DBTypeValidator.Name);
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, DBNameValidator.Name);
            //_processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, DBBackupFolderValidator.Name);
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, DeliveryArtifactFolderPathValidator.Name);
        }


        public override void Release(ITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }

    }
}
