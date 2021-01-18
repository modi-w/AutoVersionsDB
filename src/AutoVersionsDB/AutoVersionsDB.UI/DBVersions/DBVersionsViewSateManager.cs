using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.Notifications;
using System.Collections.Generic;
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
                && string.CompareOrdinal(processResults.InstructionsMessageStepName, RestoreDatabaseStep.StepNameStr) == 0)
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
                _dbVersionsViewModelData.IncrementalScriptFiles = new List<RuntimeScriptFileBase>();
                _dbVersionsViewModelData.RepeatableScriptFiles = new List<RuntimeScriptFileBase>();
                _dbVersionsViewModelData.DevDummyDataScriptFiles = new List<RuntimeScriptFileBase>();
            }
            else
            {
                _dbVersionsViewModelData.IncrementalScriptFiles = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                _dbVersionsViewModelData.RepeatableScriptFiles = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();

                if (_dbVersionsViewModelData.ProjectConfig.DevEnvironment)
                {
                    _dbVersionsViewModelData.DevDummyDataScriptFiles = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                }
                else
                {
                    _dbVersionsViewModelData.DevDummyDataScriptFiles = new List<RuntimeScriptFileBase>();
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

                    _notificationsViewModel.SetAttentionMessage("Select the target Database State, and click on Apply");

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

                case DBVersionsViewStateType.SetDBStateManually:

                    AppendNoneDBTargetState();

                    _dbVersionsControls.PnlSetDBStateManuallyVisible = true;
                    _dbVersionsControls.LblColorTargetStateSquareVisible = true;
                    _dbVersionsControls.LblColorTargetStateCaptionVisible = true;

                    _dbVersionsControls.PnlRepeatableFilesVisible = false;
                    _dbVersionsControls.PnlDevDummyDataFilesVisible = false;

                    _notificationsViewModel.SetAttentionMessage("Select the Target Database State to virtually mark, and click on Apply");

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
            List<RuntimeScriptFileBase> incScripts = _dbVersionsViewModelData.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFileBase incNoneTargetState = new NoneRuntimeScriptFile();
            incScripts.Insert(0, incNoneTargetState);

            _dbVersionsViewModelData.IncrementalScriptFiles = incScripts;
            _dbVersionsViewModelData.TargetIncScriptFileName = NoneRuntimeScriptFile.TargetNoneScriptFileName;

            List<RuntimeScriptFileBase> rptScripts = _dbVersionsViewModelData.ScriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFileBase rptNoneTargetState = new NoneRuntimeScriptFile();
            rptScripts.Insert(0, rptNoneTargetState);

            _dbVersionsViewModelData.RepeatableScriptFiles = rptScripts;
            _dbVersionsViewModelData.TargetRptScriptFileName = NoneRuntimeScriptFile.TargetNoneScriptFileName;


            List<RuntimeScriptFileBase> dddScripts = _dbVersionsViewModelData.ScriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFileBase dddNoneTargetState = new NoneRuntimeScriptFile();
            dddScripts.Insert(0, dddNoneTargetState);

            _dbVersionsViewModelData.DevDummyDataScriptFiles = dddScripts;
            _dbVersionsViewModelData.TargetDDDScriptFileName = NoneRuntimeScriptFile.TargetNoneScriptFileName;
        }






    }
}
