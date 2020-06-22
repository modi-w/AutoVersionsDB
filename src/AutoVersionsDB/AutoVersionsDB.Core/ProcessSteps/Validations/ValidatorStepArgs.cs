﻿using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ValidatorStepArgs : ActionStepArgs
    {
        public ValidatorBase Validator { get; set; }

        public ValidatorStepArgs(ValidatorBase validator)
        {
            Validator = validator;
        }
    }
}