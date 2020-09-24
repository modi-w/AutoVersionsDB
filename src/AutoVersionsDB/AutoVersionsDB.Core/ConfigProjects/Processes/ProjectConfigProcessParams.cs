using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{

    public class ProjectConfigProcessParams : ProcessParams
    {
        public ProjectConfigItem ProjectConfig { get; }
        public string ProjectCode { get; }

        public ProjectConfigProcessParams(string projectCode)
        {
            ProjectCode = projectCode;
        }

        public ProjectConfigProcessParams(ProjectConfigItem projectConfig)
        {
            ProjectConfig = projectConfig;
            ProjectCode = projectConfig.Code;
        }
    }
}
