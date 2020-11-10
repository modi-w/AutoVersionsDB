using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
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
    public class DeliveryEnv_ProjectConfigValidation_NotValid_API : TestDefinition<ProjectConfigWithDBTestContext>
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_ProjectConfigValidation_NotValid_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                                                ProcessAsserts processAsserts)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DevEnvironment = false,
            };

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);
            ProjectConfigWithDBTestContext testContext = new ProjectConfigWithDBTestContext(overrideTestArgs as ProjectConfigTestArgs, DBBackupFileType.None, ScriptFilesStateType.None);
            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);

            return testContext;
        }

        public override void Act(ProjectConfigWithDBTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateProjectConfig(testContext.ProjectConfig, null);
        }


        public override void Asserts(ProjectConfigWithDBTestContext testContext)
        {
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DBType");
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DBName");
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DBBackupFolderPath");
            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DeliveryArtifactFolderPath");
        }


        public override void Release(ProjectConfigWithDBTestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }

    }
}
