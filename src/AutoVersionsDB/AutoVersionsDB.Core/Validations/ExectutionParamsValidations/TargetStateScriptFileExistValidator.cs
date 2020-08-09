using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class TargetStateScriptFileExistValidator : ValidatorBase
    {
        public override string ValidatorName => "TargetStateScriptFileExist";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";

        private ScriptFilesState _scriptFilesState;

        public TargetStateScriptFileExistValidator(ScriptFilesState scriptFilesState)
        {
            _scriptFilesState = scriptFilesState;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            executionParam.ThrowIfNull(nameof(executionParam));

            if (!string.IsNullOrWhiteSpace(executionParam.TargetStateScriptFileName))
            {
                var isTargetFileExsit =
                    _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == executionParam.TargetStateScriptFileName.Trim().ToUpperInvariant());

                if (!isTargetFileExsit)
                {
                    string errorMsg = $"The target file '{executionParam.TargetStateScriptFileName}' is not exsit.";
                    return errorMsg;
                }
            }

            return "";
        }


    }
}
