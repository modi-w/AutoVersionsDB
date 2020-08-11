using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class IsTargetScriptFiletAlreadyExecutedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;

        public override string ValidatorName => "IsTargetScriptFiletAlreadyExecuted";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";


        public IsTargetScriptFiletAlreadyExecutedValidator(ScriptFilesState scriptFilesState)
        {
            _scriptFilesState = scriptFilesState;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            executionParam.ThrowIfNull(nameof(executionParam));

            if (!string.IsNullOrWhiteSpace(executionParam.TargetStateScriptFileName))
            {
                var isTargetFileExecuted =
                    _scriptFilesState.IncrementalScriptFilesComparer.ExecutedFiles
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == executionParam.TargetStateScriptFileName.Trim().ToUpperInvariant());

                if (isTargetFileExecuted)
                {

                    string errorMsg = $"The target file '{executionParam.TargetStateScriptFileName}' is already executed on this database.";
                    return errorMsg;
                }
            }

            return "";
        }


    }
}
