using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;

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

        public DevEnv_SyncDB_UI(DevEnv_SyncDB_API devEnv_SyncDB_API, DBVersionsViewModel dbVersionsViewModel)
        {
            _devEnv_SyncDB_API = devEnv_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
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

            AssertDBVersionsViewModelData(this.GetType().Name, _dbVersionsViewModel.DBVersionsViewModelData);

            AssertDBVersionsViewStateAsReadyToSync(this.GetType().Name, _dbVersionsViewModel.DBVersionsControls);

            AssertNotificationsViewModelAsReadyToSync(this.GetType().Name, _dbVersionsViewModel.NotificationsViewModel.NotificationsViewModelData);
        }

        public void AssertNotificationsViewModelAsReadyToSync(string testName, NotificationsViewModelData notificationsViewModelData)
        {
            AssertPropertyState(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), eNotificationStatus.CompleteSuccessfully.ToString());
            AssertPropertyState(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.ProcessStatusMessage, "The process complete successfully");
            AssertPropertyState(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), eStatusImageType.Succeed.ToString());

        }



        public void AssertDBVersionsViewStateAsReadyToSync(string testName, DBVersionsControls dbVersionsControls)
        {
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled), dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled), dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled), dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnDeployVisible), dbVersionsControls.BtnDeployVisible, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchMainVisible), dbVersionsControls.BtnRecreateDbFromScratchMainVisible, true);
           // AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible), dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.BtnRefreshEnable), dbVersionsControls.BtnRefreshEnable, true);
       //     AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnShowHistoricalBackupsEnabled), dbVersionsControls.BtnShowHistoricalBackupsEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.GridToSelectTargetStateEnabled), dbVersionsControls.GridToSelectTargetStateEnabled, false);
            AssertPropertyState(testName, nameof(dbVersionsControls.IncrementalScriptsGridEnabled), dbVersionsControls.IncrementalScriptsGridEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.LblColorTargetState_CaptionVisible), dbVersionsControls.LblColorTargetState_CaptionVisible, false);
            AssertPropertyState(testName, nameof(dbVersionsControls.LblColorTargetState_SquareVisible), dbVersionsControls.LblColorTargetState_SquareVisible, false);
            AssertPropertyState(testName, nameof(dbVersionsControls.LblProjectNameText), dbVersionsControls.LblProjectNameText, $"{IntegrationTestsConsts.TestProjectId} - ");
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlDevDummyDataFilesVisible), dbVersionsControls.PnlDevDummyDataFilesVisible, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlMainActionsEnabled), dbVersionsControls.PnlMainActionsEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlMainActionsVisible), dbVersionsControls.PnlMainActionsVisible, true);
          //  AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesEnabled), dbVersionsControls.PnlMissingSystemTablesEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesVisible), dbVersionsControls.PnlMissingSystemTablesVisible, false);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlRepeatableFilesVisible), dbVersionsControls.PnlRepeatableFilesVisible, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlRestoreDbErrorVisible), dbVersionsControls.PnlRestoreDbErrorVisible, false);
            //AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyEnabled), dbVersionsControls.PnlSetDBStateManuallyEnabled, true);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyVisible), dbVersionsControls.PnlSetDBStateManuallyVisible, false);
            AssertPropertyState(testName, nameof(dbVersionsControls.PnlSyncToSpecificStateVisible), dbVersionsControls.PnlSyncToSpecificStateVisible, false);
        }

        private void AssertPropertyState(string testName, string propertyName, bool actualValue, bool expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be '{expectedValue}', but was '{actualValue}'");
        }
        private void AssertPropertyState(string testName, string propertyName, string actualValue, string expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be '{expectedValue}', but was '{actualValue}'");
        }


        public void AssertDBVersionsViewModelData(string testName, DBVersionsViewModelData dbVersionsViewModelData)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.Equal);
        }

        private void AssertFilesListSize(string testName, string listName, IList<RuntimeScriptFileBase> runtimeFiles, int targetSize)
        {
            Assert.That(runtimeFiles.Count == targetSize, $"{testName} -> {listName} size should be {targetSize}, but was '{runtimeFiles.Count}'");

        }

        private void AssertFilesListHashState(string testName, IEnumerable<RuntimeScriptFileBase> runtimeFiles, HashDiffType targetHashState)
        {
            foreach (var file in runtimeFiles)
            {
                AssertFileHashState(testName, file, targetHashState);
            }
        }

        private void AssertFileHashState(string testName, RuntimeScriptFileBase runtimeFile, HashDiffType targetHashState)
        {
            Assert.That(runtimeFile.HashDiffType == targetHashState, $"{testName} -> The {runtimeFile.ScriptFileType.FileTypeCode} file: '{runtimeFile.Filename}' should be '{targetHashState}' hash state, but was '{runtimeFile.HashDiffType}'");

        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_SyncDB_API.Release(testContext);
        }

    }
}
