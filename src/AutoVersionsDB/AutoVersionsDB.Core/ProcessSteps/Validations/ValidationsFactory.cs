using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public abstract  class ValidationsFactory
    {
        internal abstract string ValidationName { get; }

        internal abstract ValidationsGroup Create(ProcessContext processContext);
    }
}
