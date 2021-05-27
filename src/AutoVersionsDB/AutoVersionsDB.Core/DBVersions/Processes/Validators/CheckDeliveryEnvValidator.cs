using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class CheckDeliveryEnvValidator : ValidatorBase
    {
        public const string Name = "DeliveryEnvironment";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.DeliveryEnvErrorMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;


        private readonly bool _isDevEnvironment;

        public CheckDeliveryEnvValidator(bool isDevEnvironment)
        {
            _isDevEnvironment = isDevEnvironment;
        }

        public override string Validate()
        {
            if (!_isDevEnvironment)
            {
                return CoreTextResources.DeliveryEnvErrorMessage;
            }

            return "";
        }
    }
}
