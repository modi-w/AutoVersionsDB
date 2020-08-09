using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class TargetStateScriptFileValidationEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                                        ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(notificationExecutersProviderFactory, null)
        {
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(targetStateScriptFileValidationStep);
        }
    }
}
