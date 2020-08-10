using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ConnStrValidator : ValidatorBase
    {
        public override string ValidatorName => "ConnStr";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        private string _connStr;
        private string _dbTypeCode;
        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;

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
                        string exMessage;
                        if (!dbConnection.CheckConnection(out exMessage))
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
