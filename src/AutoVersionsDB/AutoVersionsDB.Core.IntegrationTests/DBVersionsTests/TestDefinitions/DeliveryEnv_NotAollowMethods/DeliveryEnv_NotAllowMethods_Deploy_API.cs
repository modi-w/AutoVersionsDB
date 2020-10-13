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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_Deploy_API : TestDefinition<DBVersionsTestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ProcessAsserts _processAsserts;

        public DeliveryEnv_NotAllowMethods_Deploy_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                                    ProcessAsserts processAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);

            ClearDeployFiles(testContext as DBVersionsTestContext);

            return testContext;
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.Deploy(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, false);

            _processAsserts.AssertContainError(GetType().Name, testContext.ProcessResults.Trace, "DeliveryEnvironment");
        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);

            ClearDeployFiles(testContext);
        }


        private static void ClearDeployFiles(DBVersionsTestContext testContext)
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
