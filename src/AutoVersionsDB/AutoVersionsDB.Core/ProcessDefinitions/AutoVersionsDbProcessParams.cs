using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class AutoVersionsDbProcessParams : ProcessParams
    {
        public ProjectConfigItem ProjectConfig { get; set; }
        public string TargetStateScriptFileName { get; set; }

        public AutoVersionsDbProcessParams(ProjectConfigItem projectConfig, string targetStateScriptFileName)
        {
            ProjectConfig = projectConfig;
            TargetStateScriptFileName = targetStateScriptFileName;
        }
    }
}
