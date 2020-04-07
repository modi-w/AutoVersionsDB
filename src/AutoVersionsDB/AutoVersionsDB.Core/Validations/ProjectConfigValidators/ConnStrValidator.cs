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
        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;

        public ConnStrValidator(string connStr,
                                string dbTypeCode,
                                DBCommands_FactoryProvider dbCommands_FactoryProvider)
        {
            _connStr = connStr;
            _dbTypeCode = dbTypeCode;
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
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
                using (IDBConnectionManager dbConnectionManager = _dbCommands_FactoryProvider.CreateDBConnectionManager(_dbTypeCode, _connStr, 0))
                {
                    if (dbConnectionManager != null)
                    {
                        string exMessage;
                        if (!dbConnectionManager.CheckConnection(out exMessage))
                        {
                            string errorMsg = string.Format("Could not connect to the Database with the Connection String: '{0}'. Error Message: '{1}' ", _connStr, exMessage);
                            return errorMsg;
                        }
                    }
                }

            }

            return "";
        }
    }
}
