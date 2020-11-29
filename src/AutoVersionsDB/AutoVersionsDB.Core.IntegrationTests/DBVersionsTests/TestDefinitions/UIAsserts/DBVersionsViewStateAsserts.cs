using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
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

        public void AssertDBVersionsViewStateCompleteSuccessfully(string testName, DBVersionsControls dbVersionsControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled), dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled), dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled), dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnDeployVisible), dbVersionsControls.BtnDeployVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchMainVisible), dbVersionsControls.BtnRecreateDbFromScratchMainVisible, isDevEnv);
            // AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible), dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRefreshEnable), dbVersionsControls.BtnRefreshEnable, true);
            //     AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnShowHistoricalBackupsEnabled), dbVersionsControls.BtnShowHistoricalBackupsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.GridToSelectTargetStateEnabled), dbVersionsControls.GridToSelectTargetStateEnabled, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.IncrementalScriptsGridEnabled), dbVersionsControls.IncrementalScriptsGridEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_CaptionVisible), dbVersionsControls.LblColorTargetState_CaptionVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_SquareVisible), dbVersionsControls.LblColorTargetState_SquareVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblProjectNameText), dbVersionsControls.LblProjectNameText, $"{IntegrationTestsConsts.TestProjectId} - ");
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevDummyDataFilesVisible), dbVersionsControls.PnlDevDummyDataFilesVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsEnabled), dbVersionsControls.PnlMainActionsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsVisible), dbVersionsControls.PnlMainActionsVisible, true);
            //  AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesEnabled), dbVersionsControls.PnlMissingSystemTablesEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMissingSystemTablesVisible), dbVersionsControls.PnlMissingSystemTablesVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRepeatableFilesVisible), dbVersionsControls.PnlRepeatableFilesVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRestoreDbErrorVisible), dbVersionsControls.PnlRestoreDbErrorVisible, false);
            //AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyEnabled), dbVersionsControls.PnlSetDBStateManuallyEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyVisible), dbVersionsControls.PnlSetDBStateManuallyVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSyncToSpecificStateVisible), dbVersionsControls.PnlSyncToSpecificStateVisible, false);
        }


        public void AssertDBVersionsViewStateScriptsOrSystemTableError(string testName, DBVersionsControls dbVersionsControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled), dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled), dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled), dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnDeployVisible), dbVersionsControls.BtnDeployVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchMainVisible), dbVersionsControls.BtnRecreateDbFromScratchMainVisible, isDevEnv);
            // AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible), dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRefreshEnable), dbVersionsControls.BtnRefreshEnable, true);
            //     AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnShowHistoricalBackupsEnabled), dbVersionsControls.BtnShowHistoricalBackupsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.GridToSelectTargetStateEnabled), dbVersionsControls.GridToSelectTargetStateEnabled, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.IncrementalScriptsGridEnabled), dbVersionsControls.IncrementalScriptsGridEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_CaptionVisible), dbVersionsControls.LblColorTargetState_CaptionVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_SquareVisible), dbVersionsControls.LblColorTargetState_SquareVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblProjectNameText), dbVersionsControls.LblProjectNameText, $"{IntegrationTestsConsts.TestProjectId} - ");
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevDummyDataFilesVisible), dbVersionsControls.PnlDevDummyDataFilesVisible, isDevEnv);
            //_propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsEnabled), dbVersionsControls.PnlMainActionsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsVisible), dbVersionsControls.PnlMainActionsVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMissingSystemTablesEnabled), dbVersionsControls.PnlMissingSystemTablesEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMissingSystemTablesVisible), dbVersionsControls.PnlMissingSystemTablesVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRepeatableFilesVisible), dbVersionsControls.PnlRepeatableFilesVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRestoreDbErrorVisible), dbVersionsControls.PnlRestoreDbErrorVisible, false);
            //AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyEnabled), dbVersionsControls.PnlSetDBStateManuallyEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyVisible), dbVersionsControls.PnlSetDBStateManuallyVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSyncToSpecificStateVisible), dbVersionsControls.PnlSyncToSpecificStateVisible, false);
        }

        public void AssertDBVersionsViewStateProcessError(string testName, DBVersionsControls dbVersionsControls, bool isDevEnv)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled), dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled), dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled), dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnDeployVisible), dbVersionsControls.BtnDeployVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchMainVisible), dbVersionsControls.BtnRecreateDbFromScratchMainVisible, isDevEnv);
            // AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible), dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.BtnRefreshEnable), dbVersionsControls.BtnRefreshEnable, true);
            //     AssertUIControlPropertyState(testName, nameof(dbVersionsControls.BtnShowHistoricalBackupsEnabled), dbVersionsControls.BtnShowHistoricalBackupsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.GridToSelectTargetStateEnabled), dbVersionsControls.GridToSelectTargetStateEnabled, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.IncrementalScriptsGridEnabled), dbVersionsControls.IncrementalScriptsGridEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_CaptionVisible), dbVersionsControls.LblColorTargetState_CaptionVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblColorTargetState_SquareVisible), dbVersionsControls.LblColorTargetState_SquareVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.LblProjectNameText), dbVersionsControls.LblProjectNameText, $"{IntegrationTestsConsts.TestProjectId} - ");
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlDevDummyDataFilesVisible), dbVersionsControls.PnlDevDummyDataFilesVisible, isDevEnv);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsEnabled), dbVersionsControls.PnlMainActionsEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMainActionsVisible), dbVersionsControls.PnlMainActionsVisible, true);
            //  AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlMissingSystemTablesEnabled), dbVersionsControls.PnlMissingSystemTablesEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlMissingSystemTablesVisible), dbVersionsControls.PnlMissingSystemTablesVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRepeatableFilesVisible), dbVersionsControls.PnlRepeatableFilesVisible, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlRestoreDbErrorVisible), dbVersionsControls.PnlRestoreDbErrorVisible, false);
            //AssertUIControlPropertyState(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyEnabled), dbVersionsControls.PnlSetDBStateManuallyEnabled, true);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSetDBStateManuallyVisible), dbVersionsControls.PnlSetDBStateManuallyVisible, false);
            _propertiesAsserts.AssertProperty(testName, nameof(dbVersionsControls.PnlSyncToSpecificStateVisible), dbVersionsControls.PnlSyncToSpecificStateVisible, false);
        }





    }
}
