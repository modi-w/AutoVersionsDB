using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.Notifications;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewSateManager : IDBVersionsViewSateManager
    {
        private readonly DBVersionsViewModelData _dbVersionsViewModelData;
        private readonly DBVersionsControls _dbVersionsControls;
        private readonly INotificationsViewModel _notificationsViewModel;


        public DBVersionsViewStateType LastViewState { get; private set; }


        public DBVersionsViewSateManager(DBVersionsViewModelData dbVersionsViewModelData,
                                            DBVersionsControls dbVersionsControls,
                                            INotificationsViewModel notificationsViewModel)
        {
            _dbVersionsViewModelData = dbVersionsViewModelData;
            _dbVersionsControls = dbVersionsControls;
            _notificationsViewModel = notificationsViewModel;
        }


        public void ChangeViewStateAfterProcessComplete(ProcessTrace processResults)
        {
            processResults.ThrowIfNull(nameof(processResults));

            if (processResults.HasError
                && !string.IsNullOrWhiteSpace(processResults.InstructionsMessageStepName)
                && string.CompareOrdinal(processResults.InstructionsMessageStepName, RestoreDatabaseStep.Name) == 0)
            {
                ChangeViewState(DBVersionsViewStateType.RestoreDatabaseError);
            }
            else
            {
                ChangeViewState(DBVersionsViewStateType.ReadyToRunSync);
            }
        }

        public void ChangeViewState(DBVersionsViewStateType viewType)
        {
            LastViewState = viewType;

            _dbVersionsControls.LblProjectNameText = $"{_dbVersionsViewModelData.ProjectConfig.Id} - {_dbVersionsViewModelData.ProjectConfig.Description}";

            if (_dbVersionsViewModelData.ScriptFilesState == null)
            {
                _dbVersionsViewModelData.IncrementalScriptFiles = new List<RuntimeScriptFile>();
                _dbVersionsControls.LblIncNumOfExecutedText = "0";
                _dbVersionsControls.LblIncNumOfVirtualText = "0";
                _dbVersionsControls.LblIncNumOfChangedText = "0";

                _dbVersionsViewModelData.RepeatableScriptFiles = new List<RuntimeScriptFile>();
                _dbVersionsControls.LblRptNumOfExecutedText = "0";
                _dbVersionsControls.LblRptNumOfVirtualText = "0";
                _dbVersionsControls.LblRptNumOfChangedText = "0";

                _dbVersionsViewModelData.DevDummyDataScriptFiles = new List<RuntimeScriptFile>();
                _dbVersionsControls.LblDDDNumOfExecutedText = "0";
                _dbVersionsControls.LblDDDNumOfVirtualText = "0";
                _dbVersionsControls.LblDDDNumOfChangedText = "0";
            }
            else
            {
                _dbVersionsViewModelData.IncrementalScriptFiles = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                _dbVersionsControls.LblIncNumOfExecutedText = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.ExecutedFilesActual.Count.ToString(CultureInfo.InvariantCulture);
                _dbVersionsControls.LblIncNumOfVirtualText = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.ExecutedFilesVirtual.Count.ToString(CultureInfo.InvariantCulture);
                _dbVersionsControls.LblIncNumOfChangedText = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.ChangedFiles.Count.ToString(CultureInfo.InvariantCulture);

                _dbVersionsViewModelData.RepeatableScriptFiles = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                _dbVersionsControls.LblRptNumOfExecutedText = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.ExecutedFilesActual.Count.ToString(CultureInfo.InvariantCulture);
                _dbVersionsControls.LblRptNumOfVirtualText = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.ExecutedFilesVirtual.Count.ToString(CultureInfo.InvariantCulture);
                _dbVersionsControls.LblRptNumOfChangedText = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.ChangedFiles.Count.ToString(CultureInfo.InvariantCulture);

                if (_dbVersionsViewModelData.ProjectConfig.DevEnvironment)
                {
                    _dbVersionsViewModelData.DevDummyDataScriptFiles = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                    _dbVersionsControls.LblDDDNumOfExecutedText = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.ExecutedFilesActual.Count.ToString(CultureInfo.InvariantCulture);
                    _dbVersionsControls.LblDDDNumOfVirtualText = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.ExecutedFilesVirtual.Count.ToString(CultureInfo.InvariantCulture);
                    _dbVersionsControls.LblDDDNumOfChangedText = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.ChangedFiles.Count.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    _dbVersionsViewModelData.DevDummyDataScriptFiles = new List<RuntimeScriptFile>();
                }
            }


            HideAllActionPanels();




            switch (viewType)
            {
                case DBVersionsViewStateType.ReadyToRunSync:

                    _dbVersionsControls.PnlMainActionsVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = false;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = false;


                    //if (_dbVersionsViewModelData.IncrementalScriptFiles.Count > 0)
                    //{
                    //    _dbVersionsViewModelData.TargetIncScriptFileName = _dbVersionsViewModelData.IncrementalScriptFiles.Last().Filename;
                    //}

                    _dbVersionsControls.GridToSelectTargetStateEnabled = false;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.ReadyToSyncToSpecificState:

                    AppendNoneDBTargetState();

                    _dbVersionsControls.PnlSyncToSpecificStateVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = true;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = true;

                    _dbVersionsControls.PnlRepeatableFilesVisible = false;
                    _dbVersionsControls.PnlDevDummyDataFilesVisible = false;

                    _notificationsViewModel.SetAttentionMessage(UITextResources.SyncToSpecificStateInstructions);

                    _dbVersionsControls.GridToSelectTargetStateEnabled = true;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.MissingSystemTables:
                case DBVersionsViewStateType.HistoryExecutedFilesChanged:

                    _dbVersionsControls.PnlMissingSystemTablesVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = false;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = false;

                    if (_dbVersionsViewModelData.IncrementalScriptFiles.Count > 0)
                    {
                        _dbVersionsViewModelData.TargetIncScriptFileName = _dbVersionsViewModelData.IncrementalScriptFiles.Last().Filename;
                    }

                    _dbVersionsControls.GridToSelectTargetStateEnabled = false;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.SetVirtual:

                    AppendNoneDBTargetState();

                    _dbVersionsControls.PnlSetDBStateManuallyVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = true;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = true;

                    _dbVersionsControls.PnlRepeatableFilesVisible = false;
                    _dbVersionsControls.PnlDevDummyDataFilesVisible = false;

                    _notificationsViewModel.SetAttentionMessage(UITextResources.SetVirtualInstructions);

                    _dbVersionsControls.GridToSelectTargetStateEnabled = true;

                    SetAllControlsEnableDisable(true);

                    break;

                case DBVersionsViewStateType.InProcess:

                    SetAllControlsEnableDisable(false);

                    break;

                case DBVersionsViewStateType.RestoreDatabaseError:

                    SetAllControlsEnableDisable(false);

                    _dbVersionsControls.BtnShowHistoricalBackupsEnabled = true;

                    _dbVersionsControls.PnlRestoreDBErrorVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = false;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = false;
                    break;

                default:
                    break;
            }

            _dbVersionsControls.PnlRepeatableFilesVisible = true;
            _dbVersionsControls.PnlDevDummyDataFilesVisible = _dbVersionsViewModelData.ProjectConfig.DevEnvironment;

            _dbVersionsControls.BtnRecreateDBFromScratchMainVisible = _dbVersionsViewModelData.ProjectConfig.DevEnvironment;
            _dbVersionsControls.BtnRecreateDBFromScratchSecondaryVisible = _dbVersionsViewModelData.ProjectConfig.DevEnvironment;
            _dbVersionsControls.BtnDeployVisible = _dbVersionsViewModelData.ProjectConfig.DevEnvironment;

            _dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled = _dbVersionsViewModelData.ProjectConfig.DevEnvironment;


        }


        private void SetAllControlsEnableDisable(bool isEnable)
        {
            _dbVersionsControls.PnlMainActionsEnabled = isEnable;
            _dbVersionsControls.PnlMissingSystemTablesEnabled = isEnable;
            _dbVersionsControls.PnlSetDBStateManuallyEnabled = isEnable;
            _dbVersionsControls.BtnRefreshEnable = isEnable;
            _dbVersionsControls.IncrementalScriptsGridEnabled = isEnable;
            _dbVersionsControls.BtnShowHistoricalBackupsEnabled = isEnable;
            _dbVersionsControls.BtnCreateNewIncrementalScriptFileEnabled = isEnable;
            _dbVersionsControls.BtnCreateNewRepeatableScriptFileEnabled = isEnable;
            _dbVersionsControls.BtnCreateNewDevDummyDataScriptFileEnabled = isEnable;
        }


        private void HideAllActionPanels()
        {
            _dbVersionsControls.PnlMainActionsVisible = false;
            _dbVersionsControls.PnlSyncToSpecificStateVisible = false;
            _dbVersionsControls.PnlMissingSystemTablesVisible = false;
            _dbVersionsControls.PnlSetDBStateManuallyVisible = false;
            _dbVersionsControls.PnlRestoreDBErrorVisible = false;
        }



        private void AppendNoneDBTargetState()
        {
            List<RuntimeScriptFile> incScripts = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFile incNoneTargetState = 
                new RuntimeScriptFile(new IncrementalScriptFileType(),"",RuntimeScriptFile.TargetNoneScriptFileName,0);
            incScripts.Insert(0, incNoneTargetState);

            _dbVersionsViewModelData.IncrementalScriptFiles = incScripts;
            _dbVersionsViewModelData.TargetIncScriptFileName = RuntimeScriptFile.TargetNoneScriptFileName;

            List<RuntimeScriptFile> rptScripts = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFile rptNoneTargetState =
                new RuntimeScriptFile(new RepeatableScriptFileType(), "", RuntimeScriptFile.TargetNoneScriptFileName, 0);
            rptScripts.Insert(0, rptNoneTargetState);

            _dbVersionsViewModelData.RepeatableScriptFiles = rptScripts;
            _dbVersionsViewModelData.TargetRptScriptFileName = RuntimeScriptFile.TargetNoneScriptFileName;


            if (_dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer!= null)
            {
                List<RuntimeScriptFile> dddScripts = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();

                RuntimeScriptFile dddNoneTargetState =
                    new RuntimeScriptFile(new DevDummyDataScriptFileType(), "", RuntimeScriptFile.TargetNoneScriptFileName, 0);
                dddScripts.Insert(0, dddNoneTargetState);

                _dbVersionsViewModelData.DevDummyDataScriptFiles = dddScripts;
                _dbVersionsViewModelData.TargetDDDScriptFileName = RuntimeScriptFile.TargetNoneScriptFileName;
            }
        }






    }
}
