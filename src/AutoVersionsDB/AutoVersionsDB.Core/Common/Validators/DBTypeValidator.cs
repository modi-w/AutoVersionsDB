using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBTypeValidator : ValidatorBase
    {
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactory dbCommandsFactoryProvider;

        public override string ValidatorName => "DBType";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DBTypeValidator(string dbTypeCode,
                                DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactoryProvider = dbCommandsFactory;
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
                if (!dbCommandsFactoryProvider.ContainDbType(_dbTypeCode))
                {
                    string errorMsg = $"DB Type Code '{_dbTypeCode}' is not valid";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
