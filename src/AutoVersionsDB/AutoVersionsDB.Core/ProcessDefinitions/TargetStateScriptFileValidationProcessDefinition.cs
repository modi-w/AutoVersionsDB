using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class TargetStateScriptFileValidationProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationProcessDefinition(CreateScriptFilesStateStep createScriptFilesStateStep,
                                                        ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(null)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(targetStateScriptFileValidationStep);
        }
    }
}
