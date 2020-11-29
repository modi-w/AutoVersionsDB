using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ConnectionStringValidator : ValidatorBase
    {
        private readonly DBConnectionInfo _dbConnectionInfo;
        private readonly DBCommandsFactory dbCommandsFactoryProvider;

        public override string ValidatorName => "ConnectionString";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";



        public ConnectionStringValidator(DBConnectionInfo dbConnectionInfo,
                                        DBCommandsFactory dbCommandsFactory)
        {
            _dbConnectionInfo = dbConnectionInfo;
            dbCommandsFactoryProvider = dbCommandsFactory;
        }

        public override string Validate()
        {
            if (_dbConnectionInfo.HasValues)
            {
                using (var dbConnection = dbCommandsFactoryProvider.CreateDBConnection(_dbConnectionInfo))
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
