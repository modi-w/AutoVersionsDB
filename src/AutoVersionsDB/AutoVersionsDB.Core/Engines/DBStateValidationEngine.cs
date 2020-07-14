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


        public DBStateValidationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                        ScriptFilesComparersManager scriptFilesComparersManager,
                                        DBStateValidationStep dbStateValidationStep)
            : base(notificationExecutersFactoryManager, null, scriptFilesComparersManager)
        {
            ProcessSteps.Add(dbStateValidationStep);
        }
    }
}
