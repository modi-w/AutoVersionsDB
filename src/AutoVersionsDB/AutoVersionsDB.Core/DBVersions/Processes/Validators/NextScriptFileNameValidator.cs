using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class NextScriptFileNameValidator : ValidatorBase
    {
        private readonly ScriptFilesComparerBase _scriptFilesComparer;
        private readonly string _scriptName;

        public const string Name = "NextRuntimeScriptFilename";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.NextScriptFileNameInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public NextScriptFileNameValidator(ScriptFilesComparerBase scriptFilesComparer,
                                                    string scriptName)
        {
            _scriptFilesComparer = scriptFilesComparer;
            _scriptName = scriptName;
        }

        public override string Validate()
        {

            if (string.IsNullOrWhiteSpace(_scriptName))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "Script Name");
            }
            else if (!_scriptFilesComparer.TryParseNextRuntimeScriptFileName(_scriptName, out RuntimeScriptFileBase newRuntimeScriptFile))
            {
                return CoreTextResources
                    .InvalidFilenameErrorMessage
                    .Replace("[Filename]", newRuntimeScriptFile.Filename)
                    .Replace("[FileTypeCode]", newRuntimeScriptFile.ScriptFileType.FileTypeCode)
                    .Replace("[FilenamePattern]", newRuntimeScriptFile.ScriptFileType.FilenamePattern);
            }


            return "";
        }
    }
}
