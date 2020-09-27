using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class DBVersionsProcessParams : ProcessParams
    {
        public string Id { get; set; }
        public string TargetStateScriptFileName { get; set; }
        public string NewScriptName { get; set; }

        public DBVersionsProcessParams(string id, string targetStateScriptFileName, string newScriptName)
        {
            Id = id;
            TargetStateScriptFileName = targetStateScriptFileName;
            NewScriptName = newScriptName;
        }
    }
}
