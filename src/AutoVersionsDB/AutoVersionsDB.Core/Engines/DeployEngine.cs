using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class DeployEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Deploy";


        public DeployEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                            ScriptFilesComparersManager scriptFilesComparersManager,
                            ProjectConfigValidationStep projectConfigValidationStep,
                            CheckDeliveryEnvValidationStep checkDeliveryEnvValidationStep,
                            SystemTableValidationStep systemTableValidationStep,
                            DBStateValidationStep dbStateValidationStep,
                            BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(notificationExecutersFactoryManager, null, scriptFilesComparersManager)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(buildDeployArtifactFileStep);
        }
    }
}
