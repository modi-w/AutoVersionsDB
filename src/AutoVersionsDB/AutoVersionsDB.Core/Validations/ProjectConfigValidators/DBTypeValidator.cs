using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DBTypeValidator : ValidatorBase
    {
        private readonly string _dbTypeCode;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidatorName => "DBTypeCode";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DBTypeValidator(string dbTypeCode,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _dbTypeCode = dbTypeCode;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
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
