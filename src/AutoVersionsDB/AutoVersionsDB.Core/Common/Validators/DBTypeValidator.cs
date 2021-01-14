using AutoVersionsDB.DB;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBTypeValidator : ValidatorBase
    {
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactory _dbCommandsFactory;

        public override string ValidatorName => "DBType";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

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
                string errorMsg = "DB Type is mandatory";
                return errorMsg;
            }
            else
            {
                if (!_dbCommandsFactory.ContainDBType(_dbTypeCode))
                {
                    string errorMsg = $"DB Type Code '{_dbTypeCode}' is not valid";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
