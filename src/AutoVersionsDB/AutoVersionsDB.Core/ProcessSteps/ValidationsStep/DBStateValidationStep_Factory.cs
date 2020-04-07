using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Validations.DBStateValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public static class DBStateValidationStepFluent
    {
        public static AutoVersionsDbEngine DBStateValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            DBStateValidationStep_Factory dbStateValidationSteFactory = NinjectUtils.KernelInstance.Get<DBStateValidationStep_Factory>();

            ValidationsStep projectConfigValidationStep = dbStateValidationSteFactory.Create(scriptFilesComparersProvider);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }


    public class DBStateValidationStep_Factory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;

        public DBStateValidationStep_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                        DBCommands_FactoryProvider dbCommands_FactoryProvider)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
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
