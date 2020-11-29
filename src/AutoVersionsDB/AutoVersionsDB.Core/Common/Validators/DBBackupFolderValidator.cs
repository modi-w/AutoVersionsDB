using AutoVersionsDB.NotificationableEngine.Validations;

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
                string errorMsg = "DB Backup Folder Path is mandatory";
                return errorMsg;
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
