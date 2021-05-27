using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ScriptsFolderPathValidator : ValidatorBase
    {
        private readonly string _scriptFolderPath;

        public override string ValidatorName { get; }

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;


        public ScriptsFolderPathValidator(string propertyName,
                                            string scriptFolderPath)
        {
            ValidatorName = propertyName;
            _scriptFolderPath = scriptFolderPath;
        }

        public override string Validate()
        {
            if (!Directory.Exists(_scriptFolderPath))
            {
                return CoreTextResources.ScriptTypeFolderNotExistErrorMessage.Replace("[ScriptFolderPath]", _scriptFolderPath);
            }

            return "";
        }
    }
}
