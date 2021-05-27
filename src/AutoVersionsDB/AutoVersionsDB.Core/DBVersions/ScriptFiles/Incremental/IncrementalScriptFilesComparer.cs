using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental
{
    public class IncrementalScriptFilesComparer : ScriptFilesComparerBase
    {
        public IncrementalScriptFilesComparer(FileSystemScriptFiles fileSystemScriptFiles,
                                                DBExecutedFiles dbExecutedFiles)
            : base(fileSystemScriptFiles, dbExecutedFiles)
        {

        }

        public override List<RuntimeScriptFile> GetPendingFilesToExecute(string targetScriptFilename)
        {
            if (string.IsNullOrWhiteSpace(targetScriptFilename))
            {
                throw new ArgumentNullException(nameof(targetScriptFilename));
            }
            if (targetScriptFilename.Trim().ToUpperInvariant() == RuntimeScriptFile.TargetNoneScriptFileName.Trim().ToUpperInvariant())
            {
                return new List<RuntimeScriptFile>();
            }

            RuntimeScriptFile targetRuntimeScriptFile = GetTargetRuntimeScriptFile(targetScriptFilename);

            if (targetRuntimeScriptFile == null)
            {
                //Comment: targetScriptFile can be null if the user send #Last file, but thie is no file.
                return new List<RuntimeScriptFile>();
            }

            RuntimeScriptFile prevExecutionLastScriptFile = CreateLasetExecutedFileItem();


            List<RuntimeScriptFile> pendingScriptFilesList =
                FilterPendingScriptsFilesByTarget(prevExecutionLastScriptFile, targetRuntimeScriptFile, AllFileSystemScriptFiles);

            return pendingScriptFilesList;
        }

    }
}
