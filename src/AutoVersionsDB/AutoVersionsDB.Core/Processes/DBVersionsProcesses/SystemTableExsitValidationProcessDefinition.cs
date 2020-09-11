using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class SystemTableExsitValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        public SystemTableExsitValidationProcessDefinition(ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                            SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
        }
    }
}
