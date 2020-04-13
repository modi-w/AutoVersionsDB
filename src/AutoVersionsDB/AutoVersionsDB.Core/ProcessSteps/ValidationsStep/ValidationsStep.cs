using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public class ValidationsStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Validation";

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private bool _shouldContinueWhenFindError;
        private List<ValidatorBase> _validators;

        public ValidationsStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                        bool shouldContinueWhenFindError,
                                        SingleValidationStep singleValidationStep,
                                        List<ValidatorBase> validators)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _shouldContinueWhenFindError = shouldContinueWhenFindError;
            InternalNotificationableAction = singleValidationStep;
            _validators = validators;
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return _validators.Count;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(_validators.Count))
            {
                foreach (ValidatorBase validator in _validators)
                {
                    if (_shouldContinueWhenFindError
                        || !_notificationExecutersFactoryManager.HasError)
                    {
                        ValidatorStepArgs validatorStepArgs = new ValidatorStepArgs(validator);

                        notificationWrapperExecuter.ExecuteStep(InternalNotificationableAction, validator.ValidatorName, processState, validatorStepArgs);
                    }
                }
            }
        }
    }
}
