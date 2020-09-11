using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

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

        public override string Validate()
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
