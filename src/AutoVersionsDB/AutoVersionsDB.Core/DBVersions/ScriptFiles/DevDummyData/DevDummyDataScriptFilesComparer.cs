using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData
{
    public class DevDummyDataScriptFilesComparer : ScriptFilesComparerBase
    {
        public DevDummyDataScriptFilesComparer(FileSystemScriptFiles fileSystemScriptFiles,
                                                DBExecutedFiles dbExecutedFiles)
            : base(fileSystemScriptFiles, dbExecutedFiles)
        {

        }


        public override List<RuntimeScriptFile> GetPendingFilesToExecute(string targetScriptFilename)
        {
            RuntimeScriptFile prevExecutionLastScriptFile = CreateLasetExecutedFileItem();

            List<RuntimeScriptFile> pendingScriptFilesList =
                FilterPendingScriptsFilesByTarget(prevExecutionLastScriptFile, null, AllFileSystemScriptFiles);

            return pendingScriptFilesList;
        }
    }
}
