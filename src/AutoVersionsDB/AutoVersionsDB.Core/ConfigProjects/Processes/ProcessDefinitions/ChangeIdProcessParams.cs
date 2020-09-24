using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ChangeIdProcessParams : ProcessParams
    {
        public string Id { get; }
        public string NewId { get; }

        public ChangeIdProcessParams(string id, string newId)
        {
            Id = id;
            NewId = newId;
        }
    }
}
