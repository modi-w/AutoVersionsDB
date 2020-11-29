
/* Unmerged change from project 'AutoVersionsDB.WinApp'
Before:
using System;
using AutoVersionsDB.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
After:
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Collections.Generic;
*/
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.UI.ChooseProject;
using System;
using System.ComponentModel;

/* Unmerged change from project 'AutoVersionsDB.WinApp'
Before:
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core;
using AutoVersionsDB.WinApp.Utils;
using AutoVersionsDB.UI.ChooseProject;
After:
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
*/
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

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _viewModel.PropertyChanged += _viewModel_PropertyChanged;
                SetDataBindings();
            }

        }





        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
        private bool _disposed = false;

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
