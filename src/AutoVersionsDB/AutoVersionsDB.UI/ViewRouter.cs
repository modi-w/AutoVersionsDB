using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.UI
{
    public class ViewRouter
    {
        private readonly IViewContainer _viewContainer;

        private readonly MainController _mainController;
        private readonly ChooseProjectController _chooseProjectController;
        private readonly EditProjectConfigController _editProjectConfigDetailsController;
        private readonly DBVersionsController _dbVersionsController;
        

        public ViewRouter(IViewContainer viewContainer,
                            MainController mainController,
                            ChooseProjectController chooseProjectController,
                            EditProjectConfigController editProjectConfigDetailsController,
                            DBVersionsController dbVersionsController)
        {
            _viewContainer = viewContainer;
            _mainController = mainController;
            _chooseProjectController = chooseProjectController;
            _editProjectConfigDetailsController = editProjectConfigDetailsController;
            _dbVersionsController = dbVersionsController;

            DefaultView();
        }

        public void DefaultView()
        {
            NavToChooseProject();
        }


        public void NavToChooseProject()
        {
            _viewContainer.SetView(_chooseProjectController.View);

            SetBtnChooseProjectVisibility();

        }

        public void NavToEditProjectConfig(string id)
        {
            _viewContainer.SetView(_editProjectConfigDetailsController.View);

            Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _editProjectConfigDetailsController.CreateNewProjectConfig();
                }
                else
                {
                    _editProjectConfigDetailsController.SetProjectConfig(id);
                }
            });


            SetBtnChooseProjectVisibility();

        }


        public void NavToDBVersions(string id)
        {
            _viewContainer.SetView(_dbVersionsController.View);

            Task.Run(() =>
            {
                _dbVersionsController.SetProjectConfig(id);
            });

            SetBtnChooseProjectVisibility();
        }




        private void SetBtnChooseProjectVisibility()
        {
            _mainController.View.BtnChooseProjectVisible = _viewContainer.CurrentView != _chooseProjectController.View;
        }
    }
}
