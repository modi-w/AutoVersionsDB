using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;

namespace AutoVersionsDB.WinApp
{
    public partial class ChooseProject : UserControl
    {
        private const string c_SearchPlaceHolderText = "Search Project...";

        private List<ProjectConfigItem> _allProjectsList;

        public event EventHandler OnSetNewProject;
        public event OnNavToProcessHandler OnNavToProcess;
        public event OnEditProjectHandler OnEditProject;


        //private double dummy


        public ChooseProject()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                //lblProjectIcon.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblDeleteProject.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));

                //lblProjectName.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblProjectName.DataBindings.Add(new Binding("Text", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectName"));

                tbSerchProject.Text = c_SearchPlaceHolderText;
                tbSerchProject.ForeColor = Color.DimGray;

                tbSerchProject.GotFocus += TbSerchProject_GotFocus;
                tbSerchProject.LostFocus += TbSerchProject_LostFocus;


                flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;


                this.Load += ChooseProject_Load;
            }


        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            SetProjectItemsSize();
        }

        private void SetProjectItemsSize()
        {
            foreach (var childControl in flowLayoutPanel1.Controls)
            {
                ProjectItemControl projectItem = childControl as ProjectItemControl;
                projectItem.Width = flowLayoutPanel1.Width - 30;
            }
        }

        private void ChooseProject_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                //flowLayoutPanel1.Width = this.Width;
                RefreshProjectList();
            }
        }

        private void TbSerchProject_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSerchProject.Text))
            {
                tbSerchProject.Text = c_SearchPlaceHolderText;
                tbSerchProject.ForeColor = Color.DimGray;
            }
        }

        private void TbSerchProject_GotFocus(object sender, EventArgs e)
        {
            if (tbSerchProject.Text == c_SearchPlaceHolderText)
            {
                tbSerchProject.Text = "";
                tbSerchProject.ForeColor = Color.Black;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<Pending>")]
        public void RefreshProjectList()
        {
            string searchText = "";
            if (tbSerchProject.Text == c_SearchPlaceHolderText)
            {
                searchText = "";
            }
            else
            {
                searchText = tbSerchProject.Text;
            }


            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _allProjectsList = AutoVersionsDbAPI.GetProjectsList();

                    List<ProjectConfigItem> filteredProjectList =
                        _allProjectsList
                        .Where(e => string.IsNullOrWhiteSpace(searchText) || e.ProjectCode.Trim().ToUpperInvariant().Contains(searchText.Trim().ToUpperInvariant()))
                        .OrderBy(e => e.ProjectCode)
                        .ToList();

                    flowLayoutPanel1.Controls.Clear();

                    foreach (ProjectConfigItem projectConfigItem in filteredProjectList)
                    {
                        ProjectItemControl projectItem = new ProjectItemControl(projectConfigItem);
                        projectItem.OnNavToProcess += ProjectItem_OnNavToProcess;
                        projectItem.OnRefreshProjectList += ProjectItem_OnRefreshProjectList;
                        projectItem.OnEditProject += ProjectItem_OnEditProject;
                        flowLayoutPanel1.Controls.Add(projectItem);
                    }

                    SetProjectItemsSize();


            }
        }

        private void ProjectItem_OnEditProject(string projectCode)
        {
            OnEditProject?.Invoke(projectCode);
        }

        private void ProjectItem_OnRefreshProjectList()
        {
            RefreshProjectList();
        }

        private void ProjectItem_OnNavToProcess(string projectCode)
        {
            OnNavToProcess?.Invoke(projectCode);
        }

      

    

        private void TbSerchProject_TextChanged(object sender, EventArgs e)
        {
            RefreshProjectList();
        }


        private void BtnNewProject_Click(object sender, EventArgs e)
        {
            OnSetNewProject?.Invoke(sender, e);
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
