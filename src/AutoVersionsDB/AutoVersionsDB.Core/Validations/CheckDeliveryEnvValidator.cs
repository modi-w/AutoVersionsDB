using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations
{
    public class CheckDeliveryEnvValidator : ValidatorBase
    {
        internal override string ValidatorName => "DeliveryEnvironment";

        internal override string ErrorInstructionsMessage => "Could not run this command on Delivery Environment";

        private readonly bool _isDevEnvironment;

        internal CheckDeliveryEnvValidator(bool isDevEnvironment)
        {
            _isDevEnvironment = isDevEnvironment;
        }

        internal override string Validate()
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
