using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ConnStrValidator : ValidatorBase
    {
        private readonly string _connStr;
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidatorName => "ConnStr";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";



        public ConnStrValidator(string connStr,
                                string dbTypeCode,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _connStr = connStr;
            _dbTypeCode = dbTypeCode;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (string.IsNullOrWhiteSpace(_connStr))
            {
                string errorMsg = "Connection String is empty";
                return errorMsg;
            }
            else
            {
                using (IDBConnection dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(_dbTypeCode, _connStr, 0))
                {
                    if (dbConnection != null)
                    {
                        if (!dbConnection.CheckConnection(out string exMessage))
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
