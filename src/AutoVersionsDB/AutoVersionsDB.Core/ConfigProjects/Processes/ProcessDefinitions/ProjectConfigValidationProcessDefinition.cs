using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
