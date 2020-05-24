using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles.Repeatable
{
    public class RepeatableScriptFilesComparer : ScriptFilesComparerBase
    {
        public RepeatableScriptFilesComparer(ScriptFilesManager scriptFilesManager,
                                                DBExecutedFilesManager dbExecutedFilesManager)
            : base (scriptFilesManager, dbExecutedFilesManager)
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
