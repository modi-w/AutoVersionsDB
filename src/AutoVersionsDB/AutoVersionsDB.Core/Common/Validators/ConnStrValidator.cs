using AutoVersionsDB.Common;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ConnStrValidator : ValidatorBase
    {
        private readonly string _connStr;
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidatorName { get; }

        public override string ErrorInstructionsMessage => "Project Config Validation Error";



        public ConnStrValidator(string propertyName,
                                string connStr,
                                string dbTypeCode,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            ValidatorName = propertyName;
            _connStr = connStr;
            _dbTypeCode = dbTypeCode;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override string Validate()
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
