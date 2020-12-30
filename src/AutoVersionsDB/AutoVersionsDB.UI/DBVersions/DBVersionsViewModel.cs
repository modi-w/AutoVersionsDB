using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.Notifications;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewModel : INotifyPropertyChanged
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IDBVersionsViewSateManager _dbVersionsViewSateManager;
        private readonly OsProcessUtils _osProcessUtils;

        public INotificationsViewModel NotificationsViewModel { get; }

        public DBVersionsViewModelData DBVersionsViewModelData { get; }
        public DBVersionsControls DBVersionsControls { get; }


        private ViewRouter _viewRouter;
        public ViewRouter ViewRouter
        {
            get => _viewRouter;
            set
            {
                _viewRouter = value;
                SetRouteCommands();
            }
        }



        public event EventHandler<OnTextInputEventsEventArgs> OnTextInput;



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
        public RelayCommand RecreateDBFromScratchCommand { get; private set; }
        public RelayCommand ApplySyncSpecificStateCommand { get; private set; }
        public RelayCommand DeployCommand { get; private set; }
        public RelayCommand RunSetDBStateManallyCommand { get; private set; }



        public DBVersionsViewModel(ProjectConfigsAPI projectConfigsAPI,
                                    DBVersionsAPI dbVersionsAPI,
                                    IDBVersionsViewSateManager dbVersionsViewSateManager,
                                    INotificationsViewModel notificationsViewModel,
                                    DBVersionsViewModelData dbVersionsViewModelData,
                                    DBVersionsControls dbVersionsControls,
                                    OsProcessUtils osProcessUtils)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _dbVersionsAPI = dbVersionsAPI;
            _dbVersionsViewSateManager = dbVersionsViewSateManager;
            _osProcessUtils = osProcessUtils;

            NotificationsViewModel = notificationsViewModel;

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
            RecreateDBFromScratchCommand = new RelayCommand(RecreateDBFromScratch);
            ApplySyncSpecificStateCommand = new RelayCommand(ApplySyncSpecificState);
            DeployCommand = new RelayCommand(Deploy);
            RunSetDBStateManallyCommand = new RelayCommand(RunSetDBStateManally);

            SetToolTips();
        }

        private void SetToolTips()
        {
            DBVersionsControls.BtnRefreshTooltip = "Refresh";
            DBVersionsControls.BtnRunSyncTooltip = "Sync the db with the missing scripts";
            DBVersionsControls.BtnRecreateDBFromScratchMainTooltip = "Recreate DB From Scratch";
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
            _osProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.BackupFolderPath);
        }

        private void SetDBToSpecificState()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToSyncToSpecificState);
        }
        private void CancelSyncToSpecificState()
        {
            NotificationsViewModel.WaitingForUser();
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
            _osProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.IncrementalScriptsFolderPath);
        }
        private void OpenRepeatableScriptsFolder()
        {
            _osProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.RepeatableScriptsFolderPath);
        }
        private void OpenDevDummyDataScriptsFolder()
        {
            _osProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.DevDummyDataScriptsFolderPath);
        }


        private void CreateNewIncrementalScriptFile()
        {
            TextInputResults results = FireOnTextInput("Create new script script file, insert the script name:");

            if (results.IsApply)
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);

                ProcessResults processResults = _dbVersionsAPI.CreateNewIncrementalScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, NotificationsViewModel.OnNotificationStateChanged);

                NotificationsViewModel.AfterComplete(processResults);
                _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    _osProcessUtils.StartOsProcess(newFileFullPath);
                }
            }
        }
        private void CreateNewRepeatableScriptFile()
        {
            TextInputResults results = FireOnTextInput("Create new script script file, insert the script name:");

            if (results.IsApply)
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);

                ProcessResults processResults = _dbVersionsAPI.CreateNewRepeatableScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, NotificationsViewModel.OnNotificationStateChanged);

                NotificationsViewModel.AfterComplete(processResults);
                _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    _osProcessUtils.StartOsProcess(newFileFullPath);
                }

            }
        }

        private void CreateNewDevDummyDataScriptFile()
        {
            TextInputResults results = FireOnTextInput("Create new script script file, insert the script name:");


            if (results.IsApply)
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);

                ProcessResults processResults = _dbVersionsAPI.CreateNewDevDummyDataScriptFile(DBVersionsViewModelData.ProjectConfig.Id, results.ResultText, NotificationsViewModel.OnNotificationStateChanged);

                NotificationsViewModel.AfterComplete(processResults);
                _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFileFullPath = (string)processResults.Results;

                    RefreshAll();

                    _osProcessUtils.StartOsProcess(newFileFullPath);
                }
            }
        }


        private void RefreshAll()
        {
            //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> Start");

            DBVersionsViewModelData.TargetStateScriptFileName = null;

            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
            //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> After change to InProcess");

            NotificationsViewModel.BeforeStartProcess();
            //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> after call to AfterBeforeStartProcess()");

            if (RefreshScriptFilesState(true))
            {
                //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> after call to RefreshScriptFilesState(true)");

                ProcessResults processResults = _dbVersionsAPI.ValidateDBVersions(DBVersionsViewModelData.ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);
                //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> after call to ValidateDBVersions()");


                if (processResults.Trace.HasError)
                {
                    NotificationsViewModel.AfterComplete(processResults);
                    //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> HasError -> after call to  AfterComplete()");

                    if (processResults.Trace.ContainErrorCode("SystemTables"))
                    {
                        _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.MissingSystemTables);
                        //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> HasError -> after call to  MissingSystemTables()");
                    }
                    else if (processResults.Trace.ContainErrorCode("HistoryExecutedFilesChanged"))
                    {
                        _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.HistoryExecutedFilesChanged);
                        //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> HasError -> after call to  HistoryExecutedFilesChanged()");
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
                        //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> HasError -> after call to  ReadyToRunSync()");
                    }
                }
                else
                {
                    //RefreshScriptFilesState();
                    NotificationsViewModel.AfterComplete(processResults);
                    //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> No Error -> after call to  AfterComplete()");


                    NotificationsViewModel.WaitingForUser();
                    //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> No Error -> after call to  WaitingForUser()");

                    _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.ReadyToRunSync);
                    //Console.WriteLine("DBVersionsViewModel.RefreshAll() -> No Error -> after call to  ReadyToRunSync()");
                }
            }

        }

        private void RunSync()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
            NotificationsViewModel.BeforeStartProcess();

            ProcessResults processResults = _dbVersionsAPI.SyncDB(DBVersionsViewModelData.ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);

            RefreshScriptFilesState(false);

            NotificationsViewModel.AfterComplete(processResults);
            _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);
        }


        private void RecreateDBFromScratch()
        {
            bool isAllowRun = UIGeneralEvents.FireOnConfirm(this, $"This action will drop the Database and recreate it only by the scripts, you may loose Data. Are you sure?");

            if (isAllowRun)
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                NotificationsViewModel.BeforeStartProcess();

                ProcessResults processResults = _dbVersionsAPI.RecreateDBFromScratch(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, NotificationsViewModel.OnNotificationStateChanged);
                RefreshScriptFilesState(false);

                NotificationsViewModel.AfterComplete(processResults);
                _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);
            }
        }

        private void ApplySyncSpecificState()
        {
            if (CheckIsTargetStateHistory())
            {
                _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
                NotificationsViewModel.BeforeStartProcess();

                ProcessResults processResults = _dbVersionsAPI.SetDBToSpecificState(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, true, NotificationsViewModel.OnNotificationStateChanged);
                RefreshScriptFilesState(false);

                NotificationsViewModel.AfterComplete(processResults);
                _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);
            }
        }


        private void Deploy()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
            NotificationsViewModel.BeforeStartProcess();

            ProcessResults processResults = _dbVersionsAPI.Deploy(DBVersionsViewModelData.ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);
            RefreshScriptFilesState(false);

            NotificationsViewModel.AfterComplete(processResults);
            _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);

            _osProcessUtils.StartOsProcess(DBVersionsViewModelData.ProjectConfig.DeployArtifactFolderPath);
        }

        private void RunSetDBStateManally()
        {
            _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.InProcess);
            NotificationsViewModel.BeforeStartProcess();

            ProcessResults processResults = _dbVersionsAPI.SetDBStateByVirtualExecution(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, NotificationsViewModel.OnNotificationStateChanged);
            RefreshScriptFilesState(false);

            NotificationsViewModel.AfterComplete(processResults);
            _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);
        }





        private bool RefreshScriptFilesState(bool showTrace)
        {
            ProcessResults processResults;

            if (showTrace)
            {
                processResults = _dbVersionsAPI.GetScriptFilesState(DBVersionsViewModelData.ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);
            }
            else
            {
                processResults = _dbVersionsAPI.GetScriptFilesState(DBVersionsViewModelData.ProjectConfig.Id, null);
            }

            if (processResults.Trace.HasError)
            {
                NotificationsViewModel.AfterComplete(processResults);

                if (processResults.Trace.ContainErrorCode("SystemTables"))
                {
                    _dbVersionsViewSateManager.ChangeViewState(DBVersionsViewStateType.MissingSystemTables);
                }
                else
                {
                    _dbVersionsViewSateManager.ChangeViewStateAfterProcessComplete(processResults.Trace);
                }
            }
            else
            {
                DBVersionsViewModelData.ScriptFilesState = processResults.Results as ScriptFilesState;

                //if (showTrace)
                //{
                //    NotificationsViewModel.AfterComplete(processResults);

                //    _dbVersionsViewSateManager.ChangeViewState_AfterProcessComplete(processResults.Trace);
                //}
            }

            return !processResults.Trace.HasError;
        }



        private bool CheckIsTargetStateHistory()
        {
            bool isAllowRun = true;

            if (_dbVersionsAPI.ValdiateTargetStateAlreadyExecuted(DBVersionsViewModelData.ProjectConfig.Id, DBVersionsViewModelData.TargetStateScriptFileName, NotificationsViewModel.OnNotificationStateChanged).Trace.HasError)
            {
                isAllowRun = UIGeneralEvents.FireOnConfirm(this, $"This action will drop the Database and recreate it only by the scripts, you may lose Data. Are you sure?");
            }

            return isAllowRun;
        }




        private TextInputResults FireOnTextInput(string instructionMessageText)
        {
            if (OnTextInput == null)
            {
                throw new Exception($"Bind method to 'OnTextInput' event is mandatory");
            }

            var eventsArgs = new OnTextInputEventsEventArgs(instructionMessageText);
            OnTextInput(this, eventsArgs);

            return eventsArgs.Results;
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
