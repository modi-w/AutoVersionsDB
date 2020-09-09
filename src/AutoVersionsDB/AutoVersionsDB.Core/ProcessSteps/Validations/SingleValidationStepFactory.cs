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

        public virtual SingleValidationStep Create(ValidatorBase validator)
        {
            return new SingleValidationStep(validator);
        }
    }
}
