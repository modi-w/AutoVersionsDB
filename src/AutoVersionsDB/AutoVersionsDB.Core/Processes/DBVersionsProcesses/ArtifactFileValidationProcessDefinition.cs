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
    public class ArtifactFileValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Artifact File Validation";


        public ArtifactFileValidationProcessDefinition(ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                        ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(artifactFileValidationStep);
        }
    }
}
