using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class SingleValidationStep : AutoVersionsDbStep
    {
        private readonly ValidatorBase _validator;


        public override string StepName => _validator.ValidatorName;



        public SingleValidationStep(ValidatorBase validator)
        {
            _validator = validator;

        }



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            string errorMsg = _validator.Validate(processContext.ProcessParams as AutoVersionsDbProcessParams);

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                throw new NotificationProcessException(_validator.ValidatorName, errorMsg, _validator.ErrorInstructionsMessage);
            }
        }



    }
}
