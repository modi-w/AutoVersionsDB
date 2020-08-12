﻿using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DBStateValidationEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
            : base(notificationExecutersProviderFactory, null)
        {
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(dbStateValidationStep);
        }
    }
}
