using System;
using System.Collections.Generic;
using System.IO;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable
{
    public class RepeatableScriptFilesComparer : ScriptFilesComparerBase
    {
        public RepeatableScriptFilesComparer(FileSystemScriptFiles fileSystemScriptFiles,
                                                DBExecutedFiles dbExecutedFiles)
            : base(fileSystemScriptFiles, dbExecutedFiles)
        {

        }

        public override List<RuntimeScriptFile> GetPendingFilesToExecute(string targetScriptFilename)
        {
            //if (string.IsNullOrWhiteSpace(targetScriptFilename))
            //{
            //    throw new ArgumentNullException(nameof(targetScriptFilename));
            //}
            //if (targetScriptFilename.Trim().ToUpperInvariant() == RuntimeScriptFile.TargetNoneScriptFileName.Trim().ToUpperInvariant())
            //{
            //    return new List<RuntimeScriptFile>();
            //}

            //RuntimeScriptFile targetRuntimeScriptFile = GetTargetRuntimeScriptFile(targetScriptFilename);

            //if (targetRuntimeScriptFile == null)
            //{
            //    //Comment: targetScriptFile can be null if the user send #Last file, but thie is no file.
            //    return new List<RuntimeScriptFile>();
            //}


            List<RuntimeScriptFile> pendingScriptFilesList = new List<RuntimeScriptFile>();

            pendingScriptFilesList.AddRange(ChangedFiles);
            pendingScriptFilesList.AddRange(NotExistInDBButExistInFileSystem);


            //List<RuntimeScriptFile> pendingScriptFilesList =
            //    FilterPendingScriptsFilesByTarget(null, targetRuntimeScriptFile, tempPendingScriptFilesList);

            return pendingScriptFilesList;

        }
    }
}
