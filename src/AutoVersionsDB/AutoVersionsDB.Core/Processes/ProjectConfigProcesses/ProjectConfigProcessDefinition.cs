using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public abstract class ProjectConfigProcessDefinition : ProcessDefinition
    {
        public ProjectConfigProcessDefinition()
            : base(null)
        {
        }
    }
}
