using AutoVersionsDB.Core.ProcessDefinitions;

namespace AutoVersionsDB.Core.Validations
{
    public class CheckDeliveryEnvValidator : ValidatorBase
    {
        public override string ValidatorName => "DeliveryEnvironment";

        public override string ErrorInstructionsMessage => "Could not run this command on Delivery Environment";

        private readonly bool _isDevEnvironment;

        public CheckDeliveryEnvValidator(bool isDevEnvironment)
        {
            _isDevEnvironment = isDevEnvironment;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
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
