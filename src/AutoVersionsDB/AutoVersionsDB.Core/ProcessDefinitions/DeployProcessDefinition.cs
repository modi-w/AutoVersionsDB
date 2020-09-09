using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessDefinitions
{
    public class DeployProcessDefinition : AutoVersionsDbProcessDefinition
    {
        public override string EngineTypeName => "Deploy";


        public DeployProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                            CreateScriptFilesStateStep createScriptFilesStateStep,
                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                            BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(null)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
            AddStep(buildDeployArtifactFileStep);
        }
    }
}
