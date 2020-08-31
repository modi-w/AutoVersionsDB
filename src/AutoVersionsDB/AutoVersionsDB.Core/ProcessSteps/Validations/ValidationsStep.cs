using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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

        public override string StepName => $"Validation - {_validationsFactory.ValidationName}";

        public ValidationsStep(SingleValidationStepFactory singleValidationStepFactory, ValidationsFactory validationsFactory)
        {
            _validationsFactory = validationsFactory;

            _singleValidationStepFactory = singleValidationStepFactory;

        }


        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = _validationsFactory.Create(processContext.ProjectConfig, processContext);

            foreach (ValidatorBase validator in validationsGroup.GetValidators())
            {
                SingleValidationStep singleValidationStep = _singleValidationStepFactory.Create(validator);
                AddInternalStep(singleValidationStep);
            }

            base.ExecuteInternalSteps(true);
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
