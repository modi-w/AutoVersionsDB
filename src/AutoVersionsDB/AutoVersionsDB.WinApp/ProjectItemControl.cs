using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.UI.ChooseProject;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public partial class ProjectItemControl : UserControl
    {
        private readonly ChooseProjectViewModel _viewModel;


        public ProjectConfigItem ProjectConfig { get; private set; }

        //public ProjectItemControl(ProjectConfigItem projectConfigItem)
        public ProjectItemControl(ChooseProjectViewModel viewModel, ProjectConfigItem projectConfig)
        {
            InitializeComponent();

            _viewModel = viewModel;
            ProjectConfig = projectConfig;

            //if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            //{
            //    _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            //    SetDataBindings();
            //}

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();
            }
        }




        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            lblId.Text = ProjectConfig.Id;
            lblProjectDesc.Text = ProjectConfig.Description;
        }



        private void LblProjectName_Click(object sender, EventArgs e)
        {
            _viewModel.NavToDBVersionsCommand.Execute(ProjectConfig.Id);
        }

        private void LblProjectIcon_Click(object sender, EventArgs e)
        {
            _viewModel.NavToDBVersionsCommand.Execute(ProjectConfig.Id);
        }

        private void LblProcessLink_Click(object sender, EventArgs e)
        {
            _viewModel.NavToDBVersionsCommand.Execute(ProjectConfig.Id);
        }

        private void LblEditProject_Click(object sender, EventArgs e)
        {
            _viewModel.NavToEditProjectConfigCommand.Execute(ProjectConfig.Id);
        }

        private void LblDeleteProject_Click(object sender, EventArgs e)
        {
            _viewModel.DeleteProjectCommand.Execute(ProjectConfig.Id);
        }




        #region Dispose

        // To detect redundant calls
        private bool _disposed;

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).

                if (components != null)
                {
                    components.Dispose();
                }
            }

            _disposed = true;
            // Call base class implementation.
            base.Dispose(disposing);
        }

        #endregion


    }
}
