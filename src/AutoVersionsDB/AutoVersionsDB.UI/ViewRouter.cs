using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.EditProject;
using AutoVersionsDB.UI.Main;

namespace AutoVersionsDB.UI
{
    public class ViewRouter
    {

        private readonly MainViewModel _mainViewModel;
        private readonly ChooseProjectViewModel _chooseProjectViewModel;
        private readonly EditProjectViewModel _editProjectConfigDetailsViewModel;
        private readonly DBVersionsViewModel _dbVersionsViewModel;


        public ViewRouter(MainViewModel mainViewModel,
                            ChooseProjectViewModel chooseProjectViewModel,
                            EditProjectViewModel editProjectConfigDetailsViewModel,
                            DBVersionsViewModel dbVersionsViewModel)
        {
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
            _mainViewModel.MainViewModelData.CurrentView = ViewType.ChooseProject;
            _chooseProjectViewModel.Clear();

            SetBtnChooseProjectVisibility();

        }

        public void NavToEditProjectConfig(string id)
        {
            _mainViewModel.MainViewModelData.CurrentView = ViewType.EditProjectConfig;

            if (string.IsNullOrWhiteSpace(id))
            {
                _editProjectConfigDetailsViewModel.CreateNewProjectConfig();
            }
            else
            {
                _editProjectConfigDetailsViewModel.SetProjectConfig(id);
            }


            SetBtnChooseProjectVisibility();

        }


        public void NavToDBVersions(string id)
        {
            _mainViewModel.MainViewModelData.CurrentView = ViewType.DBVersions;

            _dbVersionsViewModel.SetProjectConfig(id);

            SetBtnChooseProjectVisibility();
        }




        private void SetBtnChooseProjectVisibility()
        {
            _mainViewModel.MainControls.BtnChooseProjectVisible = _mainViewModel.MainViewModelData.CurrentView != ViewType.ChooseProject;
        }
    }
}
