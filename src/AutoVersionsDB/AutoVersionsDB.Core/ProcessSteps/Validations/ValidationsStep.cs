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
    public class ValidationsStep : AutoVersionsDbStep, IDisposable
    {
        private ValidationsFactory _validationsFactory;
        private SingleValidationStepFactory _singleValidationStepFactory;

        public override string StepName => "Validation";
        public override bool HasInternalStep => true;

        protected bool ShouldContinueWhenFindError { get; }
        protected List<ValidatorBase> Validators { get; }

        public ValidationsStep(SingleValidationStepFactory singleValidationStepFactory, ValidationsFactory validationsFactory)
        {
            _validationsFactory = validationsFactory;

            _singleValidationStepFactory = singleValidationStepFactory;

            Validators = new List<ValidatorBase>();
        }



        public override int GetNumOfInternalSteps(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            return _validationsFactory.Create(projectConfig, processState).Count;
        }


        public override void Execute(ProjectConfig projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(Validators.Count))
            {
                List<ValidatorBase> validators = _validationsFactory.Create(projectConfig, processState);

                foreach (ValidatorBase validator in validators)
                {
                    if (ShouldContinueWhenFindError
                        || !notificationExecutersProvider.NotifictionStatesHistory.HasError)
                    {
                        SingleValidationStep singleValidationStep = _singleValidationStepFactory.Create(validator);

                        notificationWrapperExecuter.ExecuteStep(singleValidationStep, projectConfig, processState);
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

                foreach (IDisposable validatorItem in Validators.Where(e => e is IDisposable))
                {
                    validatorItem.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion

    }


    public class ValidationsStep<TValidationsFactory> : ValidationsStep
        where TValidationsFactory : ValidationsFactory
    {


        public ValidationsStep(SingleValidationStepFactory singleValidationStepFactory, TValidationsFactory validationsFactory)
            : base(singleValidationStepFactory, validationsFactory)
        {

        }

    }
}
