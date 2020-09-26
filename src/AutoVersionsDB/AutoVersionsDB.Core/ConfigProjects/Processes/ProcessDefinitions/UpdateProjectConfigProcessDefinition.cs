using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class UpdateProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public UpdateProjectConfigProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        UpdateProjectConfigStep updateProjectConfigStep,
                                                        CreateMissingFoldersStep createMissingFoldersStep)
            : base()
        {
            AddStep(idNotExistValidationStep);
            AddStep(updateProjectConfigStep);
            AddStep(createMissingFoldersStep);
        }
    }
}
