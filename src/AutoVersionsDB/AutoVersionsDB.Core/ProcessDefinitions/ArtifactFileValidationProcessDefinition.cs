using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class ArtifactFileValidationProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "Artifact File Validation";


        public ArtifactFileValidationProcessDefinition(ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep)
            : base(null)
        {
            AddStep(artifactFileValidationStep);
        }
    }
}
