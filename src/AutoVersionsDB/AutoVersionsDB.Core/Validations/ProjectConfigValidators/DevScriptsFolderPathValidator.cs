using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DevScriptsBaseFolderPathValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _devScriptsBaseFolderPath;

        internal override string ValidatorName => "DevScriptsBaseFolder";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal DevScriptsBaseFolderPathValidator(bool isDevEnvironment,
                                                    string devScriptsBaseFolderPath)
        {
            _isDevEnvironment= isDevEnvironment;
            _devScriptsBaseFolderPath = devScriptsBaseFolderPath;
        }

        internal override string Validate()
        {
            if (_isDevEnvironment)
            {
                if (string.IsNullOrWhiteSpace(_devScriptsBaseFolderPath))
                {
                    string errorMsg = "Scripts Folder Path is empty";
                    return errorMsg;
                }
                else
                {
                    if (!Directory.Exists(_devScriptsBaseFolderPath))
                    {
                        string errorMsg = "Scripts Folder is not exist";
                        return errorMsg;
                    }
                }
            }

            return "";
        }
    }
}
