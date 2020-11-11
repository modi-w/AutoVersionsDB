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
    public class DevEnv_NewScrtiptFile_DevDummyData_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DevEnv_NewScrtiptFile_DevDummyData_API _devEnv_NewScrtiptFile_DevDummyData_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_NewScrtiptFile_DevDummyData_UI(DevEnv_NewScrtiptFile_DevDummyData_API devEnv_NewScrtiptFile_DevDummyData_API,
                                                    DBVersionsViewModel dbVersionsViewModel,
                                                    DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_NewScrtiptFile_DevDummyData_API = devEnv_NewScrtiptFile_DevDummyData_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_NewScrtiptFile_DevDummyData_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput += _dbVersionsViewModel_OnTextInput;
            var task1 = _dbVersionsViewModel.CreateNewDevDummyDataScriptFileCommand.ExecuteWrapped();
            task1.Wait();
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInput;
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_NewScrtiptFile_DevDummyData_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertNewDDDScriptFile(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistoryForNewSingleScriptFile(this.GetType().Name, testContext.ViewStatesHistory);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.OnTextInput -= _dbVersionsViewModel_OnTextInput;

            _devEnv_NewScrtiptFile_DevDummyData_API.Release(testContext);
        }


        private TextInputResults _dbVersionsViewModel_OnTextInput(object sender, string instructionMessageText)
        {
            return new TextInputResults()
            {
                IsApply = true,
                ResultText = _devEnv_NewScrtiptFile_DevDummyData_API.ScriptName1
            };
        }
    }
}
