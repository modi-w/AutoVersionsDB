﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual
{
    public class DevEnv_Virtual_EmptyDB_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DevEnv_Virtual_EmptyDB_API _devEnv_Virtual_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_Virtual_EmptyDB_UI(DevEnv_Virtual_EmptyDB_API devEnv_Virtual_API,
                                        DBVersionsViewModel dbVersionsViewModel,
                                        DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_Virtual_API = devEnv_Virtual_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_Virtual_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsUITestContext testContext)
        {
            _dbVersionsViewModel.DBVersionsViewModelData.TargetIncScriptFileName = IntegrationTestsConsts.MiddleStateTargetScripts.IncScriptFileName;
            var task = _dbVersionsViewModel.RunStateByVirtualExecutionCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_Virtual_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessForMiddleStateVirtual(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsUITestContext testContext)
        {
            _devEnv_Virtual_API.Release(testContext);
        }

    }
}
