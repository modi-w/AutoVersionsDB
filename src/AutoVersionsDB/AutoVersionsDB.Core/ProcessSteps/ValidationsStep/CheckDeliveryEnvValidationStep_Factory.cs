using System.Collections.Generic;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using Ninject;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public static class CheckDeliveryEnvValidationStepFluent
    {
        public static AutoVersionsDbEngine CheckDeliveryEnvValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ProjectConfigItem projectConfig)
        {
            CheckDeliveryEnvValidationStep_Factory checkDeliveryEnvValidationSteFactory = NinjectUtils.KernelInstance.Get<CheckDeliveryEnvValidationStep_Factory>();

            ValidationsStep projectConfigValidationStep = checkDeliveryEnvValidationSteFactory.Create(projectConfig);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class CheckDeliveryEnvValidationStep_Factory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public CheckDeliveryEnvValidationStep_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ProjectConfigItem projectConfig)
        {
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
