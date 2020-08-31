using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class DBStateValidationProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationProcessDefinition(CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
            : base(null)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(dbStateValidationStep);
        }
    }
}
