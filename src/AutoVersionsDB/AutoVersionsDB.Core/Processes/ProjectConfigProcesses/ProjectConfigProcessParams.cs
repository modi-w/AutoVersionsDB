using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class ProjectConfigProcessParams : ProcessParams, IProjectCode
    {
        public ProjectConfigItem ProjectConfig { get; set; }
        public string ProjectCode => ProjectConfig.ProjectCode;
        public string NewProjectCode { get; set; }

        public ProjectConfigProcessParams(ProjectConfigItem projectConfig, string newProjectCode)
        {
            ProjectConfig = projectConfig;
            NewProjectCode = newProjectCode;
        }
    }
}
