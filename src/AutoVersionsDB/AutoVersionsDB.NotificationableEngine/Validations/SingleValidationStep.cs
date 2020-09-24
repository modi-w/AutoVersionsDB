using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.NotificationableEngine.Validations
{
    public class SingleValidationStep : ActionStepBase
    {
        private readonly ValidatorBase _validator;


        public override string StepName => _validator.ValidatorName;



        public SingleValidationStep(ValidatorBase validator)
        {
            _validator = validator;

        }



        public override void Execute(ProcessContext processContext)
        {
            string errorMsg = _validator.Validate();

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                throw new NotificationProcessException(_validator.ValidatorName, errorMsg, _validator.ErrorInstructionsMessage);
            }
        }



    }
}
