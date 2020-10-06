using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy
{
    public class DevEnv_Deploy_API : ITestDefinition
    {
        private readonly DBVersionsValidTest _dbVersionsValidTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DevEnv_Deploy_API(DBVersionsValidTest dbVersionsValidTest,
                                        ScriptFilesAsserts scriptFilesAsserts,
                                        DBAsserts dbAsserts)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            ClearDeployFiles(testContext);

            return testContext;
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.Deploy(testContext.ProjectConfig.Id, null);
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsValidTest.Asserts(testContext);

            _scriptFilesAsserts.AssertThat_NewFileInTheDeployPath_And_ItsContentBeEqualToTheDevScriptsFolder(GetType().Name, testContext.ProjectConfig);
        }

        public void Release(TestContext testContext)
        {
            _dbVersionsValidTest.Release(testContext);
            ClearDeployFiles(testContext);
        }


        private static void ClearDeployFiles(TestContext testContext)
        {
            if (testContext != null
                && testContext.ProjectConfig != null)
            {
                string[] deployFiles = Directory.GetFiles(testContext.ProjectConfig.DeployArtifactFolderPath);

                foreach (string file in deployFiles)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}
