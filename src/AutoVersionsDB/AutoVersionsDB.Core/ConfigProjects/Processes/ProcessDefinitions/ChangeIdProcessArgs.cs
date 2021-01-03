using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ChangeIdProcessArgs : ProcessArgs
    {
        public string Id { get; }
        public string NewId { get; }

        public ChangeIdProcessArgs(string id, string newId)
        {
            Id = id;
            NewId = newId;
        }
    }
}
