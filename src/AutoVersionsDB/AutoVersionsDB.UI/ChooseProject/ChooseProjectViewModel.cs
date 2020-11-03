using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.UI.ChooseProject
{
    public class ChooseProjectViewModel : INotifyPropertyChanged
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;

        private List<ProjectConfigItem> _allProjectsList;


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





        public ChooseProjectViewModelData ChooseProjectViewModelData { get; }


        public RelayCommand NavToCreateNewProjectConfigCommand { get; private set; }
        public RelayCommand<string> NavToEditProjectConfigCommand { get; private set; }
        public RelayCommand<string> NavToDBVersionsCommand { get; private set; }


        public RelayCommand<string> DeleteProjectCommand { get; private set; }



        public ChooseProjectViewModel(ProjectConfigsAPI projectConfigsAPI,
                                    ChooseProjectViewModelData chooseProjectViewModelData)
        {
            _projectConfigsAPI = projectConfigsAPI;
            ChooseProjectViewModelData = chooseProjectViewModelData;

            DeleteProjectCommand = new RelayCommand<string>(DeleteProject);

            ChooseProjectViewModelData.PropertyChanged += ChooseProjectViewModelData_PropertyChanged;
        }

        private void ChooseProjectViewModelData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ChooseProjectViewModelData.SerchProjectText):

                    RefreshProjectList();
                    break;

                default:
                    break;
            }
        }

        public void Clear()
        {
            ChooseProjectViewModelData.SerchProjectText = "";
            RefreshProjectList();
        }


        public void RefreshProjectList()
        {
            _allProjectsList = _projectConfigsAPI.GetProjectsList();

            ChooseProjectViewModelData.FilteredProjectList =
                _allProjectsList
                .Where(e => string.IsNullOrWhiteSpace(ChooseProjectViewModelData.SerchProjectText)
                            || e.Id.Trim().ToUpperInvariant().Contains(ChooseProjectViewModelData.SerchProjectText.Trim().ToUpperInvariant())
                            || e.Description.Trim().ToUpperInvariant().Contains(ChooseProjectViewModelData.SerchProjectText.Trim().ToUpperInvariant()))
                .OrderBy(e => e.Id)
                .ToList();
        }



        private void SetRouteCommands()
        {
            NavToCreateNewProjectConfigCommand = new RelayCommand(NavToCreateNewProjectConfig);
            NavToEditProjectConfigCommand = new RelayCommand<string>(NavToEditProjectConfig);
            NavToDBVersionsCommand = new RelayCommand<string>(NavToDBVersions);
        }


        private void NavToCreateNewProjectConfig()
        {
            ViewRouter.NavToEditProjectConfig(null);
        }


        private void NavToEditProjectConfig(string id)
        {
            ViewRouter.NavToEditProjectConfig(id);
        }



        private void NavToDBVersions(string id)
        {
            ViewRouter.NavToDBVersions(id);
        }



        private void DeleteProject(string id)
        {
            bool isAllowRun = UIGeneralEvents.FireOnConfirm(this,$"Are you sure you want to delete the configurration for the project: '{id}'");

            if (isAllowRun)
            {
                _projectConfigsAPI.RemoveProjectConfig(id, null);

                RefreshProjectList();
            }
        }





  



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
