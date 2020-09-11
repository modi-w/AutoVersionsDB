using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class TargetStateScriptFileValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationProcessDefinition(ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                                ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(targetStateScriptFileValidationStep);
        }
    }
}
