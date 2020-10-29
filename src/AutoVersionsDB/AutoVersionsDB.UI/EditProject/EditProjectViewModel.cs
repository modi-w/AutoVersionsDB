using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoVersionsDB.UI;

namespace AutoVersionsDB.UI.EditProject
{
    public class EditProjectViewModel : INotifyPropertyChanged
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly NotificationsViewModel _notificationsViewModel;

        private readonly EditProjectViewSateManager _editProjectViewSateManager;

        private bool _isNewProjectConfig = false;


        public List<DBType> DBTypes { get; }

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

        public ProjectConfigErrorMessages ProjectConfigErrorMessages { get; }

        public EditProjectControls EditProjectControls { get; }

        private ObservableProjectConfig _projectConfig;
        public ObservableProjectConfig ProjectConfig
        {
            get => _projectConfig;
            set
            {
                SetField(ref _projectConfig, value);

                _editProjectViewSateManager.ShowHideEnvFields(ProjectConfig.DevEnvironment);

                _projectConfig.PropertyChanged += _projectConfig_PropertyChanged;
            }
        }


        public RelayCommand NavToChooseProjectCommand { get; private set; }
        public RelayCommand NavToDBVersionsCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        public EditProjectViewModel(ProjectConfigsAPI projectConfigsAPI,
                                    DBVersionsAPI dbVersionsAPI,
                                    EditProjectViewSateManager editProjectViewSateManager,
                                    NotificationsViewModel notificationsViewModel,
                                    EditProjectControls editProjectControls,
                                    ProjectConfigErrorMessages projectConfigErrorMessages)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _dbVersionsAPI = dbVersionsAPI;
            _editProjectViewSateManager = editProjectViewSateManager;
            _notificationsViewModel = notificationsViewModel;

            EditProjectControls = editProjectControls;
            ProjectConfigErrorMessages = projectConfigErrorMessages;

            DBTypes = _projectConfigsAPI.GetDBTypes();

            SaveCommand = new RelayCommand(Save);
            SetEditIdStateCommand = new RelayCommand(SetEditIdState);
            CancelEditIdCommand = new RelayCommand(CancelEditId);
            SaveChangeIdCommand = new RelayCommand(SaveChangeId);

        }





        public void CreateNewProjectConfig()
        {
            _notificationsViewModel.WaitingForUser();

            _editProjectViewSateManager.ClearUIElementsErrors();

            ProjectConfigItem newProjectConfigItem = new ProjectConfigItem();
            newProjectConfigItem.DevEnvironment = true;
            newProjectConfigItem.SetDefaltValues();

            ProjectConfig = new ObservableProjectConfig(newProjectConfigItem);
            //ProjectConfig.PropertyChanged += ProjectConfig_PropertyChanged;

            _isNewProjectConfig = true;

            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.New);

        }


        private void _projectConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ProjectConfig.DevEnvironment):
                case nameof(ProjectConfig.DeliveryEnvironment):

                    _editProjectViewSateManager.ShowHideEnvFields(ProjectConfig.DevEnvironment);

                    break;
            }
        }


        public void SetProjectConfig(string id)
        {
            _isNewProjectConfig = false;

            Refresh(id);
        }



        private void Refresh()
        {
            Refresh(ProjectConfig.Id);
        }
        private void Refresh(string id)
        {
            ProjectConfigItem projectConfig = _projectConfigsAPI.GetProjectConfigById(id);
            ProjectConfig = new ObservableProjectConfig(projectConfig);

            ValidateAll();

            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.Update);
        }


        private void SetRouteCommands()
        {
            NavToChooseProjectCommand = new RelayCommand(_viewRouter.NavToChooseProject);
            NavToDBVersionsCommand = new RelayCommand(NavToDBVersions);
        }



        private void NavToDBVersions()
        {
            _viewRouter.NavToDBVersions(ProjectConfig.Id);
        }


        private void Save()
        {
            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.InPorcess);


            Task.Run(() =>
            {
                if (_isNewProjectConfig)
                {
                    ProcessResults processResults = _projectConfigsAPI.SaveNewProjectConfig(ProjectConfig.ActualProjectConfig, _notificationsViewModel.OnNotificationStateChanged);

                    handleCompleteProcess(processResults.Trace);

                    if (!processResults.Trace.HasError)
                    {
                        _isNewProjectConfig = false;
                    }

                }
                else
                {
                    ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(ProjectConfig.ActualProjectConfig, _notificationsViewModel.OnNotificationStateChanged);

                    handleCompleteProcess(processResults.Trace);
                }
            });
        }


        #region Change Id

        private string _prevId = null;

        public RelayCommand SetEditIdStateCommand { get; private set; }
        public RelayCommand CancelEditIdCommand { get; private set; }
        public RelayCommand SaveChangeIdCommand { get; private set; }


        private void SetEditIdState()
        {
            _prevId = ProjectConfig.Id;
            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.EditId);
        }

        private void CancelEditId()
        {
            Refresh();
        }

        private void SaveChangeId()
        {
            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.InPorcess);


            Task.Run(() =>
            {
                ProcessResults processResults = _projectConfigsAPI.ChangeProjectId(_prevId, ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);

                handleCompleteProcess(processResults.Trace);
            });
        }

        #endregion







        #region Validation

        private void ValidateAll()
        {
            _editProjectViewSateManager.ChangeViewState(EditProjectViewStateType.InPorcess);

            Task.Run(() =>
            {
                _notificationsViewModel.WaitingForUser();

                _editProjectViewSateManager.ClearUIElementsErrors();

                ProcessResults processResults = _dbVersionsAPI.ValidateProjectConfig(ProjectConfig.Id, _notificationsViewModel.OnNotificationStateChanged);

                _editProjectViewSateManager.HandleProcessErrors(_isNewProjectConfig, processResults.Trace);
            });
        }

        private void handleCompleteProcess(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                _editProjectViewSateManager.HandleProcessErrors(_isNewProjectConfig, processResults);
            }
            else
            {
                Refresh();
            }
        }






        #endregion




        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        //private void ProjectConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    OnPropertyChanged(e.PropertyName);
        //}

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
