using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.ExectutionParamsValidations
{
    public class TargetStateScriptFileExistValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;
        private readonly string _targetStateScriptFileName;

        public override string ValidatorName => "TargetStateScriptFileExist";

        public override string ErrorInstructionsMessage => "Target State Script Should Not Be Historical";


        public TargetStateScriptFileExistValidator(ScriptFilesState scriptFilesState,
                                                    string targetStateScriptFileName)
        {
            _scriptFilesState = scriptFilesState;
            _targetStateScriptFileName = targetStateScriptFileName;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_targetStateScriptFileName))
            {
                var isTargetFileExsit =
                    _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles
                        .Any(e => e.Filename.Trim().ToUpperInvariant() == _targetStateScriptFileName.Trim().ToUpperInvariant());

                if (!isTargetFileExsit)
                {
                    string errorMsg = $"The target file '{_targetStateScriptFileName}' is not exsit.";
                    return errorMsg;
                }
            }

            return "";
        }


    }
}
