using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ConnectionStringValidator : ValidatorBase
    {
        private readonly DBConnectionInfo _dbConnectionInfo;
        private readonly DBCommandsFactory _dbCommandsFactory;

        public override string ValidatorName => "ConnectionString";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;


        public ConnectionStringValidator(DBConnectionInfo dbConnectionInfo,
                                        DBCommandsFactory dbCommandsFactory)
        {
            _dbConnectionInfo = dbConnectionInfo;
            _dbCommandsFactory = dbCommandsFactory;
        }

        public override string Validate()
        {
            if (_dbConnectionInfo.HasValues)
            {
                using (var dbConnection = _dbCommandsFactory.CreateDBConnection(_dbConnectionInfo))
                {
                    if (dbConnection != null)
                    {
                        if (!dbConnection.CheckConnection(out string exMessage))
                        {
                            string errorMsg = $"Could not connect to the Database with the following properties: {_dbConnectionInfo}, Check each of the above properties. The used connection String was: '{dbConnection.ConnectionString}'. Error Message: '{exMessage}'";
                            return errorMsg;
                        }
                    }
                }
            }


            return "";
        }
    }
}
