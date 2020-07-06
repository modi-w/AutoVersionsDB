using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public abstract class AutoVersionsDbStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ProjectConfigItem>
    {
    }
}
