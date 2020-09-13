using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.ProcessSteps.ProjectConfig;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class UpdateProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public UpdateProjectConfigProcessDefinition(ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeNotExistValidationStep,
                                                        UpdateProjectConfigStep updateProjectConfigStep)
            : base()
        {
            AddStep(projectCodeNotExistValidationStep);
            AddStep(updateProjectConfigStep);
        }
    }
}
