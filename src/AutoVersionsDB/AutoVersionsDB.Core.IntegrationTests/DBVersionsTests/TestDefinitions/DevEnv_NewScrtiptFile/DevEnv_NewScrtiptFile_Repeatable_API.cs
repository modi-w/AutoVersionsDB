using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Repeatable_API : TestDefinition
    {
        private string _relFolder_Repeatable = "Repeatable";

        private readonly ProjectConfigWithDBArrangeAndAssert _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public string ScriptName1 => "TestRptScript1";



        public DevEnv_NewScrtiptFile_Repeatable_API(ProjectConfigWithDBArrangeAndAssert dbVersionsTestHelper,
                                                        ScriptFilesAsserts scriptFilesAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ITestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);

            ClearScriptsFiles(testContext);

            return testContext;
        }


        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewRepeatableScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_Repeatable_scriptName1(testContext.ProjectConfig.DBConnectionInfo));
        }


        public override void Release(ITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);

            ClearScriptsFiles(testContext);
        }





        public string GetScriptFullPath_Repeatable_scriptName1(DBConnectionInfo dbConnectionInfo)
        {
            string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

            devScriptsBaseFolderPath = devScriptsBaseFolderPath.Replace("[DBType]", dbConnectionInfo.DBType);

            string scriptFilename = $"rptScript_{ScriptName1}.sql";
            string script1FullPath = Path.Combine(devScriptsBaseFolderPath, _relFolder_Repeatable, scriptFilename);

            return script1FullPath;
        }



        private void ClearScriptsFiles(ITestContext testContext)
        {
            if (File.Exists(GetScriptFullPath_Repeatable_scriptName1(testContext.ProjectConfig.DBConnectionInfo)))
            {
                File.Delete(GetScriptFullPath_Repeatable_scriptName1(testContext.ProjectConfig.DBConnectionInfo));
            }
        }


    }
}
