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
    public class ChangeProjectCodeProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public ChangeProjectCodeProcessDefinition(ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeNotExistValidationStep,
                                                        ChangeProjectCodeStep changeProjectCodeStep)
            : base()
        {
            AddStep(projectCodeNotExistValidationStep);
            AddStep(changeProjectCodeStep);
        }
    }
}
