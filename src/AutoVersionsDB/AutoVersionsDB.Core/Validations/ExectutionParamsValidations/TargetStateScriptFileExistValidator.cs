using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class TargetStateScriptFileExistValidator : ValidatorBase
    {
        public override string ValidatorName => "TargetStateScriptFileExist";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";

        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        public TargetStateScriptFileExistValidator(ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (!string.IsNullOrWhiteSpace(executionParam.TargetStateScriptFileName))
            {
                var isTargetFileExsit =
                    _scriptFilesComparersProvider.IncrementalScriptFilesComparer.AllFileSystemScriptFiles
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
