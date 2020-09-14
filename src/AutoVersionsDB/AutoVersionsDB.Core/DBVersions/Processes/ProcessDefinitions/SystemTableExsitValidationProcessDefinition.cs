using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class SystemTableExsitValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        public SystemTableExsitValidationProcessDefinition(ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
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
