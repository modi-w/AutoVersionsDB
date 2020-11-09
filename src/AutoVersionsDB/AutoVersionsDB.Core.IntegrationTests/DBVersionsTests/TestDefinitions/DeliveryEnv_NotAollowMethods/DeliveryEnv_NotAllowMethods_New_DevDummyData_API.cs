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


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_New_DevDummyData_API : TestDefinition<DBVersionsAPITestContext>
    {
        private string _relFolder_DevDummyData = "DevDummyData";

        private string _scriptFullPath_DevDummyData_scriptName1
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"dddScript_{ScriptName1}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, ScriptName1, scriptFilename);

                return script1FullPath;
            }
        }

        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ProcessAsserts _processAsserts;



        public string ScriptName1 => "TestDddScript1";


        public DeliveryEnv_NotAllowMethods_New_DevDummyData_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);

            if (File.Exists(_scriptFullPath_DevDummyData_scriptName1))
            {
                File.Delete(_scriptFullPath_DevDummyData_scriptName1);
            }


            return testContext;
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.CreateNewDevDummyDataScriptFile(testContext.ProjectConfig.Id, ScriptName1, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DeliveryEnvironment");
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);

            if (File.Exists(_scriptFullPath_DevDummyData_scriptName1))
            {
                File.Delete(_scriptFullPath_DevDummyData_scriptName1);
            }

        }
    }
}
