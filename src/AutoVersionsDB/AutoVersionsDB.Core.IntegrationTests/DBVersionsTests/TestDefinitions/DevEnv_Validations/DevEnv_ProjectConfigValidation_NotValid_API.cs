using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_ProjectConfigValidation_NotValid_API : TestDefinition<DBVersionsTestContext>
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProcessAsserts _processAsserts;

        public DevEnv_ProjectConfigValidation_NotValid_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
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
                DevEnvironment = true
            };

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);
            DBVersionsTestContext testContext = new DBVersionsTestContext(overrideTestArgs as ProjectConfigTestArgs, DBBackupFileType.None, ScriptFilesStateType.None);
            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);

            return testContext;
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateProjectConfig(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "DBType");
            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "DBName");
            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "DBBackupFolderPath");
            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "DevScriptsBaseFolder");
            _processAsserts.AssertContainError(this.GetType().Name, testContext.ProcessResults.Trace, "DeployArtifactFolderPath");
        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }

    }
}
