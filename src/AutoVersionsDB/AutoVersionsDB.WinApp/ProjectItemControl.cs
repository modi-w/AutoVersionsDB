﻿using System;
using AutoVersionsDB.Common;
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
using AutoVersionsDB.WinApp.Utils;

namespace AutoVersionsDB.WinApp
{
    public partial class ProjectItemControl : UserControl
    {
        public event OnNavToProcessHandler OnNavToProcess;
        public event OnRefreshProjectListHandler OnRefreshProjectList;
        public event OnEditProjectHandler OnEditProject;

        public ProjectConfigItem ProjectConfig { get; private set; }

        public ProjectItemControl(ProjectConfigItem projectConfigItem)
        {
            
            projectConfigItem.ThrowIfNull(nameof(projectConfigItem));

            InitializeComponent();

            ProjectConfig = projectConfigItem;

            lblProjectName.Text = ProjectConfig.ProjectName;

        }

        private void LblProjectName_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(ProjectConfig);
        }

        private void LblProjectIcon_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(ProjectConfig);
        }

        private void LblProcessLink_Click(object sender, EventArgs e)
        {
            OnNavToProcess?.Invoke(ProjectConfig);
        }

        private void LblEditProject_Click(object sender, EventArgs e)
        {
            OnEditProject?.Invoke(ProjectConfig);
        }

        private void LblDeleteProject_Click(object sender, EventArgs e)
        {
            string warningMessage = $"Are you sure you want to delete the configurration for the project: '{ProjectConfig.ProjectName}'";
            bool results = MessageBox.Show(this, warningMessage, "Delete Project", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes;

            if (results)
            {
                AutoVersionsDbAPI.RemoveProjectConfig(ProjectConfig.ProjectGuid);

                OnRefreshProjectList?.Invoke();
            }
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
