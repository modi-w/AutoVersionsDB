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

            this.Load += ChooseProject_Load;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {


                //lblProjectIcon.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblDeleteProject.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));

                //lblProjectName.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblProjectName.DataBindings.Add(new Binding("Text", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectName"));

                tbSerchProject.ForeColor = Color.DimGray;

                tbSerchProject.GotFocus += TbSerchProject_GotFocus;
                tbSerchProject.LostFocus += TbSerchProject_LostFocus;


                flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;


                renderProjectList();

                //this.Load += ChooseProject_Load;
            }


        }

        private void ChooseProject_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel.PropertyChanged += _viewModel_PropertyChanged;
                SetDataBindings();
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


        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            SetProjectItemsSize();
        }

        private void renderProjectList()
        {
            flowLayoutPanel1.Controls.Clear();

            //if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            //{

            foreach (ProjectConfigItem projectConfigItem in ViewModel.FilteredProjectList)
            {
                ProjectItemControl projectItemControl = new ProjectItemControl();
                projectItemControl.SetProjectConfig(projectConfigItem);
                projectItemControl.OnNavToProcess += ProjectItem_OnNavToProcess;
                projectItemControl.OnRefreshProjectList += ProjectItem_OnRefreshProjectList;
                projectItemControl.OnEditProject += ProjectItem_OnEditProject;
                flowLayoutPanel1.Controls.Add(projectItemControl);
            }

            SetProjectItemsSize();
            // }
        }



        private void SetProjectItemsSize()
        {
            foreach (var childControl in flowLayoutPanel1.Controls)
            {
                ProjectItemControl projectItem = childControl as ProjectItemControl;
                projectItem.Width = flowLayoutPanel1.Width - 30;
            }
        }

        //private void ChooseProject_Load(object sender, EventArgs e)
        //{
        //    if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        //    {
        //        //flowLayoutPanel1.Width = this.Width;
        //        RefreshProjectList();
        //    }
        //}

        private void TbSerchProject_LostFocus(object sender, EventArgs e)
        {
            ViewModel.ResolveSerchProjectTextPlaceHolder();

            if (ViewModel.IsSerchProjectTextEmpty)
            {
                tbSerchProject.ForeColor = Color.DimGray;
            }
        }

        private void TbSerchProject_GotFocus(object sender, EventArgs e)
        {
            ViewModel.ResolveSerchProjectTextOnFocus();

            if (ViewModel.IsSerchProjectTextEmpty)
            {
                tbSerchProject.ForeColor = Color.Black;
            }
        }


        private void ProjectItem_OnEditProject(string id)
        {
            ViewModel.NavToEditProjectConfig(id);
        }

        private void ProjectItem_OnRefreshProjectList()
        {
            ViewModel.RefreshProjectList();
        }

        private void ProjectItem_OnNavToProcess(string id)
        {
            ViewModel.NavToDBVersions(id);
        }





        //private void TbSerchProject_TextChanged(object sender, EventArgs e)
        //{
        //    ViewModel.RefreshProjectList();
        //}


        private void BtnNewProject_Click(object sender, EventArgs e)
        {
            ViewModel.NavToCreateNewProjectConfig();
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
