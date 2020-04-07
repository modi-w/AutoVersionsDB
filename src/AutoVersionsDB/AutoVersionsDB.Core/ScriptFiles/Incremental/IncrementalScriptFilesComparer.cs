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
        public IncrementalScriptFilesComparer(ScriptFilesManager scriptFilesManager,
                                                DBExecutedFilesManager dbExecutedFilesManager)
            : base(scriptFilesManager, dbExecutedFilesManager)
        {

        }

        public override List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename)
        {
            List<RuntimeScriptFileBase> pendingScriptFilesList = new List<RuntimeScriptFileBase>();

            if (targetScriptFilename != EmptyDbStateRuntimeScriptFile.C_TargetScriptFileName_EmptyDBState)
            {
                RuntimeScriptFileBase prevExecutionLastScriptFile = createLasetExecutedFileItem();

                RuntimeScriptFileBase targetScriptFile = null;
                if (!string.IsNullOrWhiteSpace(targetScriptFilename))
                {
                    string targetFileFullPath = Path.Combine(_scriptFilesManager.FolderPath, targetScriptFilename);
                    targetScriptFile = _scriptFilesManager.CreateRuntimeScriptFileInstanceByFilename(targetFileFullPath);
                }


                foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
                {
                    if ((prevExecutionLastScriptFile == null || 0 < scriptFileItem.SortKey.CompareTo(prevExecutionLastScriptFile.SortKey))
                        && (targetScriptFile == null || scriptFileItem.SortKey.CompareTo(targetScriptFile.SortKey) <= 0))
                    {
                        pendingScriptFilesList.Add(scriptFileItem);
                    }
                }
            }


            return pendingScriptFilesList;
        }

       


    }
}
