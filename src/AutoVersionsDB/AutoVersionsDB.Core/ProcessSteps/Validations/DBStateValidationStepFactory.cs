using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Validations.DBStateValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class DBStateValidationStepFluent
    {
        public static AutoVersionsDbEngine DBStateValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            scriptFilesComparersProvider.ThrowIfNull(nameof(scriptFilesComparersProvider));

            DBStateValidationStepFactory dbStateValidationSteFactory = NinjectUtils.KernelInstance.Get<DBStateValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = dbStateValidationSteFactory.Create(scriptFilesComparersProvider);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class DBStateValidationStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public DBStateValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            List<ValidatorBase> validators = new List<ValidatorBase>();

            IsHistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator = new IsHistoryExecutedFilesChangedValidator(scriptFilesComparersProvider);
            validators.Add(isHistoryExecutedFilesChangedValidator);

            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep =
                new ValidationsStep(_notificationExecutersFactoryManager, false, singleValidationStep, validators);

            return validationStep;
        }


    }
}
