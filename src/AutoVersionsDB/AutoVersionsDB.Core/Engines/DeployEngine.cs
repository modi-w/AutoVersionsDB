using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DeployEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Deploy";


        public DeployEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                            ProjectConfigValidationStep projectConfigValidationStep,
                            CheckDeliveryEnvValidationStep checkDeliveryEnvValidationStep,
                            SystemTableValidationStep systemTableValidationStep,
                            DBStateValidationStep dbStateValidationStep,
                            BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(notificationExecutersFactoryManager, null)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(buildDeployArtifactFileStep);
        }
    }
}
