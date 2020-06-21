using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class SystemTableValidationStepFluent
    {
        public static AutoVersionsDbEngine SystemTableValidation(this AutoVersionsDbEngine autoVersionsDbEngine, IDBCommands dbCommands, bool isDevEnvironment)
        {
            SystemTableValidationStep_Factory systemTableValidationSteFactory = NinjectUtils.KernelInstance.Get<SystemTableValidationStep_Factory>();

            ValidationsStep projectConfigValidationStep = systemTableValidationSteFactory.Create(dbCommands, isDevEnvironment);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }

    public class SystemTableValidationStep_Factory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public SystemTableValidationStep_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(IDBCommands dbCommands, bool isDevEnvironment)
        {
            List<ValidatorBase> validators = new List<ValidatorBase>();


            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(dbCommands, isDevEnvironment);
            validators.Add(systemTablesValidator);

            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep = new ValidationsStep(_notificationExecutersFactoryManager, false, singleValidationStep, validators);
            return validationStep;
        }

    }
}
