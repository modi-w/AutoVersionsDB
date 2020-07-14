using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class ArtifactFileValidationEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Artifact File Validation";


        public ArtifactFileValidationEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            ArtifactFileValidationStep artifactFileValidationStep)
            : base(notificationExecutersFactoryManager, null)
        {
            ProcessSteps.Add(artifactFileValidationStep);
        }
    }
}
