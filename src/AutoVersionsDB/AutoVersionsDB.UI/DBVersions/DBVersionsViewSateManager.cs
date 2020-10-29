﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewSateManager
    {
        private readonly DBVersionsViewModelData _dbVersionsViewModelData;
        private readonly DBVersionsControls _dbVersionsControls;
        private readonly NotificationsViewModel _notificationsViewModel;


        private ProjectConfigItem _projectConfig;
        private ScriptFilesState _scriptFilesState;


        public DBVersionsViewSateManager(DBVersionsViewModelData dbVersionsViewModelData,
                                            DBVersionsControls dbVersionsControls,
                                            NotificationsViewModel notificationsViewModel)
        {
            _dbVersionsViewModelData = dbVersionsViewModelData;
            _dbVersionsControls = dbVersionsControls;
            _notificationsViewModel = notificationsViewModel;
        }

        public void SetProjectConfig(ProjectConfigItem projectConfig)
        {
            _projectConfig = projectConfig;
        }



        public void ChangeViewState_AfterProcessComplete(ProcessTrace processResults)
        {
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
            _dbVersionsControls.LblProjectNameText = $"{_projectConfig.Id} - {_projectConfig.Description}";

            if (_scriptFilesState == null)
            {
                _dbVersionsViewModelData.IncrementalScriptFiles = new List<RuntimeScriptFileBase>();
                _dbVersionsViewModelData.RepeatableScriptFiles = new List<RuntimeScriptFileBase>();
                _dbVersionsViewModelData.DevDummyDataScriptFiles = new List<RuntimeScriptFileBase>();
            }
            else
            {
                _dbVersionsViewModelData.IncrementalScriptFiles = _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                _dbVersionsViewModelData.RepeatableScriptFiles = _scriptFilesState.RepeatableScriptFilesComparer.AllFileSystemScriptFiles.ToList();
                _dbVersionsViewModelData.DevDummyDataScriptFiles = _scriptFilesState.DevDummyDataScriptFilesComparer.AllFileSystemScriptFiles.ToList();
            }


            HideAllActionPanels();

            _dbVersionsControls.PnlRepeatableFilesVisible = true;

            if (_projectConfig.DevEnvironment)
            {
                _dbVersionsControls.PnlDevDummyDataFilesVisible = true;
            }




            switch (viewType)
            {
                case DBVersionsViewStateType.ReadyToRunSync:

                    _dbVersionsControls.PnlMainActionsVisible = true;
                    _dbVersionsControls.LblColorTargetState_SquareVisible = false;
                    _dbVersionsControls.LblColorTargetState_CaptionVisible = false;


                    if (_dbVersionsViewModelData.IncrementalScriptFiles.Count > 0)
                    {
                        _dbVersionsViewModelData.TargetStateScriptFileName = _dbVersionsViewModelData.IncrementalScriptFiles.Last().Filename;
                    }

                    _dbVersionsControls.GridToSelectTargetStateEnabled = false;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.ReadyToSyncToSpecificState:

                    AppendEmptyDBTargetStateToIncremental();

                    _dbVersionsControls.PnlSyncToSpecificStateVisible = true;
                    _dbVersionsControls.LblColorTargetState_SquareVisible = true;
                    _dbVersionsControls.LblColorTargetState_CaptionVisible = true;

                    _dbVersionsControls.PnlRepeatableFilesVisible = false;
                    _dbVersionsControls.PnlDevDummyDataFilesVisible = false;

                    _notificationsViewModel.SetAttentionMessage("Select the target Database State, and click on Apply");

                    _dbVersionsControls.GridToSelectTargetStateEnabled = true;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.MissingSystemTables:
                case DBVersionsViewStateType.HistoryExecutedFilesChanged:

                    _dbVersionsControls.PnlMissingSystemTablesVisible = true;
                    _dbVersionsControls.LblColorTargetState_SquareVisible = false;
                    _dbVersionsControls.LblColorTargetState_CaptionVisible = false;

                    if (_dbVersionsViewModelData.IncrementalScriptFiles.Count > 0)
                    {
                        _dbVersionsViewModelData.TargetStateScriptFileName = _dbVersionsViewModelData.IncrementalScriptFiles.Last().Filename;
                    }

                    _dbVersionsControls.GridToSelectTargetStateEnabled = false;

                    SetAllControlsEnableDisable(true);
                    break;

                case DBVersionsViewStateType.SetDBStateManually:

                    AppendEmptyDBTargetStateToIncremental();

                    _dbVersionsControls.PnlSetDBStateManuallyVisible = true;
                    _dbVersionsControls.LblColorTargetState_SquareVisible = true;
                    _dbVersionsControls.LblColorTargetState_CaptionVisible = true;

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

                    _dbVersionsControls.PnlRestoreDbErrorVisible = true;
                    _dbVersionsControls.LblColorTargetState_SquareVisible = false;
                    _dbVersionsControls.LblColorTargetState_CaptionVisible = false;
                    break;

                default:
                    break;
            }

            _dbVersionsControls.BtnRecreateDbFromScratchMainVisible = _projectConfig.DevEnvironment;
            _dbVersionsControls.BtnRecreateDbFromScratchSecondaryVisible = _projectConfig.DevEnvironment;
            _dbVersionsControls.BtnDeployVisible = _projectConfig.DevEnvironment;

        }


        private void SetAllControlsEnableDisable(bool isEnable)
        {
            _dbVersionsControls.PnlMainActionsEnabled = isEnable;
            _dbVersionsControls.PnlMissingSystemTablesEnabled = isEnable;
            _dbVersionsControls.PnlSetDBStateManuallyEnabled = isEnable;
            _dbVersionsControls.BtnRefreshEnable = isEnable;
            _dbVersionsControls.IncrementalScriptsGridEnabled= isEnable;
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
            _dbVersionsControls.PnlRestoreDbErrorVisible = false;
        }



        private void AppendEmptyDBTargetStateToIncremental()
        {
            List<RuntimeScriptFileBase> incScripts = _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.ToList();

            RuntimeScriptFileBase emptyDBTargetState = new EmptyDbStateRuntimeScriptFile();
            incScripts.Insert(0, emptyDBTargetState);

            _dbVersionsViewModelData.IncrementalScriptFiles = incScripts;
        }






    }
}