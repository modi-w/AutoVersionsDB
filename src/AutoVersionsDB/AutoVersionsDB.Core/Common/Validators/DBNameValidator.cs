using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBNameValidator : ValidatorBase
    {
        private readonly string _dbName;

        public override string ValidatorName => "DBName";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DBNameValidator(string dbName)
        {
            _dbName = dbName;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbName))
            {
                string errorMsg = "DB Name is mandatory";
                return errorMsg;
            }

            return "";
        }
    }
}
