using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class TargetScriptFiletAlreadyExecutedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;
        private readonly string _targetStateScriptFileName;

        public override string ValidatorName => "TargetScriptFileAlreadyExecuted";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public TargetScriptFiletAlreadyExecutedValidator(ScriptFilesState scriptFilesState,
                                                            string targetStateScriptFileName)
        {
            _scriptFilesState = scriptFilesState;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_targetStateScriptFileName)
                && _targetStateScriptFileName.Trim().ToUpperInvariant() != LastRuntimeScriptFile.TargetLastScriptFileName.Trim().ToUpperInvariant())
            {
                var isTargetFileExecuted =
                    _scriptFilesState.IncrementalScriptFilesComparer.ExecutedFiles
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == _targetStateScriptFileName.Trim().ToUpperInvariant());

                if (isTargetFileExecuted)
                {

                    string errorMsg = $"The target file '{_targetStateScriptFileName}' is already executed on this database.";
                    return errorMsg;
                }
            }

            return "";
        }


    }
}
