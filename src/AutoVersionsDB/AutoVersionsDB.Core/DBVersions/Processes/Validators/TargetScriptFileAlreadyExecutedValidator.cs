using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class TargetScriptFileAlreadyExecutedValidator : ValidatorBase
    {
        private readonly ScriptFilesComparerBase _scriptFilesComparer;
        private readonly string _targetStateScriptFileName;

        public const string Name = "TargetScriptFileAlreadyExecuted";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.HistoricalTargetStateScriptInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public TargetScriptFileAlreadyExecutedValidator(ScriptFilesComparerBase scriptFilesComparer,
                                                            string targetStateScriptFileName)
        {
            _scriptFilesComparer = scriptFilesComparer;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_targetStateScriptFileName)
                && _targetStateScriptFileName.Trim().ToUpperInvariant() != RuntimeScriptFile.TargetLastScriptFileName.Trim().ToUpperInvariant())
            {
                var isTargetFileExecuted =
                    _scriptFilesComparer.ExecutedFilesAll
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == _targetStateScriptFileName.Trim().ToUpperInvariant());

                if (isTargetFileExecuted)
                {
                    return CoreTextResources.HistoricalTargetStateScriptErrorMessage
                        .Replace("[FileName]", _targetStateScriptFileName)
                        .Replace("[FileTypeCode]", _scriptFilesComparer.ScriptFileType.FileTypeCode);
                }
            }

            return "";
        }


    }
}
