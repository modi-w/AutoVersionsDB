using AutoVersionsDB.Core.ScriptFiles.Incremental;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Incremental
{
    public class IncrementalScriptFilesComparer : ScriptFilesComparerBase
    {
        public IncrementalScriptFilesComparer(FileSystemScriptFiles fileSystemScriptFiles,
                                                DBExecutedFiles dbExecutedFiles)
            : base(fileSystemScriptFiles, dbExecutedFiles)
        {

        }

        public override List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename)
        {
            List<RuntimeScriptFileBase> pendingScriptFilesList = new List<RuntimeScriptFileBase>();

            if (targetScriptFilename != EmptyDbStateRuntimeScriptFile.TargetScriptFileNameEmptyDBState)
            {
                RuntimeScriptFileBase prevExecutionLastScriptFile = CreateLasetExecutedFileItem();

                RuntimeScriptFileBase targetScriptFile = null;
                if (!string.IsNullOrWhiteSpace(targetScriptFilename))
                {
                    string targetFileFullPath = Path.Combine(FileSystemScriptFiles.FolderPath, targetScriptFilename);
                    targetScriptFile = FileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(targetFileFullPath);
                }


                foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
                {
                    if ((prevExecutionLastScriptFile == null || 0 < string.Compare(scriptFileItem.SortKey, prevExecutionLastScriptFile.SortKey, StringComparison.Ordinal))
                        && (targetScriptFile == null || string.Compare(scriptFileItem.SortKey,targetScriptFile.SortKey,StringComparison.Ordinal) <= 0))
                    {
                        pendingScriptFilesList.Add(scriptFileItem);
                    }
                }
            }


            return pendingScriptFilesList;
        }




    }
}
