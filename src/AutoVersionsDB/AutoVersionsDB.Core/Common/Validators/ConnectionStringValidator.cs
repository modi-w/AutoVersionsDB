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

        public const string Name = "ConnectionString";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

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
                            string errorMsg = CoreTextResources
                                .ConnectionStringValidatorErrorMessage
                                .Replace("[DBConnectionInfo]", _dbConnectionInfo.ToString())
                                .Replace("[ConnectionString]", dbConnection.ConnectionString)
                                .Replace("[exMessage]", exMessage);

                            return errorMsg;
                        }
                    }
                }
            }


            return "";
        }
    }
}
