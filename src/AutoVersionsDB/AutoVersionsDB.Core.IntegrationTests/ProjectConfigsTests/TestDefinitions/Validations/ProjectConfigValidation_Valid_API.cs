using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations
{
    public class ProjectConfigValidation_Valid_API : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;

        public ProjectConfigValidation_Valid_API(DBVersionsTestHelper dbVersionsTestHelper)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ValidateProjectConfig(testContext.ProjectConfig, null);
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
