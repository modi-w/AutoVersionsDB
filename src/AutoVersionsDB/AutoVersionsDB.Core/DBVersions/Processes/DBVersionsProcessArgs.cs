using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class DBVersionsProcessArgs : ProcessArgs
    {
        public string Id { get; set; }
        public string TargetStateScriptFileName { get; set; }
        public string NewScriptName { get; set; }

        public DBVersionsProcessArgs(string id, string targetStateScriptFileName, string newScriptName)
        {
            Id = id;
            TargetStateScriptFileName = targetStateScriptFileName;
            NewScriptName = newScriptName;
        }
    }
}
