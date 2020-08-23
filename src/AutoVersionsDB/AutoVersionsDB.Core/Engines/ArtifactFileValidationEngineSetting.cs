using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class ArtifactFileValidationEngineSetting : AutoVersionsDbEngineSettingBase
    {
        public override string EngineTypeName => "Artifact File Validation";


        public ArtifactFileValidationEngineSetting(ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep)
            : base(null)
        {
            AddStep(artifactFileValidationStep);
        }
    }
}
