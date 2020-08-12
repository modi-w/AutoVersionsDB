using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class SystemTableExsitValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        public SystemTableExsitValidationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep)
            : base(notificationExecutersProviderFactory, null)
        {
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(systemTableValidationStep);
        }
    }
}
