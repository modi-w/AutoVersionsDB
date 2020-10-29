using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewModel : INotifyPropertyChanged
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly DBVersionsViewSateManager _dbVersionsViewSateManager;
        private readonly NotificationsViewModel _notificationsViewModel;

        public DBVersionsViewModelData DBVersionsViewModelData { get; }
        public DBVersionsControls DBVersionsControls { get; }


        private ViewRouter _viewRouter;
        public ViewRouter ViewRouter
        {
            get
            {
                return _viewRouter;
            }
            set
            {
                _viewRouter = value;
                SetRouteCommands();
            }
        }



        public event OnExceptionEventHandler OnException;
        public event OnConfirmEventHandler OnConfirm;
        public event OnTextInputEventHandler OnTextInput;



        public RelayCommand NavToChooseProjectCommand { get; private set; }
        public RelayCommand NavToEditProjectConfigCommand { get; private set; }


        public RelayCommand ShowHistoricalBackupsCommand { get; private set; }
        public RelayCommand SetDBToSpecificStateCommand { get; private set; }
        public RelayCommand CancelSyncToSpecificStateCommand { get; private set; }

        public RelayCommand SetDBStateManuallyViewStateCommand { get; private set; }
        public RelayCommand CancelSetDBStateManuallyCommand { get; private set; }


        public RelayCommand<string> SelectTargetStateScriptFileNameCommand { get; private set; }

        public RelayCommand OpenIncrementalScriptsFolderCommand { get; private set; }
        public RelayCommand OpenRepeatableScriptsFolderCommand { get; private set; }
        public RelayCommand OpenDevDummyDataScriptsFolderCommand { get; private set; }


        public RelayCommand CreateNewIncrementalScriptFileCommand { get; private set; }
        public RelayCommand CreateNewRepeatableScriptFileCommand { get; private set; }
        public RelayCommand CreateNewDevDummyDataScriptFileCommand { get; private set; }


        public RelayCommand RefreshAllCommand { get; private set; }
        public RelayCommand RunSyncCommand { get; private set; }
        public RelayCommand RecreateDbFromScratchCommand { get; private set; }
        public RelayCommand ApplySyncSpecificStateCommand { get; private set; }
        public RelayCommand DeployCommand { get; private set; }
        public RelayCommand RunSetDBStateManallyCommand { get; private set; }
        


        public DBVersionsViewModel(ProjectConfigsAPI projectConfigsAPI,
                                    DBVersionsAPI dbVersionsAPI,
                                    DBVersionsViewSateManager dbVersionsViewSateManager,
                                    NotificationsViewModel notificationsViewModel,
                                    DBVersionsViewModelData dbVersionsViewModelData,
                                    DBVersionsControls dbVersionsControls)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _dbVersionsAPI = dbVersionsAPI;
            _dbVersionsViewSateManager = dbVersionsViewSateManager;
            _notificationsViewModel = notificationsViewModel;

            DBVersionsViewModelData = dbVersionsViewModelData;
            DBVersionsControls = dbVersionsControls;

            ShowHistoricalBackupsCommand = new RelayCommand(ShowHistoricalBackups);

            SetDBToSpecificStateCommand = new RelayCommand(SetDBToSpecificState);
            CancelSyncToSpecificStateCommand = new RelayCommand(CancelSyncToSpecificState);

            SetDBStateManuallyViewStateCommand = new RelayCommand(SetDBStateManuallyViewState);
            CancelSetDBStateManuallyCommand = new RelayCommand(CancelSetDBStateManually);

            SelectTargetStateScriptFileNameCommand = new RelayCommand<string>(SelectTargetStateScriptFileName);

            OpenIncrementalScriptsFolderCommand = new RelayCommand(OpenIncrementalScriptsFolder);
            OpenRepeatableScriptsFolderCommand = new RelayCommand(OpenRepeatableScriptsFolder);
            OpenDevDummyDataScriptsFolderCommand = new RelayCommand(OpenDevDummyDataScriptsFolder);

            CreateNewIncrementalScriptFileCommand = new RelayCommand(CreateNewIncrementalScriptFile);
            CreateNewRepeatableScriptFileCommand = new RelayCommand(CreateNewRepeatableScriptFile);
            CreateNewDevDummyDataScriptFileCommand = new RelayCommand(CreateNewDevDummyDataScriptFile);


            RefreshAllCommand = new RelayCommand(RefreshAll);
            RunSyncCommand = new RelayCommand(RunSync);
            RecreateDbFromScratchCommand = new RelayCommand(RecreateDbFromScratch);
            ApplySyncSpecificStateCommand = new RelayCommand(ApplySyncSpecificState);
            DeployCommand = new RelayCommand(Deploy);
            RunSetDBStateManallyCommand = new RelayCommand(RunSetDBStateManally);

            SetToolTips();
        }

        private void SetToolTips()
        {
            DBVersionsControls.BtnRefreshTooltip = "Refresh";
            DBVersionsControls.BtnRunSyncTooltip = "Sync the db with the missing scripts";
            DBVersionsControls.BtnRecreateDbFromScratchMainTooltip = "Recreate DB From Scratch";
            DBVersionsControls.BtnDeployTooltip = "Create Deploy Package";
            DBVersionsControls.BtnSetDBToSpecificStateTooltip = "Set DB To Specific State";
            DBVersionsControls.BtnVirtualExecutionTooltip = "Set DB to specific state virtually. Use it if your DB is not empty but you never use our migration tool on it yet.";
            DBVersionsControls.BtnShowHistoricalBackupsTooltip = "Open the backup history folder.";
        }



        public void SetProjectConfig(string id)
        {
            DBVersionsViewModelData.ProjectConfig = _projectConfigsAPI.GetProjectConfigById(id);

            RefreshAll();
        }


        private void SetRouteCommands()
        {
            NavToChooseProjectCommand = new RelayCommand(_viewRouter.NavToChooseProject);
            NavToEditProjectConfigCommand = new RelayCommand(NavToEditProjectConfig);
        }

        private void NavToEditProjectConfig()
        {
            _viewRouter.NavToEditProjectConfig(DBVersionsViewModelData.ProjectConfig.Id);
        }


        private void ShowHistoricalBackups()
        {
            OsProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.BackupFolderPath);
        }

        private void SetDBToSpecificState()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToSyncToSpecificState);
        }
        private void CancelSyncToSpecificState()
        {
            _notificationsViewModel.WaitingForUser();
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToRunSync);
        }


        private void SetDBStateManuallyViewState()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.SetDBStateManually);
        }

        private void CancelSetDBStateManually()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.MissingSystemTables);
        }


        private void SelectTargetStateScriptFileName(string targetStateScriptFileName)
        {
            DBVersionsViewModelData.TargetStateScriptFileName = targetStateScriptFileName;
        }


        private void OpenIncrementalScriptsFolder()
        {
            OsProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.IncrementalScriptsFolderPath);
        }
        private void OpenRepeatableScriptsFolder()
        {
            OsProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.RepeatableScriptsFolderPath);
        }
        private void OpenDevDummyDataScriptsFolder()
        {
            OsProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.DevDummyDataScriptsFolderPath);
        }


        private void CreateNewIncrementalScriptFile()
        {
            TextInputResults results = fireOnTextInput("Create new script script file, insert the script name:");


            if (results.IsApply)
            {
                ProcessResults processResults = _dbVersionsAPI.CreateNewIncrementalScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, _notificationsViewModel.OnNotificationStateChanged);

                _notificationsViewModel.AfterComplete();
                _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }
        }
        private void CreateNewRepeatableScriptFile()
        {
            TextInputResults results = fireOnTextInput("Create new script script file, insert the script name:");

            if (results.IsApply)
            {
                ProcessResults processResults = _dbVersionsAPI.CreateNewRepeatableScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, _notificationsViewModel.OnNotificationStateChanged);

                _notificationsViewModel.AfterComplete();
                _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }
        }
        private void CreateNewDevDummyDataScriptFile()
        {
            TextInputResults results = fireOnTextInput("Create new script script file, insert the script name:");


            if (results.IsApply)
            {
                ProcessResults processResults = _dbVersionsAPI.CreateNewDevDummyDataScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, _notificationsViewModel.OnNotificationStateChanged);

                _notificationsViewModel.AfterComplete();
                _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    OsProcessUtils.StartOsProcess(newFileFullPath);
                }
            }
        }


        private void RefreshAll()
        {
            DBVersionsViewModelData.TargetStateScriptFileName = null;


            Task.Run(() =>
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);

                _notificationsViewModel.BeforeStartProcess();

                try
                {
                    if (RefreshScriptFilesState(true))
                    {

                        ProcessResults processResults = _dbVersionsAPI.ValidateDBVersions(DBVersionsViewModelData.ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);


                        if (processResults.Trace.HasError)
                        {
                            _notificationsViewModel.AfterComplete();

                            if (processResults.Trace.ContainErrorCode("SystemTables"))
                            {
                                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.MissingSystemTables);
                            }
                            else if (processResults.Trace.ContainErrorCode("HistoryExecutedFilesChanged"))
                            {
                                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.HistoryExecutedFilesChanged);
                            }
                            else
                            {
                                //   if (processResults.Trace.ContainErrorCode("DevScriptsBaseFolder")
                                //       || processResults.Trace.ContainErrorCode("ConnectionString"))
                                //   {
                                //       _scriptFilesState = null;
                                //   }
                                //   else
                                //   {
                                ////       RefreshScriptFilesState();
                                //   }

                                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToRunSync);
                            }
                        }
                        else
                        {
                            //RefreshScriptFilesState();

                            _notificationsViewModel.WaitingForUser();
                            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToRunSync);
                        }
                    }
                }
                catch (Exception ex)
                {
                    fireOnException(ex);
                }

            });
        }

        private void RunSync()
        {
            Task.Run(() =>
            {
                try
                {
                    _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                    _notificationsViewModel.BeforeStartProcess();

                    ProcessResults processResults = _dbVersionsAPI.SyncDB(DBVersionsViewModelData.ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);

                    RefreshScriptFilesState(false);

                    _notificationsViewModel.AfterComplete();
                    _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                }
                catch (Exception ex)
                {
                    fireOnException(ex);
                }
            });
        }


        private void RecreateDbFromScratch()
        {
            bool isAllowRun = fireOnConfirm($"This action will drop the Database and recreate it only by the scripts, you may loose Data. Are you sure?");

            if (isAllowRun)
            {
                Task.Run(() =>
                {
                    try
                    {
                        _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                        _notificationsViewModel.BeforeStartProcess();

                        ProcessResults processResults = _dbVersionsAPI.RecreateDBFromScratch(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, _notificationsViewModel.OnNotificationStateChanged);
                        RefreshScriptFilesState(false);

                        _notificationsViewModel.AfterComplete();
                        _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                    }
                    catch (Exception ex)
                    {
                        fireOnException(ex);
                    }

                });
            }
        }

        private void ApplySyncSpecificState()
        {
            if (CheckIsTargetStateHistory())
            {
                Task.Run(() =>
                {
                    try
                    {
                        _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                        _notificationsViewModel.BeforeStartProcess();

                        ProcessResults processResults = _dbVersionsAPI.SetDBToSpecificState(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, true, _notificationsViewModel.OnNotificationStateChanged);
                        RefreshScriptFilesState(false);

                        _notificationsViewModel.AfterComplete();
                        _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);

                    }
                    catch (Exception ex)
                    {
                        fireOnException(ex);
                    }

                });
            }
        }


        private void Deploy()
        {
            try
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                _notificationsViewModel.BeforeStartProcess();

                ProcessResults processResults = _dbVersionsAPI.Deploy(DBVersionsViewModelData.ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);
                RefreshScriptFilesState(false);

                _notificationsViewModel.AfterComplete();
                _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);

                OsProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.DeployArtifactFolderPath);
            }
            catch (Exception ex)
            {
                fireOnException(ex);
            }
        }

        private void RunSetDBStateManally()
        {
            Task.Run(() =>
            {
                try
                {
                    _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                    _notificationsViewModel.BeforeStartProcess();

                    ProcessResults processResults = _dbVersionsAPI.SetDBStateByVirtualExecution(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, _notificationsViewModel.OnNotificationStateChanged);
                    RefreshScriptFilesState(false);

                    _notificationsViewModel.AfterComplete();
                    _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                }
                catch (Exception ex)
                {
                    fireOnException(ex);
                }

            });
        }





        private bool RefreshScriptFilesState(bool showTrace)
        {
            ProcessResults processResults;

            if (showTrace)
            {
                processResults = _dbVersionsAPI.GetScriptFilesState(DBVersionsViewModelData.ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);
            }
            else
            {
                processResults = _dbVersionsAPI.GetScriptFilesState(DBVersionsViewModelData.ProjectConfig.Id, null);
            }

            if (processResults.Trace.HasError)
            {
                _notificationsViewModel.AfterComplete();

                if (processResults.Trace.ContainErrorCode("SystemTables"))
                {
                    _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.MissingSystemTables);
                }
                else
                {
                    _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                }
            }
            else
            {
                DBVersionsViewModelData.ScriptFilesState = processResults.Results as ScriptFilesState;

                if (showTrace)
                {
                    _notificationsViewModel.AfterComplete();

                    _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                }
            }

            return !processResults.Trace.HasError;
        }



        private bool CheckIsTargetStateHistory()
        {
            bool isAllowRun = true;

            if (_dbVersionsAPI.ValdiateTargetStateAlreadyExecuted(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, _notificationsViewModel.OnNotificationStateChanged).Trace.HasError)
            {
                isAllowRun = fireOnConfirm($"This action will drop the Database and recreate it only by the scripts, you may lose Data. Are you sure?");
            }

            return isAllowRun;
        }



        private void fireOnException(Exception ex)
        {
            if (OnException == null)
            {
                throw new Exception($"Bind method to 'OnException' event is mandatory");
            }

            OnException(this, ex.ToString());
        }

        private bool fireOnConfirm(string confirmMessage)
        {
            if (OnConfirm == null)
            {
                throw new Exception($"Bind method to 'OnConfirm' event is mandatory");
            }

            return OnConfirm(this, confirmMessage);
        }

        private TextInputResults fireOnTextInput(string instructionMessageText)
        {
            if (OnTextInput == null)
            {
                throw new Exception($"Bind method to 'OnTextInput' event is mandatory");
            }

            return OnTextInput(this, instructionMessageText);
        }



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

    }
}
