using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class HistoryExecutedFilesChangedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;

        public override string ValidatorName => "HistoryExecutedFilesChanged";

        public override string ErrorInstructionsMessage => "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'";



        public HistoryExecutedFilesChangedValidator(ScriptFilesState scriptFilesState)
        {
            _scriptFilesState = scriptFilesState;
        }


        public override string Validate()
        {

            if (_scriptFilesState.IncrementalScriptFilesComparer.ChangedFiles.Count > 0)
            {
                IEnumerable<string> changeFilenamesList = _scriptFilesState.IncrementalScriptFilesComparer.ChangedFiles.Select(e => e.Filename);

                string errorMsg = $"The following files changed: '{string.Join(", ", changeFilenamesList)}'";
                return errorMsg;
            }

            if (!string.IsNullOrWhiteSpace(_scriptFilesState.IncrementalScriptFilesComparer.LastFileOfLastExecutedFilename))
            {
                foreach (var scriptFileItem in _scriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles)
                {
                    if (scriptFileItem.Filename.Trim().ToUpperInvariant() == _scriptFilesState.IncrementalScriptFilesComparer.LastFileOfLastExecutedFilename.Trim().ToUpperInvariant())
                    {
                        break;
                    }
                    else
                    {
                        bool isFileNotExecuted = _scriptFilesState.IncrementalScriptFilesComparer.NotExistInDBButExistInFileSystem.Any(e => e.Filename.Trim().ToUpperInvariant() == scriptFileItem.Filename.Trim().ToUpperInvariant());

                        if (isFileNotExecuted)
                        {
                            string errorMsg = $"The history file '{scriptFileItem.Filename}' is not executed on this Database";
                            return errorMsg;
                        }
                    }
                }

                if (_scriptFilesState.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Count > 0)
                {
                    IEnumerable<string> notExistInFileSystemFilenamesList = _scriptFilesState.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Select(e => e.Filename);

                    string errorMsg = $"The following files missing from the scripts folder: '{string.Join(", ", notExistInFileSystemFilenamesList)}'";
                    return errorMsg;
                }
            }


            return "";
        }




    }
}
