using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBBackupFolderValidator : ValidatorBase
    {
        private readonly string _dbBackupBaseFolder;

        public const string Name = "DBBackupFolderPath";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public DBBackupFolderValidator(string dbBackupBaseFolder)
        {
            _dbBackupBaseFolder = dbBackupBaseFolder;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbBackupBaseFolder))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "DB Backup Folder Path");
            }
            else
            {

                //Comment: We dont want to check if the folder is exist. 
                // 1. we creating it if it dont exist
                // 2. if it run on the first time on a machine (like on github action), the run will fall here.
                //      and we dont have a way to create the folder


            }

            return "";
        }
    }
}
