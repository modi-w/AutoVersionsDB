using System.Collections.Generic;
using Ninject;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public static class ArtifactFileValidationStepFluent
    {
        public static AutoVersionsDbEngine ArtifactFileValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            ArtifactFileValidationStep_Factory artifactFileValidationSteFactory = NinjectUtils.KernelInstance.Get<ArtifactFileValidationStep_Factory>();

            ValidationsStep projectConfigValidationStep = artifactFileValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class ArtifactFileValidationStep_Factory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public ArtifactFileValidationStep_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
        {
            List<ValidatorBase> validators = new List<ValidatorBase>();

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.IsDevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validators.Add(artifactFileValidator);

            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep = 
                new ValidationsStep(_notificationExecutersFactoryManager,false, singleValidationStep, validators);

            return validationStep;
        }


    }
}
