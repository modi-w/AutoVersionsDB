using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DBBackupFolderValidator : ValidatorBase
    {
        private readonly string _dbBackupBaseFolder;

        internal override string ValidatorName => "DBBackupFolderPath";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal DBBackupFolderValidator(string dbBackupBaseFolder)
        {
            _dbBackupBaseFolder = dbBackupBaseFolder;
        }

        internal override string Validate()
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
