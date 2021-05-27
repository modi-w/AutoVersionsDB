using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{

    public class ProjectConfigProcessArgs : ProcessArgs
    {
        public ProjectConfigItem ProjectConfig { get; }
        public string Id { get; }

        public ProjectConfigProcessArgs(string id)
        {
            Id = id;
        }

        public ProjectConfigProcessArgs(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ProjectConfig = projectConfig;
            Id = projectConfig.Id;
        }
    }
}
