using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public abstract class ValidationsStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => "Validation";

        private readonly NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
     
        protected abstract bool ShouldContinueWhenFindError { get; }
        protected List<ValidatorBase> Validators { get; }

        public ValidationsStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                        SingleValidationStep singleValidationStep)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            InternalNotificationableAction = singleValidationStep;
            
            Validators = new List<ValidatorBase>();
        }



        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            SetValidators(projectConfig);
        }

        protected abstract void SetValidators(ProjectConfigItem projectConfig);


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return Validators.Count;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(Validators.Count))
            {
                foreach (ValidatorBase validator in Validators)
                {
                    if (ShouldContinueWhenFindError
                        || !_notificationExecutersFactoryManager.HasError)
                    {
                        ValidatorStepArgs validatorStepArgs = new ValidatorStepArgs(validator);

                        notificationWrapperExecuter.ExecuteStep(InternalNotificationableAction, validator.ValidatorName, processState, validatorStepArgs);
                    }
                }
            }
        }


        #region IDisposable

        private bool _disposed = false;

        ~ValidationsStep() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                foreach (IDisposable validatorItem in Validators.Where(e=>e is IDisposable))
                {
                    validatorItem.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion

    }
}
