using System.Collections.Generic;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using Ninject;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class CheckDeliveryEnvValidationStepFluent
    {
        public static AutoVersionsDbEngine CheckDeliveryEnvValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            projectConfig.ThrowIfNull(nameof(projectConfig));

            CheckDeliveryEnvValidationStepFactory checkDeliveryEnvValidationSteFactory = NinjectUtils.KernelInstance.Get<CheckDeliveryEnvValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = checkDeliveryEnvValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class CheckDeliveryEnvValidationStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public CheckDeliveryEnvValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            List<ValidatorBase> validators = new List<ValidatorBase>();

            CheckDeliveryEnvValidator checkDeliveryEnvValidator = new CheckDeliveryEnvValidator(projectConfig.IsDevEnvironment);
            validators.Add(checkDeliveryEnvValidator);

            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep =
                new ValidationsStep(_notificationExecutersFactoryManager, false, singleValidationStep, validators);

            return validationStep;
        }


    }
}
