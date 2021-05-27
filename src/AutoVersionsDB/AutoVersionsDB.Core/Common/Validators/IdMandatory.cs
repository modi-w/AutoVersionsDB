using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class IdMandatory : ValidatorBase
    {
        private readonly string _id;

        public const string Name = "IdMandatory";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public IdMandatory(string id)
        {
            _id = id;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_id))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "Id");
            }

            return "";
        }
    }
}
