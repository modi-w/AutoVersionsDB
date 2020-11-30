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
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Incremental_API : TestDefinition
    {
        private string _relFolder_Incremental = "Incremental";

        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public string ScriptName1 => "TestIncScript1";
        public string ScriptName2 => "TestIncScript2";



        public DevEnv_NewScrtiptFile_Incremental_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert,
                                                        ScriptFilesAsserts scriptFilesAsserts)
        {
            _projectConfigWithDBArrangeAndAssert = projectConfigWithDBArrangeAndAssert;
            _scriptFilesAsserts = scriptFilesAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ITestContext testContext = _projectConfigWithDBArrangeAndAssert.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);

            ClearScriptsFiles(testContext);

            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewIncrementalScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewIncrementalScriptFile(testContext.ProjectConfig.Id, ScriptName2, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, true);

            _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_Incremental_scriptName1(testContext.ProjectConfig.DBConnectionInfo));

            //Comment: The second file is to check that the version changed in the filename for 2 scripts created in same day
            _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_Incremental_scriptName2(testContext.ProjectConfig.DBConnectionInfo));

        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);

            ClearScriptsFiles(testContext);
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



        private void ClearScriptsFiles(ITestContext testContext)
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
}
