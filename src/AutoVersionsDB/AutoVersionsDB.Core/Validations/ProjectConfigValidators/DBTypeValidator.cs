using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DBTypeValidator : ValidatorBase
    {
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        internal override string ValidatorName => "DBTypeCode";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal DBTypeValidator(string dbTypeCode,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _dbTypeCode = dbTypeCode;
        }

        internal override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbTypeCode))
            {
                string errorMsg = "DB Type is empty";
                return errorMsg;
            }
            else
            {
                if (!_dbCommandsFactoryProvider.ContainDbType(_dbTypeCode))
                {
                    string errorMsg = $"DB Type Code '{_dbTypeCode}' is not valid";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
