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


        public ArtifactFileValidationEngine(ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep)
            : base(null)
        {
            ProcessSteps.Add(artifactFileValidationStep);
        }
    }
}
