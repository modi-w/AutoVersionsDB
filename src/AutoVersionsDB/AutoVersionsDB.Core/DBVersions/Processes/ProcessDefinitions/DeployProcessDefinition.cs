﻿using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class DeployProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Deploy";


        public DeployProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
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