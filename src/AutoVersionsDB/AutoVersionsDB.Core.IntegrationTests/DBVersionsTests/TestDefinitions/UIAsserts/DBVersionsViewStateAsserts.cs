using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts
{
    public class DBVersionsViewStateAsserts
    {
        private readonly PropertiesAsserts _propertiesAsserts;

        public DBVersionsViewStateAsserts(PropertiesAsserts propertiesAsserts)
        {
            _propertiesAsserts = propertiesAsserts;
        }

        public void AssertDBVersionsViewStateCompleteSuccessfully(string testName, DBVersionsControls dbVersionsControls)
        {
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled), dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled), dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled), dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnDeployVisible), dbVersionsControls.BtnDeployVisible, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchMainVisible), dbVersionsControls.BtnRecreateDbFromScratchMainVisible, true);
            // AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible), dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.BtnRefreshEnable), dbVersionsControls.BtnRefreshEnable, true);
            //     AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnShowHistoricalBackupsEnabled), dbVersionsControls.BtnShowHistoricalBackupsEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.GridToSelectTargetStateEnabled), dbVersionsControls.GridToSelectTargetStateEnabled, false);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.IncrementalScriptsGridEnabled), dbVersionsControls.IncrementalScriptsGridEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.LblColorTargetState_CaptionVisible), dbVersionsControls.LblColorTargetState_CaptionVisible, false);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.LblColorTargetState_SquareVisible), dbVersionsControls.LblColorTargetState_SquareVisible, false);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.LblProjectNameText), dbVersionsControls.LblProjectNameText, $"{IntegrationTestsConsts.TestProjectId} - ");
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlDevDummyDataFilesVisible), dbVersionsControls.PnlDevDummyDataFilesVisible, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlMainActionsEnabled), dbVersionsControls.PnlMainActionsEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlMainActionsVisible), dbVersionsControls.PnlMainActionsVisible, true);
            //  AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesEnabled), dbVersionsControls.PnlMissingSystemTablesEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesVisible), dbVersionsControls.PnlMissingSystemTablesVisible, false);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlRepeatableFilesVisible), dbVersionsControls.PnlRepeatableFilesVisible, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlRestoreDbErrorVisible), dbVersionsControls.PnlRestoreDbErrorVisible, false);
            //AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyEnabled), dbVersionsControls.PnlSetDBStateManuallyEnabled, true);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyVisible), dbVersionsControls.PnlSetDBStateManuallyVisible, false);
            _propertiesAsserts.AssertPropertyState(testName, nameof(dbVersionsControls.PnlSyncToSpecificStateVisible), dbVersionsControls.PnlSyncToSpecificStateVisible, false);
        }


        public void AssertNotificationsViewModelCompleteSuccessfully(string testName, NotificationsViewModelData notificationsViewModelData)
        {
            _propertiesAsserts.AssertPropertyState(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), eNotificationStatus.CompleteSuccessfully.ToString());
            _propertiesAsserts.AssertPropertyState(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.ProcessStatusMessage, "The process complete successfully");
            _propertiesAsserts.AssertPropertyState(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), eStatusImageType.Succeed.ToString());
        }
    }
}
