using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DeployEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName =>  "Deploy";


        public DeployEngine(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                            CreateScriptFilesStateStep createScriptFilesStateStep,
                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                            BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(null)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(buildDeployArtifactFileStep);
        }
    }
}
