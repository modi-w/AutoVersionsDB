﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB
{
    public class DevEnv_SyncDB_UI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_SyncDB_API _devEnv_SyncDB_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_SyncDB_UI(DevEnv_SyncDB_API devEnv_SyncDB_API, 
                                DBVersionsViewModel dbVersionsViewModel,
                                DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_SyncDB_API = devEnv_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsTestContext testContext = _devEnv_SyncDB_API.Arrange(testArgs) as DBVersionsTestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            var task = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_SyncDB_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessfully(_dbVersionsViewModel);
        }




        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_SyncDB_API.Release(testContext);
        }

    }
}