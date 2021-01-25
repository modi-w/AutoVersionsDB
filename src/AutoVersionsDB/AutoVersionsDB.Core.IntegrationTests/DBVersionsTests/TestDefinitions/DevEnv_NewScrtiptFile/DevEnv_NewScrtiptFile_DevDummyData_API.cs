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
    public class DevEnv_NewScrtiptFile_DevDummyData_API : TestDefinition
    {
        private string _relFolder_DevDummyData = "DevDummyData";

        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public string ScriptName1 => "TestDDDScript1";



        public DevEnv_NewScrtiptFile_DevDummyData_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert,
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
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewDevDummyDataScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, true);

            _scriptFilesAsserts.AssertScriptFileExsit(this.GetType().Name, GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo));

        }


        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);

            ClearScriptsFiles(testContext);
        }





        public string GetScriptFullPath_DevDummyData_scriptName1(DBConnectionInfo dbConnectionInfo)
        {
            string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

            devScriptsBaseFolderPath = devScriptsBaseFolderPath.Replace("[DBType]", dbConnectionInfo.DBType);

            string scriptFilename = $"dddScript_0003_{ScriptName1}.sql";
            string script1FullPath = Path.Combine(devScriptsBaseFolderPath, _relFolder_DevDummyData, scriptFilename);

            return script1FullPath;
        }



        private void ClearScriptsFiles(ITestContext testContext)
        {
            if (File.Exists(GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo)))
            {
                File.Delete(GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo));
            }
        }
    }
}
