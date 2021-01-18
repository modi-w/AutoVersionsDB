using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class DBVersionsProcessArgs : ProcessArgs
    {
        public string Id { get; set; }
        public string NewScriptName { get; set; }
        public TargetScripts TargetScripts { get; set; }

        public DBVersionsProcessArgs(string id, string newScriptName, TargetScripts targetScripts)
        {
            Id = id;
            NewScriptName = newScriptName;
            TargetScripts = targetScripts;
        }
    }
}