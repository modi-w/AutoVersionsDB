using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_DevDummyData_API : ITestDefinition
    {
        private string _relFolder_DevDummyData = "DevDummyData";

        private readonly DBVersionsValidTest _dbVersionsValidTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public string ScriptName1 => "TestDDDScript1";



        public DevEnv_NewScrtiptFile_DevDummyData_API(DBVersionsValidTest dbVersionsValidTest,
                                                        ScriptFilesAsserts scriptFilesAsserts)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
            _scriptFilesAsserts = scriptFilesAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            ClearScriptsFiles(testContext);

            return testContext;
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewDevDummyDataScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsValidTest.Asserts(testContext);

            _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo));

        }


        public void Release(TestContext testContext)
        {
            _dbVersionsValidTest.Release(testContext);

            ClearScriptsFiles(testContext);
        }





        public string GetScriptFullPath_DevDummyData_scriptName1(DBConnectionInfo dbConnectionInfo)
        {
            string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

            devScriptsBaseFolderPath = devScriptsBaseFolderPath.Replace("[DBType]", dbConnectionInfo.DBType);

            string scriptFilename = $"dddScript_{ScriptName1}.sql";
            string script1FullPath = Path.Combine(devScriptsBaseFolderPath, _relFolder_DevDummyData, scriptFilename);

            return script1FullPath;
        }



        private void ClearScriptsFiles(TestContext testContext)
        {
            if (File.Exists(GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo)))
            {
                File.Delete(GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo));
            }
        }
    }
}
