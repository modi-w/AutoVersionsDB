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

        public event EventHandler OnSetNewProject;
        public event OnNavToProcessHandler OnNavToProcess;
        public event OnEditProjectHandler OnEditProject;

        private AutoVersionsDbAPI _autoVersionsDbAPI = null;

        //private double dummy


        public ChooseProject()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _autoVersionsDbAPI = AutoVersionsDbAPI.Instance;

                //lblProjectIcon.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblDeleteProject.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));

                //lblProjectName.DataBindings.Add(new Binding("Tag", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectGuid"));
                //lblProjectName.DataBindings.Add(new Binding("Text", _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList, "ProjectName"));

                tbSerchProject.Text = c_SearchPlaceHolderText;
                tbSerchProject.ForeColor = Color.DimGray;

                tbSerchProject.GotFocus += TbSerchProject_GotFocus;
                tbSerchProject.LostFocus += TbSerchProject_LostFocus;


                flowLayoutPanel1.Resize += FlowLayoutPanel1_Resize;

            }
           
            this.Load += ChooseProject_Load;

        }

        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            setProjectItemsSize();
        }

        private void setProjectItemsSize()
        {
            foreach (var childControl in flowLayoutPanel1.Controls)
            {
                ProjectItemControl projectItem = childControl as ProjectItemControl;
                projectItem.Width = flowLayoutPanel1.Width - 30;
            }
        }

        private void ChooseProject_Load(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Width = this.Width;
            RefreshProjectList();
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
                if (_autoVersionsDbAPI!= null
                    && _autoVersionsDbAPI.ConfigProjectsManager != null)
                {
                    List<ProjectConfigItem> filteredProjectList =
                        _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList
                        .Where(e => string.IsNullOrWhiteSpace(searchText) || e.ProjectName.Trim().ToLower().Contains(searchText.Trim().ToLower()))
                        .OrderBy(e => e.ProjectName)
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

                    setProjectItemsSize();
                }


            }
        }

        private void ProjectItem_OnEditProject(ProjectConfigItem projectConfigItem)
        {
            OnEditProject?.Invoke(projectConfigItem);
        }

        private void ProjectItem_OnRefreshProjectList()
        {
            RefreshProjectList();
        }

        private void ProjectItem_OnNavToProcess(ProjectConfigItem projectConfigItem)
        {
            OnNavToProcess?.Invoke(projectConfigItem);
        }

      

    

        private void tbSerchProject_TextChanged(object sender, EventArgs e)
        {
            RefreshProjectList();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            OnSetNewProject?.Invoke(sender, e);
        }

    




    }
}
