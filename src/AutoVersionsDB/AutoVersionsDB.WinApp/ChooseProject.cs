using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.UI;
using AutoVersionsDB.WinApp.Utils;
using Ninject;


namespace AutoVersionsDB.WinApp
{
    public partial class ChooseProject : UserControlNinjectBase //UserControl
    {
        [Inject]
        public ChooseProjectViewModel ViewModel { get; set; }


        public ChooseProject()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.PropertyChanged += _viewModel_PropertyChanged;
                SetDataBindings();

                flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;

                renderProjectList();
            }
        }

      

        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.FilteredProjectList):

                    renderProjectList();
                    break;

                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            this.tbSerchProject.DataBindings.Clear();
            this.tbSerchProject.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbSerchProject,
                    nameof(tbSerchProject.Text),
                    ViewModel,
                    nameof(ViewModel.SerchProjectText)
                )
            );

        }

        private void renderProjectList()
        {
            flowLayoutPanel1.Controls.Clear();

            //if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            //{

            foreach (ProjectConfigItem projectConfig in ViewModel.FilteredProjectList)
            {
                ProjectItemControl projectItemControl = new ProjectItemControl(ViewModel, projectConfig);
                flowLayoutPanel1.Controls.Add(projectItemControl);
            }

            SetProjectItemsSize();
            // }
        }


        private void BtnNewProject_Click(object sender, EventArgs e)
        {
            ViewModel.NavToCreateNewProjectConfigCommand.Execute();
        }



        private void SetProjectItemsSize()
        {
            foreach (var childControl in flowLayoutPanel1.Controls)
            {
                ProjectItemControl projectItem = childControl as ProjectItemControl;
                projectItem.Width = flowLayoutPanel1.Width - 30;
            }
        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            SetProjectItemsSize();
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
                //_autoVersionsDbAPI.Dispose();

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
