using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DBBackupFolderValidator : ValidatorBase
    {
        private readonly string _dbBackupBaseFolder;

        public override string ValidatorName => "DBBackupFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DBBackupFolderValidator(string dbBackupBaseFolder)
        {
            _dbBackupBaseFolder = dbBackupBaseFolder;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_dbBackupBaseFolder))
            {
                string errorMsg = "DB Backup Folder Path is empty";
                return errorMsg;
            }
            else
            {
                if (!Directory.Exists(_dbBackupBaseFolder))
                {
                    string errorMsg = $"DB Backup Folder '{_dbBackupBaseFolder}' is not exist";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
