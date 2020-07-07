using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class TargetStateScriptFileValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                    TargetStateScriptFileValidationStep targetStateScriptFileValidationStep)
            : base(notificationExecutersFactoryManager, null)
        {
            ProcessSteps.Add(targetStateScriptFileValidationStep);
        }
    }
}
