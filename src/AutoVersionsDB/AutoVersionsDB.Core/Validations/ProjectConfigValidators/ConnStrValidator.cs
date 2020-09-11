using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ConnStrValidator : ValidatorBase
    {
        private readonly string _connStr;
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        internal override string ValidatorName { get; }

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";



        internal ConnStrValidator(string propertyName,
                                string connStr,
                                string dbTypeCode,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            ValidatorName = propertyName;
            _connStr = connStr;
            _dbTypeCode = dbTypeCode;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        internal override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_connStr))
            {
                string errorMsg = "Connection String is empty";
                return errorMsg;
            }
            else
            {
                using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(_dbTypeCode, _connStr, 0).AsDisposable())
                {
                    if (dbConnection != null)
                    {
                        if (!dbConnection.Instance.CheckConnection(out string exMessage))
                        {
                            string errorMsg = $"Could not connect to the Database with the Connection String: '{_connStr}'. Error Message: '{exMessage}'";
                            return errorMsg;
                        }
                    }
                }

            }

            return "";
        }
    }
}
