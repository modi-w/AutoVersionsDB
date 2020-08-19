using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DBStateValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationEngine(CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
            : base(null)
        {
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(dbStateValidationStep);
        }
    }
}
