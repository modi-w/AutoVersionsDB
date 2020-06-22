using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Validations.ExectutionParamsValidations;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public static class TargetStateScriptFileValidationStepFluent
    {
        public static AutoVersionsDbEngine TargetStateScriptFileValidation(this AutoVersionsDbEngine autoVersionsDbEngine, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            scriptFilesComparersProvider.ThrowIfNull(nameof(scriptFilesComparersProvider));

            TargetStateScriptFileValidationStepFactory targetStateScriptFileigValidationSteFactory = NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationStepFactory>();

            ValidationsStep projectConfigValidationStep = targetStateScriptFileigValidationSteFactory.Create(scriptFilesComparersProvider);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }

    public class TargetStateScriptFileValidationStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public TargetStateScriptFileValidationStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ValidationsStep Create(ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            List<ValidatorBase> validators = new List<ValidatorBase>();


            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(scriptFilesComparersProvider);
            validators.Add(targetStateScriptFileExistValidator);

            IsTargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new IsTargetScriptFiletAlreadyExecutedValidator(scriptFilesComparersProvider);
            validators.Add(isTargetScriptFiletAlreadyExecutedValidator);


            SingleValidationStep singleValidationStep = new SingleValidationStep();

            ValidationsStep validationStep = new ValidationsStep(_notificationExecutersFactoryManager, true, singleValidationStep, validators);
            return validationStep;
        }

    }
}
