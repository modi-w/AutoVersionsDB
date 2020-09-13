using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class ProjectConfigProcessParams : ProcessParams, IProjectCode
    {
        public ProjectConfigItem ProjectConfig { get; }
        public string ProjectCode { get; }
        public string NewProjectCode { get; }

        public ProjectConfigProcessParams(string projectCode)
        {
            ProjectCode = projectCode;
        }

        public ProjectConfigProcessParams(string projectCode, string newProjectCode)
        {
            ProjectCode = projectCode;
            NewProjectCode = newProjectCode;
        }
        public ProjectConfigProcessParams(ProjectConfigItem projectConfig)
        {
            ProjectConfig = projectConfig;
            ProjectCode = projectConfig.ProjectCode;
        }
    }
}
