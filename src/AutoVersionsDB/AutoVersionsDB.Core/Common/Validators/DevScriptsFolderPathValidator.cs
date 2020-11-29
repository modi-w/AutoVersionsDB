using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DevScriptsBaseFolderPathValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _devScriptsBaseFolderPath;

        public override string ValidatorName => "DevScriptsBaseFolder";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DevScriptsBaseFolderPathValidator(bool isDevEnvironment,
                                                    string devScriptsBaseFolderPath)
        {
            _isDevEnvironment = isDevEnvironment;
            _devScriptsBaseFolderPath = devScriptsBaseFolderPath;
        }

        public override string Validate()
        {
            if (_isDevEnvironment)
            {
                if (string.IsNullOrWhiteSpace(_devScriptsBaseFolderPath))
                {
                    string errorMsg = "Scripts Folder Path is mandatory";
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
