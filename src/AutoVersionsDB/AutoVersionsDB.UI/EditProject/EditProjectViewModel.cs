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




        public NotificationsViewModel NotificationsViewModel { get; }
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
            }
        }






        public RelayCommand NavToChooseProjectCommand { get; private set; }
        public RelayCommand NavToDBVersionsCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }


        public EditProjectViewModel(ProjectConfigsAPI projectConfigsAPI,
                                    DBVersionsAPI dbVersionsAPI,
                                    NotificationsViewModel notificationsViewModel)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _dbVersionsAPI = dbVersionsAPI;
            NotificationsViewModel = notificationsViewModel;

            ProjectConfigErrorMessages = new ProjectConfigErrorMessages();
            EditProjectControls = new EditProjectControls();

            DBTypes = _projectConfigsAPI.GetDBTypes();
        }





        public void CreateNewProjectConfig()
        {
            NotificationsViewModel.WaitingForUser();

            ProjectConfigErrorMessages.ClearUIElementsErrors();

            ProjectConfigItem newProjectConfigItem = new ProjectConfigItem();
            newProjectConfigItem.DevEnvironment = true;
            newProjectConfigItem.SetDefaltValues();

            ProjectConfig = new ObservableProjectConfig(newProjectConfigItem);
            //ProjectConfig.PropertyChanged += ProjectConfig_PropertyChanged;

            EditProjectControls.ChangeViewState(EditProjectViewStateType.New);

        }



        public void SetProjectConfig(string id)
        {
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

            EditProjectControls.ChangeViewState(EditProjectViewStateType.Update);
        }


        private void SetRouteCommands()
        {
            NavToChooseProjectCommand = new RelayCommand(_viewRouter.NavToChooseProject);
            NavToDBVersionsCommand = new RelayCommand(NavToDBVersions);
            SaveCommand = new RelayCommand(Save);
            SetEditIdStateCommand = new RelayCommand(SetEditIdState);
            CancelEditIdCommand = new RelayCommand(CancelEditId);
            SaveChangeIdCommand = new RelayCommand(SaveChangeId);


        }



        private void NavToDBVersions()
        {
            _viewRouter.NavToDBVersions(ProjectConfig.Id);
        }


        private void Save()
        {
            EditProjectControls.ChangeViewState(EditProjectViewStateType.InPorcess);


            Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(ProjectConfig.Id))
                {
                    ProcessResults processResults = _projectConfigsAPI.SaveNewProjectConfig(ProjectConfig.ActualProjectConfig, NotificationsViewModel.OnNotificationStateChanged);

                    handleCompleteProcess(processResults.Trace);

                }
                else
                {
                    ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(ProjectConfig.ActualProjectConfig, NotificationsViewModel.OnNotificationStateChanged);

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
            EditProjectControls.ChangeViewState(EditProjectViewStateType.EditId);
        }

        private void CancelEditId()
        {
            Refresh();
        }

        private void SaveChangeId()
        {
            EditProjectControls.ChangeViewState(EditProjectViewStateType.InPorcess);


            Task.Run(() =>
            {
                ProcessResults processResults = _projectConfigsAPI.ChangeProjectId(_prevId, ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);

                handleCompleteProcess(processResults.Trace);
            });
        }

        #endregion







        #region Validation

        private void ValidateAll()
        {


            EditProjectControls.ChangeViewState(EditProjectViewStateType.InPorcess);

            Task.Run(() =>
            {
                NotificationsViewModel.WaitingForUser();

                ProjectConfigErrorMessages.ClearUIElementsErrors();

                ProcessResults processResults = _dbVersionsAPI.ValidateProjectConfig(ProjectConfig.Id, NotificationsViewModel.OnNotificationStateChanged);

                handleProcessErrors(processResults.Trace);
            });
        }

        private void handleCompleteProcess(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                handleProcessErrors(processResults);
            }
            else
            {
                Refresh();
            }
        }


        private void handleProcessErrors(ProcessTrace processResults)
        {
            NotificationsViewModel.AfterComplete();

            ProjectConfigErrorMessages.SetErrorsToUiElements(processResults);

            EditProjectControls.ImgErrorVisible = processResults.HasError;
            EditProjectControls.ImgValidVisible = !processResults.HasError;
            EditProjectControls.BtnNavToProcessVisible = !processResults.HasError;

            if (string.IsNullOrWhiteSpace(ProjectConfig.Id))
            {
                EditProjectControls.ChangeViewState(EditProjectViewStateType.New);
            }
            else
            {
                EditProjectControls.ChangeViewState(EditProjectViewStateType.Update);
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
