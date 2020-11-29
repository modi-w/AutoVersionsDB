using System.Collections.Generic;

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
            List<RuntimeScriptFileBase> pendingScriptFilesList = new List<RuntimeScriptFileBase>();

            pendingScriptFilesList.AddRange(ChangedFiles);
            pendingScriptFilesList.AddRange(NotExistInDBButExistInFileSystem);

            return pendingScriptFilesList;
        }
    }
}
