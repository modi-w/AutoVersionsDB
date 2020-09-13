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
    public class SaveNewProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public SaveNewProjectConfigProcessDefinition(ValidationsStep<ProjectCodeNotExistValidationsFactory> projectCodeNotExistValidationStep,
                                                        SaveNewProjectConfigStep saveNewProjectConfigStep)
            : base()
        {
            AddStep(projectCodeNotExistValidationStep);
            AddStep(saveNewProjectConfigStep);
        }
    }
}
