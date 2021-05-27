using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBNameValidator : ValidatorBase
    {
        private readonly string _dbName;

        public const string Name = "DBName";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public DBNameValidator(string dbName)
        {
            _dbName = dbName;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbName))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "DB Name");
            }

            return "";
        }
    }
}
