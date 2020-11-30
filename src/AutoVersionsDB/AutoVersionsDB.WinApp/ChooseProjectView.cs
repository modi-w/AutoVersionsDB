using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.ComponentModel;
using System.Windows.Forms;

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

                RenderProjectList();
            }
        }


        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.ChooseProjectViewModelData.FilteredProjectList):

                    RenderProjectList();
                    break;

                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            tbSerchProject.DataBindings.Clear();
            tbSerchProject.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    tbSerchProject,
                    nameof(tbSerchProject.Text),
                    ViewModel.ChooseProjectViewModelData,
                    nameof(ViewModel.ChooseProjectViewModelData.SerchProjectText)
                )
            );

        }

        private void RenderProjectList()
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
