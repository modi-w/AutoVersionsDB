using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Repeatable_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DevEnv_NewScrtiptFile_Repeatable_API _devEnv_NewScrtiptFile_Repeatable_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_NewScrtiptFile_Repeatable_UI(DevEnv_NewScrtiptFile_Repeatable_API devEnv_NewScrtiptFile_Repeatable_API,
                                                    DBVersionsViewModel dbVersionsViewModel,
                                                    DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_NewScrtiptFile_Repeatable_API = devEnv_NewScrtiptFile_Repeatable_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_NewScrtiptFile_Repeatable_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsUITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput += _dbVersionsViewModel_OnTextInput;
            var task1 = _dbVersionsViewModel.CreateNewRepeatableScriptFileCommand.ExecuteWrapped();
            task1.Wait();
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInput;
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_NewScrtiptFile_Repeatable_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertNewRptScriptFile(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistoryForNewSingleScriptFile(this.GetType().Name, testContext.ViewStatesHistory);
        }



        public override void Release(DBVersionsUITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInput;

            _devEnv_NewScrtiptFile_Repeatable_API.Release(testContext);
        }



        private void _dbVersionsViewModel_OnTextInput(object sender, OnTextInputEventsEventArgs e)
        {
            e.Results = new TextInputResults()
            {
                IsApply = true,
                ResultText = _devEnv_NewScrtiptFile_Repeatable_API.ScriptName1
            };
        }


    }
}
