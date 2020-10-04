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
    public class DevEnv_NewScrtiptFile_Incremental_API : ITestDefinition
    {
        private string _relFolder_Incremental = "Incremental";

        private readonly DBVersionsValidTest _dbVersionsValidTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public string ScriptName1 => "TestIncScript1";
        public string ScriptName2 => "TestIncScript2";



        public DevEnv_NewScrtiptFile_Incremental_API(DBVersionsValidTest dbVersionsValidTest,
                                                        ScriptFilesAsserts scriptFilesAsserts)
        {
            _dbVersionsValidTest = dbVersionsValidTest;
            _scriptFilesAsserts = scriptFilesAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _dbVersionsValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            if (File.Exists(GetScriptFullPath_Incremental_scriptName1(projectConfig.DBConnectionInfo)))
            {
                File.Delete(GetScriptFullPath_Incremental_scriptName1(projectConfig.DBConnectionInfo));
            }
            if (File.Exists(GetScriptFullPath_Incremental_scriptName2(projectConfig.DBConnectionInfo)))
            {
                File.Delete(GetScriptFullPath_Incremental_scriptName2(projectConfig.DBConnectionInfo));
            }


            return testContext;
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
            testContext.ProcessResults = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(testContext.ProjectConfig.Id, ScriptName2, null);
        }


        public void Asserts(TestContext testContext)
        {
            try
            {
                _dbVersionsValidTest.Asserts(testContext);

                _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_Incremental_scriptName1(testContext.ProjectConfig.DBConnectionInfo));

                //Comment: The second file is to check the change version in the filename for 2 scripts created in same day
                _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_Incremental_scriptName2(testContext.ProjectConfig.DBConnectionInfo));
            }
            finally
            {
                if (File.Exists(GetScriptFullPath_Incremental_scriptName1(testContext.ProjectConfig.DBConnectionInfo)))
                {
                    File.Delete(GetScriptFullPath_Incremental_scriptName1(testContext.ProjectConfig.DBConnectionInfo));
                }
                if (File.Exists(GetScriptFullPath_Incremental_scriptName2(testContext.ProjectConfig.DBConnectionInfo)))
                {
                    File.Delete(GetScriptFullPath_Incremental_scriptName2(testContext.ProjectConfig.DBConnectionInfo));
                }

            }

        }



        public string GetScriptFullPath_Incremental_scriptName1(DBConnectionInfo dbConnectionInfo)
        {
            string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

            devScriptsBaseFolderPath = devScriptsBaseFolderPath.Replace("[DBType]", dbConnectionInfo.DBType);

            string scriptFilename = $"incScript_{DateTime.Today:yyyy-MM-dd}.001_{ScriptName1}.sql";
            string script1FullPath = Path.Combine(devScriptsBaseFolderPath, _relFolder_Incremental, scriptFilename);

            return script1FullPath;
        }
        public string GetScriptFullPath_Incremental_scriptName2(DBConnectionInfo dbConnectionInfo)
        {
            string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

            devScriptsBaseFolderPath = devScriptsBaseFolderPath.Replace("[DBType]", dbConnectionInfo.DBType);

            string scriptFilename = $"incScript_{DateTime.Today:yyyy-MM-dd}.002_{ScriptName2}.sql";
            string script1FullPath = Path.Combine(devScriptsBaseFolderPath, _relFolder_Incremental, scriptFilename);

            return script1FullPath;
        }

    }
}
