using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep)
            : base(null)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
