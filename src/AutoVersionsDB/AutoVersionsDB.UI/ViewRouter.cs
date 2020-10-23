using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.UI
{
    public class ViewRouter
    {
        private readonly IViewContainer _viewContainer;

        private readonly MainViewModel _mainViewModel;
        private readonly ChooseProjectViewModel _chooseProjectViewModel;
        private readonly EditProjectViewModel _editProjectConfigDetailsViewModel;
        private readonly DBVersionsViewModel _dbVersionsViewModel;


        public ViewRouter(IViewContainer viewContainer,
                            MainViewModel mainViewModel,
                            ChooseProjectViewModel chooseProjectViewModel,
                            EditProjectViewModel editProjectConfigDetailsViewModel,
                            DBVersionsViewModel dbVersionsViewModel)
        {
            _viewContainer = viewContainer;
            _mainViewModel = mainViewModel;
            _chooseProjectViewModel = chooseProjectViewModel;
            _editProjectConfigDetailsViewModel = editProjectConfigDetailsViewModel;
            _dbVersionsViewModel = dbVersionsViewModel;

            _mainViewModel.ViewRouter = this;
            _chooseProjectViewModel.ViewRouter = this;
            _editProjectConfigDetailsViewModel.ViewRouter = this;
            _dbVersionsViewModel.ViewRouter = this;

            DefaultView();
        }

        public void DefaultView()
        {
            NavToChooseProject();
        }


        public void NavToChooseProject()
        {
            _viewContainer.SetView(ViewType.ChooseProject);

            SetBtnChooseProjectVisibility();

        }

        public void NavToEditProjectConfig(string id)
        {
            _viewContainer.SetView(ViewType.EditProjectConfig);

            Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _editProjectConfigDetailsViewModel.CreateNewProjectConfig();
                }
                else
                {
                    _editProjectConfigDetailsViewModel.SetProjectConfig(id);
                }
            });


            SetBtnChooseProjectVisibility();

        }


        public void NavToDBVersions(string id)
        {
            _viewContainer.SetView(ViewType.DBVersions);

            Task.Run(() =>
            {
                _dbVersionsViewModel.SetProjectConfig(id);
            });

            SetBtnChooseProjectVisibility();
        }




        private void SetBtnChooseProjectVisibility()
        {
            _mainViewModel.BtnChooseProjectVisible = _viewContainer.CurrentView != ViewType.ChooseProject;
        }
    }
}
