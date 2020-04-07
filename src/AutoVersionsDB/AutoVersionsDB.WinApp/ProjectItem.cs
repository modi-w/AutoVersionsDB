using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core;

namespace AutoVersionsDB.WinApp
{
    public partial class ProjectItem : UserControl
    {
        private AutoVersionsDbAPI _autoVersionsDbAPI = null;


        public event OnNavToProcessHandler OnNavToProcess;
        public event OnRefreshProjectListHandler OnRefreshProjectList;
        
        public ProjectConfigItem ProjectConfig { get; private set; }

        public ProjectItem()
        {

        }
        public ProjectItem(ProjectConfigItem projectConfigItem)
        {
            InitializeComponent();

            ProjectConfig = projectConfigItem;

            lblProjectName.Text = ProjectConfig.ProjectName;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _autoVersionsDbAPI = AutoVersionsDbAPI.Instance;
            }
        }

        private void lblProjectName_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(ProjectConfig);
        }

        private void lblProjectIcon_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(ProjectConfig);
        }

        private void lblDeleteProject_Click(object sender, EventArgs e)
        {
            string currProjectGuid = (sender as Label).Tag.ToString();

            ProjectConfigItem currProjectConfig = _autoVersionsDbAPI.ConfigProjectsManager.ProjectConfigsList.Where(ee => ee.ProjectGuid == currProjectGuid).First();

            string warningMessage = $"Are you sure you want to delete the configurration for the project: '{currProjectConfig.ProjectName}'";
            bool results = MessageBox.Show(this, warningMessage, "Delete Project", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes;

            if (results)
            {
                _autoVersionsDbAPI.ConfigProjectsManager.RemoveProjectConfig(currProjectGuid);

                OnRefreshProjectList?.Invoke();
            }
        }
    }
}
