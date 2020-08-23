using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class ProjectConfigValidationEngine : AutoVersionsDbEngineSettingBase
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationEngine(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep)
            : base(null)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
