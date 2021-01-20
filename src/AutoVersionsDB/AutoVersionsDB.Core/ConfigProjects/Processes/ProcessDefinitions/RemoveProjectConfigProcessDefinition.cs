﻿using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class RemoveProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public const string Name = "Save New Config Validation";
        public override string EngineTypeName => Name;


        public RemoveProjectConfigProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        RemoveProjectConfigStep removeProjectConfigStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(removeProjectConfigStep);
        }
    }
}
