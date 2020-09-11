using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.ScriptFiles;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class IsTargetScriptFiletAlreadyExecutedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;
        private readonly string _targetStateScriptFileName;

        internal override string ValidatorName => "IsTargetScriptFiletAlreadyExecuted";

        internal override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";


        internal IsTargetScriptFiletAlreadyExecutedValidator(ScriptFilesState scriptFilesState,
                                                            string targetStateScriptFileName)
        {
            _scriptFilesState = scriptFilesState;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        internal override string Validate()
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
