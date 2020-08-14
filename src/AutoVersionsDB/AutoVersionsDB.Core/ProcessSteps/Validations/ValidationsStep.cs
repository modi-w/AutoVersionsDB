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
    public class ValidationsStep : AutoVersionsDbStep
    {
        private readonly ValidationsFactory _validationsFactory;
        private readonly SingleValidationStepFactory _singleValidationStepFactory;

        public override string StepName => "Validation";
        public override bool HasInternalStep => true;


        public ValidationsStep(SingleValidationStepFactory singleValidationStepFactory, ValidationsFactory validationsFactory)
        {
            _validationsFactory = validationsFactory;

            _singleValidationStepFactory = singleValidationStepFactory;

        }



        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            return _validationsFactory.Create(projectConfig, processState).Count;
        }


        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            notificationExecutersProvider.ThrowIfNull(nameof(notificationExecutersProvider));
            processState.ThrowIfNull(nameof(processState));

            ValidationsGroup validationsGroup = _validationsFactory.Create(projectConfig, processState);

            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(validationsGroup.Count))
            {

                foreach (ValidatorBase validator in validationsGroup.GetValidators())
                {
                    if (validationsGroup.ShouldContinueWhenFindError
                        || !notificationExecutersProvider.ProcessTrace.HasError)
                    {
                        SingleValidationStep singleValidationStep = _singleValidationStepFactory.Create(validator);

                        notificationWrapperExecuter.ExecuteStep(singleValidationStep, projectConfig, processState);
                    }
                }
            }
        }



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
