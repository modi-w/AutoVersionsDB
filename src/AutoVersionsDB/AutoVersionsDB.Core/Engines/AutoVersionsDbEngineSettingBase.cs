using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public abstract class AutoVersionsDbEngineSettingBase : EngineSettings
    {
        public bool IsVirtualExecution { get; protected set; }

        public AutoVersionsDbEngineSettingBase(ActionStepBase rollbackStep)
            :base(rollbackStep)
        {

        }
    }
}
