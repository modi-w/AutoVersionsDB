using AutoVersionsDB.Common;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class IsTargetScriptFiletAlreadyExecutedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;
        private readonly string _targetStateScriptFileName;

        public override string ValidatorName => "IsTargetScriptFiletAlreadyExecuted";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";


        public IsTargetScriptFiletAlreadyExecutedValidator(ScriptFilesState scriptFilesState,
                                                            string targetStateScriptFileName)
        {
            _scriptFilesState = scriptFilesState;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_targetStateScriptFileName))
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
