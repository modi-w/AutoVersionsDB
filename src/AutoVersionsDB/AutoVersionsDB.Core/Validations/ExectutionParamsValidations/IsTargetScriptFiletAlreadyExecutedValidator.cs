using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class IsTargetScriptFiletAlreadyExecutedValidator : ValidatorBase
    {
        public override string ValidatorName => "IsTargetScriptFiletAlreadyExecuted";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";

        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        public IsTargetScriptFiletAlreadyExecutedValidator(ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (!string.IsNullOrWhiteSpace(executionParam.TargetStateScriptFileName))
            {
                var isTargetFileExecuted =
                    _scriptFilesComparersProvider.IncrementalScriptFilesComparer.ExecutedFiles
                        .Any(e => e.Filename.Trim().ToLower() == executionParam.TargetStateScriptFileName.Trim().ToLower());

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
