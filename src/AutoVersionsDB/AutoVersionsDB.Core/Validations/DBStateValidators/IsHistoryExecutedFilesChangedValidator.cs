using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.Validations.DBStateValidators
{
    public class IsHistoryExecutedFilesChangedValidator : ValidatorBase
    {
        public override string ValidatorName => "IsHistoryExecutedFilesChanged";

        public override string ErrorInstructionsMessage => "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'";


        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        public IsHistoryExecutedFilesChangedValidator(ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }


        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {

            if (_scriptFilesComparersProvider.IncrementalScriptFilesComparer.ChangedFiles.Count > 0)
            {
                IEnumerable<string> changeFilenamesList = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.ChangedFiles.Select(e => e.Filename);

                string errorMsg = $"The following files changed: '{string.Join(", ", changeFilenamesList)}'";
                return errorMsg;
            }

            if (!string.IsNullOrWhiteSpace(_scriptFilesComparersProvider.IncrementalScriptFilesComparer.LastFileOfLastExecutedFilename))
            {
                foreach (var scriptFileItem in _scriptFilesComparersProvider.IncrementalScriptFilesComparer.AllFileSystemScriptFiles)
                {
                    if (scriptFileItem.Filename.Trim().ToLower() == _scriptFilesComparersProvider.IncrementalScriptFilesComparer.LastFileOfLastExecutedFilename.Trim().ToLower())
                    {
                        break;
                    }
                    else
                    {
                        bool isFileNotExecuted = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.NotExistInDBButExistInFileSystem.Any(e => e.Filename.Trim().ToLower() == scriptFileItem.Filename.Trim().ToLower());

                        if (isFileNotExecuted)
                        {
                            string errorMsg = $"The history file '{scriptFileItem.Filename}' is not executed on this Database";
                            return errorMsg;
                        }
                    }
                }

                if (_scriptFilesComparersProvider.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Count > 0)
                {
                    IEnumerable<string> notExistInFileSystemFilenamesList = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Select(e => e.Filename);

                    string errorMsg = $"The following files changed: '{string.Join(", ", notExistInFileSystemFilenamesList)}'";
                    return errorMsg;
                }
            }


            return "";
        }




    }
}
