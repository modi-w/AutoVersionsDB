using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationProcessDefinition(ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep)
            : base(null, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
