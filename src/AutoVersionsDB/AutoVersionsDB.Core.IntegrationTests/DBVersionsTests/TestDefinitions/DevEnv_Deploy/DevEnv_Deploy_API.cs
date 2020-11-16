using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy
{
    public class DevEnv_Deploy_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Deploy_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                    ScriptFilesAsserts scriptFilesAsserts,
                                    DBAsserts dbAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ITestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);

            ClearDeployFiles(testContext);

            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.Deploy(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(GetType().Name, testContext, true);

            _scriptFilesAsserts.AssertThat_NewFileInTheDeployPath_And_ItsContentBeEqualToTheDevScriptsFolder(GetType().Name, testContext.ProjectConfig);
        }

        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
            ClearDeployFiles(testContext);
        }


        private static void ClearDeployFiles(ITestContext testContext)
        {
            if (testContext != null
                && testContext.ProjectConfig != null)
            {
                if (Directory.Exists(testContext.ProjectConfig.DeployArtifactFolderPath))
                {
                    Directory.Delete(testContext.ProjectConfig.DeployArtifactFolderPath, true);
                }
            }
        }
    }
}
