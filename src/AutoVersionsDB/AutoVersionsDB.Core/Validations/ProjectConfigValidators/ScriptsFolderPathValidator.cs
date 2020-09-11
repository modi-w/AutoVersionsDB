using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ScriptsFolderPathValidator : ValidatorBase
    {
        private readonly string _scriptFolderPath;

        internal override string ValidatorName { get; }

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";



        internal ScriptsFolderPathValidator(string propertyName,
                                            string scriptFolderPath)
        {
            ValidatorName = propertyName;
            _scriptFolderPath = scriptFolderPath;
        }

        internal override string Validate()
        {
            if (!Directory.Exists(_scriptFolderPath))
            {
                string errorMsg = $"'{_scriptFolderPath}' Scripts Folder is not exist";
                return errorMsg;
            }

            return "";
        }
    }
}
