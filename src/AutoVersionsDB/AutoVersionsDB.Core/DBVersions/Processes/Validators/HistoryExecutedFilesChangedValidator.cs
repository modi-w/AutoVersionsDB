using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class HistoryExecutedFilesChangedValidator : ValidatorBase
    {
        private readonly ScriptFilesComparerBase _scriptFilesComparer;

        public const string Name = "HistoryExecutedFilesChanged";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.HistoryExecutedFilesChangedInstructionsMessage;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;


        public HistoryExecutedFilesChangedValidator(ScriptFilesComparerBase scriptFilesComparer)
        {
            _scriptFilesComparer = scriptFilesComparer;
        }


        public override string Validate()
        {

            if (_scriptFilesComparer.ChangedFiles.Count > 0)
            {
                IEnumerable<string> changeFilenamesList = _scriptFilesComparer.ChangedFiles.Select(e => e.Filename);

                return CoreTextResources.FilesChangedErrorMessage
                    .Replace("[FilesList]", string.Join(", ", changeFilenamesList))
                    .Replace("[FileTypeCode]", _scriptFilesComparer.ScriptFileType.FileTypeCode);
            }

            if (!string.IsNullOrWhiteSpace(_scriptFilesComparer.LastFileOfLastExecutedFilename))
            {
                foreach (var scriptFileItem in _scriptFilesComparer.AllFileSystemScriptFiles)
                {
                    if (scriptFileItem.Filename.Trim().ToUpperInvariant() == _scriptFilesComparer.LastFileOfLastExecutedFilename.Trim().ToUpperInvariant())
                    {
                        break;
                    }
                    else
                    {
                        bool isFileNotExecuted = _scriptFilesComparer.NotExistInDBButExistInFileSystem.Any(e => e.Filename.Trim().ToUpperInvariant() == scriptFileItem.Filename.Trim().ToUpperInvariant());

                        if (isFileNotExecuted)
                        {
                            return CoreTextResources.HistoryFilesNotExecutedErrorMessage
                                .Replace("[FileName]", scriptFileItem.Filename)
                                .Replace("[FileTypeCode]", _scriptFilesComparer.ScriptFileType.FileTypeCode);

                        }
                    }
                }

                if (_scriptFilesComparer.NotExistInFileSystemButExistInDB.Count > 0)
                {
                    IEnumerable<string> notExistInFileSystemFilenamesList = _scriptFilesComparer.NotExistInFileSystemButExistInDB.Select(e => e.Filename);
             
                    return CoreTextResources.HistoryExecutedFilesMissingErrorMessage
                        .Replace("[FilesList]", string.Join(", ", notExistInFileSystemFilenamesList))
                        .Replace("[FileTypeCode]", _scriptFilesComparer.ScriptFileType.FileTypeCode);
                }
            }


            return "";
        }




    }
}
