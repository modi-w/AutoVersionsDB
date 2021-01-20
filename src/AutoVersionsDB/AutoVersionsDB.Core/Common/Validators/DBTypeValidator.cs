using AutoVersionsDB.DB;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBTypeValidator : ValidatorBase
    {
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactory _dbCommandsFactory;

        public const string Name = "DBType";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public DBTypeValidator(string dbTypeCode,
                                DBCommandsFactory dbCommandsFactory)
        {
            _dbCommandsFactory = dbCommandsFactory;
            _dbTypeCode = dbTypeCode;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbTypeCode))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "DB Type");
            }
            else
            {
                if (!_dbCommandsFactory.ContainDBType(_dbTypeCode))
                {
                    return CoreTextResources.DBTypeCodeNotValidErrorMessage.Replace("[DBTypeCode]", _dbTypeCode);
                }
            }

            return "";
        }
    }
}
