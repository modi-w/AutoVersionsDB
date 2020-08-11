using AutoVersionsDB.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class SingleValidationStepFactory
    {
        public SingleValidationStepFactory()
        {

        }

#pragma warning disable CA1822 // Mark members as static
        public SingleValidationStep Create(ValidatorBase validator)
#pragma warning restore CA1822 // Mark members as static
        {
            return new SingleValidationStep(validator);
        }
    }
}
