using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{

    public class ProjectConfigProcessParams : ProcessParams
    {
        public ProjectConfigItem ProjectConfig { get; }
        public string Id { get; }

        public ProjectConfigProcessParams(string id)
        {
            Id = id;
        }

        public ProjectConfigProcessParams(ProjectConfigItem projectConfig)
        {
            ProjectConfig = projectConfig;
            Id = projectConfig.Id;
        }
    }
}
