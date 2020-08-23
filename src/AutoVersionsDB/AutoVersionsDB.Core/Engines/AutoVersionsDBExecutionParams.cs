using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDBExecutionParams : ExecutionParams
    {
        public ProjectConfigItem ProjectConfig { get; set; }
        public string TargetStateScriptFileName { get; set; }

        public AutoVersionsDBExecutionParams(ProjectConfigItem projectConfig, string targetStateScriptFileName)
        {
            ProjectConfig = projectConfig;
            TargetStateScriptFileName = targetStateScriptFileName;
        }
    }
}
