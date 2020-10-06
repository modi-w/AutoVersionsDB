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
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_New_Repeatable_API : ITestDefinition
    {
        private string _relFolder_Repeatable = "Repeatable";

        private string _scriptFullPath_Repeatable_scriptName1
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"rptScript_{ScriptName1}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, ScriptName1, scriptFilename);

                return script1FullPath;
            }
        }

        private readonly DBVersionsNotValidTest _dbVersionsNotValidTest;
        private readonly ProcessAsserts _processAsserts;



        public string ScriptName1 => "TestRptScript1";


        public DeliveryEnv_NotAllowMethods_New_Repeatable_API(DBVersionsNotValidTest dbVersionsNotValidTest,
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsNotValidTest = dbVersionsNotValidTest;
            _processAsserts = processAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _dbVersionsNotValidTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            if (File.Exists(_scriptFullPath_Repeatable_scriptName1))
            {
                File.Delete(_scriptFullPath_Repeatable_scriptName1);
            }


            return testContext;
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewRepeatableScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
        }


        public void Asserts(TestContext testContext)
        {
            //Comment: When we implement the  _dbAsserts.AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(), we should not call this method here.
            //          Because in this process we dont create a backup file.
            //          The above method is called on DBVersionsTest.Asserts()
            _dbVersionsNotValidTest.Asserts(testContext);

            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DeliveryEnvironment");
        }


        public void Release(TestContext testContext)
        {
            _dbVersionsNotValidTest.Release(testContext);
        }
    }
}
