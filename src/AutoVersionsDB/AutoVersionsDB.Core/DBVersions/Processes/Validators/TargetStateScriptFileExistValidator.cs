using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class TargetStateScriptFileExistValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;
        private readonly string _targetStateScriptFileName;

        public const string Name = "TargetStateScriptFileExist";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.TargetStateScriptFileNotExistInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public TargetStateScriptFileExistValidator(ScriptFilesState scriptFilesState,
                                                    string targetStateScriptFileName)
        {
            _scriptFilesState = scriptFilesState;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_targetStateScriptFileName)
                && _targetStateScriptFileName.Trim().ToUpperInvariant() != RuntimeScriptFile.TargetLastScriptFileName.Trim().ToUpperInvariant())
            {
                var isTargetFileExsit =
                    _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == _targetStateScriptFileName.Trim().ToUpperInvariant());

                if (!isTargetFileExsit)
                {
                    return CoreTextResources.TargetStateScriptFileNotExistErrorMessage.Replace("[FileName]", _targetStateScriptFileName);
                }
            }

            return "";
        }


    }
}
