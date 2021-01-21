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

        public const string Name = "TargetScriptFileAlreadyExecuted";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.HistoricalTargetStateScriptInstructionsMessage;

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
                && _targetStateScriptFileName.Trim().ToUpperInvariant() != RuntimeScriptFileBase.TargetLastScriptFileName.Trim().ToUpperInvariant())
            {
                var isTargetFileExecuted =
                    _scriptFilesState.IncrementalScriptFilesComparer.ExecutedFilesAll
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == _targetStateScriptFileName.Trim().ToUpperInvariant());

                if (isTargetFileExecuted)
                {
                    return CoreTextResources.HistoricalTargetStateScriptErrorMessage.Replace("[FileName]", _targetStateScriptFileName);
                }
            }

            return "";
        }


    }
}
