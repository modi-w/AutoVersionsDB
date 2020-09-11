using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ProjectConfig;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class DeployProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Deploy";


        public DeployProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                        ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                        BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
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
