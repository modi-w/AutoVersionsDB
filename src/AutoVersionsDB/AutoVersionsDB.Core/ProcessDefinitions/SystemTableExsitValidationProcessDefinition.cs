using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class SystemTableExsitValidationProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        public SystemTableExsitValidationProcessDefinition(CreateScriptFilesStateStep createScriptFilesStateStep,
                                                ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep)
            : base(null)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
        }
    }
}
