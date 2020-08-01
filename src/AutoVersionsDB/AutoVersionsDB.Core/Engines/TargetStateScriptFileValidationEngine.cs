using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class TargetStateScriptFileValidationEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                                     ScriptFilesComparersManager scriptFilesComparersManager,
                                                     TargetStateScriptFileValidationStep targetStateScriptFileValidationStep)
            : base(notificationExecutersProviderFactory, null, scriptFilesComparersManager)
        {
            ProcessSteps.Add(targetStateScriptFileValidationStep);
        }
    }
}
