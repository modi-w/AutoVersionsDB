using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public abstract class AutoVersionsDbProcessDefinition : ProcessDefinition
    {
        public bool IsVirtualExecution { get; protected set; }

        public AutoVersionsDbProcessDefinition(ActionStepBase rollbackStep)
            : base(rollbackStep)
        {

        }
    }
}
