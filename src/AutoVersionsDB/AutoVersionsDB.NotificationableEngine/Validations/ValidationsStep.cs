﻿using AutoVersionsDB.Common;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public class ValidationsStep : ActionStepBase
    {
        private readonly ValidationsFactory _validationsFactory;
        private readonly SingleValidationStepFactory _singleValidationStepFactory;

        public override string StepName => $"Validation - {_validationsFactory.ValidationName}";

        public ValidationsStep(SingleValidationStepFactory singleValidationStepFactory, ValidationsFactory validationsFactory)
        {
            _validationsFactory = validationsFactory;

            _singleValidationStepFactory = singleValidationStepFactory;

        }


        public override void Execute(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = _validationsFactory.Create(processContext);

            foreach (ValidatorBase validator in validationsGroup.GetValidators())
            {
                SingleValidationStep singleValidationStep = _singleValidationStepFactory.Create(validator);
                AddInternalStep(singleValidationStep);
            }

            ExecuteInternalSteps(true);
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