using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ChangeProjectCodeProcessParams : ProcessParams
    {
        public string ProjectCode { get; }
        public string NewProjectCode { get; }

        public ChangeProjectCodeProcessParams(string projectCode, string newProjectCode)
        {
            ProjectCode = projectCode;
            NewProjectCode = newProjectCode;
        }
    }
}
