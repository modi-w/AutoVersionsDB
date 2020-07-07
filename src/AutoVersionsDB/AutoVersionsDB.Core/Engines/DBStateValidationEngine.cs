using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DBStateValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                        DBStateValidationStep dbStateValidationStep)
            : base(notificationExecutersFactoryManager, null)
        {
            ProcessSteps.Add(dbStateValidationStep);
        }
    }
}
