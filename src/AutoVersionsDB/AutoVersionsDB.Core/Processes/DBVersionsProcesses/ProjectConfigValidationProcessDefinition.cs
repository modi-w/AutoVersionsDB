using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                        ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
