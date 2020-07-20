using AutoVersionsDB.Core.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoVersionsDB.Core.Utils;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class ScriptFilesComparerBase
    {
        protected ScriptFilesManager ScriptFilesManager { get; private set; }

        protected DBExecutedFilesManager DbExecutedFilesManager { get; private set; }

        public string LastFileOfLastExecutedFilename => DbExecutedFilesManager.LastFileOfLastExecutedFilename;

        public List<RuntimeScriptFileBase> AllFileSystemScriptFiles => ScriptFilesManager.ScriptFilesList;
        public List<RuntimeScriptFileBase> ExecutedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.Equal).ToList();
        public List<RuntimeScriptFileBase> ChangedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.Different).ToList();
        public List<RuntimeScriptFileBase> NotExistInDBButExistInFileSystem => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.NotExist).ToList();

        public List<RuntimeScriptFileBase> NotExistInFileSystemButExistInDB { get; private set; }

        public ScriptFilesComparerBase(ScriptFilesManager scriptFilesManager,
                                        DBExecutedFilesManager dbExecutedFilesManager)
        {
            scriptFilesManager.ThrowIfNull(nameof(scriptFilesManager));
            dbExecutedFilesManager.ThrowIfNull(nameof(dbExecutedFilesManager));

            this.ScriptFilesManager = scriptFilesManager;
            this.DbExecutedFilesManager = dbExecutedFilesManager;

            SetIsHashDifferentFlag();

            CreateFileExistInDBButNotExistInSystemList(scriptFilesManager);
        }


        private void SetIsHashDifferentFlag()
        {
            foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
            {
                DataRow lastExecutedCurrnetFileRow =
                            DbExecutedFilesManager.ExecutedFilesList
                            .LastOrDefault(row => Convert.ToString(row["Filename"], CultureInfo.InvariantCulture).Trim().ToUpperInvariant() == scriptFileItem.Filename.Trim().ToUpperInvariant());

                if (lastExecutedCurrnetFileRow == null)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.NotExist;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"], CultureInfo.InvariantCulture) != scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.Different;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"], CultureInfo.InvariantCulture) == scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.Equal;
                }
            }
        }

        private void CreateFileExistInDBButNotExistInSystemList(ScriptFilesManager scriptFilesManager)
        {
            NotExistInFileSystemButExistInDB = new List<RuntimeScriptFileBase>();

            foreach (DataRow dbExecutedFileRow in DbExecutedFilesManager.ExecutedFilesList)
            {
                string dbFilename = Convert.ToString(dbExecutedFileRow["Filename"], CultureInfo.InvariantCulture);

                RuntimeScriptFileBase fileSystemFile =
                    AllFileSystemScriptFiles.FirstOrDefault(e => dbFilename.Trim().ToUpperInvariant() == e.Filename.Trim().ToUpperInvariant());

                if (fileSystemFile == null)
                {
                    string fileFullPath = Path.Combine(scriptFilesManager.FolderPath, dbFilename);
                    RuntimeScriptFileBase misssingFileSystemFileItem = scriptFilesManager.CreateRuntimeScriptFileInstanceByFilename(fileFullPath);
                    NotExistInFileSystemButExistInDB.Add(misssingFileSystemFileItem);
                }

                if (dbFilename == DbExecutedFilesManager.LastFileOfLastExecutedFilename)
                {
                    break;
                }
            }
        }



        public abstract List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename);


        public RuntimeScriptFileBase CreateNextNewScriptFile(string scriptName)
        {
            RuntimeScriptFileBase lasetExecutedFileItem = CreateLasetExecutedFileItem();

            ScriptFilePropertiesBase lastExecutedFileProperties = null;
            if (lasetExecutedFileItem != null)
            {
                lastExecutedFileProperties = lasetExecutedFileItem.ScriptFileProperties;
            }

            return ScriptFilesManager.CreateNextNewScriptFile(lastExecutedFileProperties, scriptName);
        }


        protected RuntimeScriptFileBase CreateLasetExecutedFileItem()
        {
            RuntimeScriptFileBase prevExecutionLastScriptFile = null;
            if (!string.IsNullOrWhiteSpace(DbExecutedFilesManager.LastFileOfLastExecutedFilename))
            {
                string lastFileFullPath = Path.Combine(ScriptFilesManager.FolderPath, DbExecutedFilesManager.LastFileOfLastExecutedFilename);
                prevExecutionLastScriptFile = ScriptFilesManager.CreateRuntimeScriptFileInstanceByFilename(lastFileFullPath);
            }

            return prevExecutionLastScriptFile;
        }
    }
}



