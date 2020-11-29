﻿using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class NextScriptFileNameValidator : ValidatorBase
    {
        private readonly ScriptFilesComparerBase _scriptFilesComparer;
        private readonly string _scriptName;

        public override string ValidatorName => "Next Runtime Script Filename";

        public override string ErrorInstructionsMessage => "Invalid script name";


        public NextScriptFileNameValidator(ScriptFilesComparerBase scriptFilesComparer,
                                                    string scriptName)
        {
            _scriptFilesComparer = scriptFilesComparer;
            _scriptName = scriptName;
        }

        public override string Validate()
        {

            if (string.IsNullOrWhiteSpace(_scriptName))
            {
                string errorMsg = "Script Name is mandatory";
                return errorMsg;
            }
            else if (!_scriptFilesComparer.TryParseNextRuntimeScriptFileName(_scriptName, out RuntimeScriptFileBase newRuntimeScriptFile))
            {
                string errorMsg = $"Filename '{newRuntimeScriptFile.Filename}' not valid for script type: '{newRuntimeScriptFile.ScriptFileType.FileTypeCode}'. Should be like the following pattern: '{newRuntimeScriptFile.ScriptFileType.FilenamePattern}'";
                return errorMsg;
            }


            return "";
        }
    }
}
