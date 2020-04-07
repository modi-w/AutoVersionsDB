using AutoVersionsDB.Core.Engines;

namespace AutoVersionsDB.Core.Validations
{
    public class CheckDeliveryEnvValidator : ValidatorBase
    {
        public override string ValidatorName => "DeliveryEnvironment";

        public override string ErrorInstructionsMessage => "Could not run this command on Delivery Environment";

        private bool _isDevEnvironment;

        public CheckDeliveryEnvValidator(bool isDevEnvironment)
        {
            _isDevEnvironment = isDevEnvironment;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (!_isDevEnvironment)
            {
                string errorMsg = "Could not run this command on Delivery Environment";
                return errorMsg;
            }

            return "";
        }
    }
}
