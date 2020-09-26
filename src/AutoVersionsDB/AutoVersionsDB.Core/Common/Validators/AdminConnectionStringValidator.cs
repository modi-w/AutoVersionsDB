using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class AdminConnectionStringValidator : ValidatorBase
    {
        private readonly DBConnectionInfo _dbConnectionInfo;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidatorName => "AdminConnectionString";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";



        public AdminConnectionStringValidator(DBConnectionInfo dbConnectionInfo,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbConnectionInfo = dbConnectionInfo;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override string Validate()
        {
            using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(_dbConnectionInfo).AsDisposable())
            {
                if (dbConnection != null)
                {
                    if (!dbConnection.Instance.CheckConnection(out string exMessage))
                    {
                        string errorMsg = $"Could not connect to the Database with the following connection String: '{dbConnection.Instance.ConnectionString}'. Error Message: '{exMessage}'";
                        return errorMsg;
                    }
                }
            }


            return "";
        }
    }
}
