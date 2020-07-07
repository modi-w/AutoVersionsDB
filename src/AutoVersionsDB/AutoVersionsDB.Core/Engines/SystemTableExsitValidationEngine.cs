using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class SystemTableExsitValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        public SystemTableExsitValidationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                SystemTableValidationStep systemTableValidationStep)
            : base(notificationExecutersFactoryManager, null)
        {
            ProcessSteps.Add(systemTableValidationStep);
        }
    }
}
