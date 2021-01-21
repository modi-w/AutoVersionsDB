using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class HistoryExecutedFilesChangedValidator : ValidatorBase
    {
        private readonly ScriptFilesState _scriptFilesState;

        public const string Name = "HistoryExecutedFilesChanged";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.HistoryExecutedFilesChangedInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;


        public HistoryExecutedFilesChangedValidator(ScriptFilesState scriptFilesState)
        {
            _scriptFilesState = scriptFilesState;
        }


        public override string Validate()
        {

            if (_scriptFilesState.IncrementalScriptFilesComparer.ChangedFiles.Count > 0)
            {
                IEnumerable<string> changeFilenamesList = _scriptFilesState.IncrementalScriptFilesComparer.ChangedFiles.Select(e => e.Filename);

                return CoreTextResources.FilesChangedErrorMessage.Replace("[FilesList]", string.Join(", ", changeFilenamesList));
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
                            return CoreTextResources.HistoryFilesNotExecutedErrorMessage.Replace("[FileName]", scriptFileItem.Filename);
                        }
                    }
                }

                if (_scriptFilesState.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Count > 0)
                {
                    IEnumerable<string> notExistInFileSystemFilenamesList = _scriptFilesState.IncrementalScriptFilesComparer.NotExistInFileSystemButExistInDB.Select(e => e.Filename);
             
                    return CoreTextResources.HistoryExecutedFilesMissingErrorMessage.Replace("[FilesList]", string.Join(", ", notExistInFileSystemFilenamesList));
                 }
            }


            return "";
        }




    }
}
