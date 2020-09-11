using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class DBStateValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationProcessDefinition(CreateScriptFilesStateStep createScriptFilesStateStep,
                                                    ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                    SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                    ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(dbStateValidationStep);
        }
    }
}
