using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
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
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            dbCommands.ThrowIfNull(nameof(dbCommands));

            SystemTableValidationStepFactory systemTableValidationSteFactory = NinjectUtils.KernelInstance.Get<SystemTableValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = systemTableValidationSteFactory.Create(dbCommands, isDevEnvironment);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }

    public class SystemTableValidationStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public SystemTableValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
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
