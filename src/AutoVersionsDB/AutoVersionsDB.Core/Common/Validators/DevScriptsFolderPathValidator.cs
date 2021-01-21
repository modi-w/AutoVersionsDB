using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DevScriptsBaseFolderPathValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _devScriptsBaseFolderPath;

        public const string Name = "DevScriptsBaseFolder";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

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
                    return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "Scripts Folder Path");
                }
                else
                {
                    if (!Directory.Exists(_devScriptsBaseFolderPath))
                    {
                        return CoreTextResources.DevScriptsFolderNotExistErrorMessage;
                    }
                }
            }

            return "";
        }
    }
}
