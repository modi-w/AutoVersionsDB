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

        public override List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename)
        {
            if (string.IsNullOrWhiteSpace(targetScriptFilename))
            {
                throw new ArgumentNullException(nameof(targetScriptFilename));
            }
            if (targetScriptFilename.Trim().ToUpperInvariant() == NoneRuntimeScriptFile.TargetNoneScriptFileName.Trim().ToUpperInvariant())
            {
                return new List<RuntimeScriptFileBase>();
            }

            RuntimeScriptFileBase targetRuntimeScriptFile = GetTargetRuntimeScriptFile(targetScriptFilename);

            if (targetRuntimeScriptFile == null)
            {
                //Comment: targetScriptFile can be null if the user send #Last file, but thie is no file.
                return new List<RuntimeScriptFileBase>();
            }


            List<RuntimeScriptFileBase> tempPendingScriptFilesList = new List<RuntimeScriptFileBase>();

            tempPendingScriptFilesList.AddRange(ChangedFiles);
            tempPendingScriptFilesList.AddRange(NotExistInDBButExistInFileSystem);


            List<RuntimeScriptFileBase> pendingScriptFilesList =
                FilterPendingScriptsFilesByTarget(null, targetRuntimeScriptFile, tempPendingScriptFilesList);

            return pendingScriptFilesList;

        }
    }
}
