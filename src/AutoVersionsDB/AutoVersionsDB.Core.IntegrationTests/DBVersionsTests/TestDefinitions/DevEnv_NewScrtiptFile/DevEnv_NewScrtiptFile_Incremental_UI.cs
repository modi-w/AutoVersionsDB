using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Incremental_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DevEnv_NewScrtiptFile_Incremental_API _devEnv_NewScrtiptFile_Incremental_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_NewScrtiptFile_Incremental_UI(DevEnv_NewScrtiptFile_Incremental_API devEnv_NewScrtiptFile_Incremental_API,
                                                    DBVersionsViewModel dbVersionsViewModel,
                                                    DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_NewScrtiptFile_Incremental_API = devEnv_NewScrtiptFile_Incremental_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_NewScrtiptFile_Incremental_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput += _dbVersionsViewModel_OnTextInputScript1;
            var task1 = _dbVersionsViewModel.CreateNewIncrementalScriptFileCommand.ExecuteWrapped();
            task1.Wait();
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInputScript1;

            _dbVersionsViewModel.OnTextInput += _dbVersionsViewModel_OnTextInputScript2;
            var task2 = _dbVersionsViewModel.CreateNewIncrementalScriptFileCommand.ExecuteWrapped();
            task2.Wait();
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInputScript2;
        }



        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_NewScrtiptFile_Incremental_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertNewIncScriptsFiles(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStatesForNewTwoScriptFiles(this.GetType().Name, testContext.ViewStatesHistory);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInputScript1;
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInputScript2;

            _devEnv_NewScrtiptFile_Incremental_API.Release(testContext);
        }







        private TextInputResults _dbVersionsViewModel_OnTextInputScript1(object sender, string instructionMessageText)
        {
            return new TextInputResults()
            {
                IsApply = true,
                ResultText = _devEnv_NewScrtiptFile_Incremental_API.ScriptName1
            };
        }

        private TextInputResults _dbVersionsViewModel_OnTextInputScript2(object sender, string instructionMessageText)
        {
            return new TextInputResults()
            {
                IsApply = true,
                ResultText = _devEnv_NewScrtiptFile_Incremental_API.ScriptName2
            };
        }
    }
}
