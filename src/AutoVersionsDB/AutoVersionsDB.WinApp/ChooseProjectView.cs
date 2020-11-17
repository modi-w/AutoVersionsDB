using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using AutoVersionsDB.UI.ChooseProject;

namespace AutoVersionsDB.WinApp
{
    public partial class ChooseProjectView : UserControlNinjectBase //UserControl
    {
        [Inject]
        public ChooseProjectViewModel ViewModel { get; set; }


        public ChooseProjectView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.ChooseProjectViewModelData.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();

                flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;

                renderProjectList();
            }
        }


        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.ChooseProjectViewModelData.FilteredProjectList):

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
                    ViewModel.ChooseProjectViewModelData,
                    nameof(ViewModel.ChooseProjectViewModelData.SerchProjectText)
                )
            );

        }

        private void renderProjectList()
        {
            if (flowLayoutPanel1.InvokeRequired)
            {
                flowLayoutPanel1.BeginInvoke((MethodInvoker)(() =>
                {
                    flowLayoutPanel1.Controls.Clear();

                    foreach (ProjectConfigItem projectConfig in ViewModel.ChooseProjectViewModelData.FilteredProjectList)
                    {
                        ProjectItemControl projectItemControl = new ProjectItemControl(ViewModel, projectConfig);
                        flowLayoutPanel1.Controls.Add(projectItemControl);
                    }

                    SetProjectItemsSize();

                }));

            }
            else
            {
                flowLayoutPanel1.Controls.Clear();

                foreach (ProjectConfigItem projectConfig in ViewModel.ChooseProjectViewModelData.FilteredProjectList)
                {
                    ProjectItemControl projectItemControl = new ProjectItemControl(ViewModel, projectConfig);
                    flowLayoutPanel1.Controls.Add(projectItemControl);
                }

                SetProjectItemsSize();

            }


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
