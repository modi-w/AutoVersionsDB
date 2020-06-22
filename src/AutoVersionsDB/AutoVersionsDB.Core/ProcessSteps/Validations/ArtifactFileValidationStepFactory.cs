using System.Collections.Generic;
using Ninject;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.Core.Utils;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class ArtifactFileValidationStepFluent
    {
        public static AutoVersionsDbEngine ArtifactFileValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ArtifactFileValidationStepFactory artifactFileValidationSteFactory = NinjectUtils.KernelInstance.Get<ArtifactFileValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = artifactFileValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class ArtifactFileValidationStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public ArtifactFileValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            List<ValidatorBase> validators = new List<ValidatorBase>();

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.IsDevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validators.Add(artifactFileValidator);

            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep =
                new ValidationsStep(_notificationExecutersFactoryManager, false, singleValidationStep, validators);

            return validationStep;
        }


    }
}
