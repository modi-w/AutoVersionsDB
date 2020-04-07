using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DBTypeValidator : ValidatorBase
    {
        public override string ValidatorName => "DBTypeCode";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        private string _dbTypeCode;
        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;

        public DBTypeValidator(string dbTypeCode,
                                DBCommands_FactoryProvider dbCommands_FactoryProvider)
        {
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
            _dbTypeCode = dbTypeCode;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (string.IsNullOrWhiteSpace(_dbTypeCode))
            {
                string errorMsg = "DB Type is empty";
                return errorMsg;
            }
            else
            {
                if (!_dbCommands_FactoryProvider.DBCommands_FactoryDictionary.ContainsKey(_dbTypeCode))
                {
                    string errorMsg = $"DB Type Code '{_dbTypeCode}' is not valid";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
