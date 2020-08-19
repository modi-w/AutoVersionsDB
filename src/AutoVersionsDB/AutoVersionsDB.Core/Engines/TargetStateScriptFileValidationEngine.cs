using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class TargetStateScriptFileValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationEngine(CreateScriptFilesStateStep createScriptFilesStateStep,
                                                        ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(null)
        {
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(targetStateScriptFileValidationStep);
        }
    }
}
