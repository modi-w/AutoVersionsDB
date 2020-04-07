using AutoVersionsDB.Core.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public abstract class ScriptFilesComparerBase
    {
        protected ScriptFilesManager _scriptFilesManager { get; private set; }

        protected DBExecutedFilesManager _dbExecutedFilesManager;

        public string LastFileOfLastExecutedFilename => _dbExecutedFilesManager.LastFileOfLastExecutedFilename;

        public List<RuntimeScriptFileBase> AllFileSystemScriptFiles => _scriptFilesManager.ScriptFilesList;
        public List<RuntimeScriptFileBase> ExecutedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.Equal).ToList();
        public List<RuntimeScriptFileBase> ChangedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.Different).ToList();
        public List<RuntimeScriptFileBase> NotExistInDBButExistInFileSystem => AllFileSystemScriptFiles.Where(e => e.HashDiffType == eHashDiffType.NotExist).ToList();

        public List<RuntimeScriptFileBase> NotExistInFileSystemButExistInDB { get; private set; }

        public ScriptFilesComparerBase(ScriptFilesManager scriptFilesManager,
                                        DBExecutedFilesManager dbExecutedFilesManager)
        {
            _scriptFilesManager = scriptFilesManager;
            _dbExecutedFilesManager = dbExecutedFilesManager;

            setIsHashDifferentFlag();

            createNotExistInFileSystemButExistInDB(scriptFilesManager);
        }

     
        private void setIsHashDifferentFlag()
        {
            foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
            {
                DataRow lastExecutedCurrnetFileRow =
                            _dbExecutedFilesManager.ExecutedFilesList
                            .LastOrDefault(row => Convert.ToString(row["Filename"]).Trim().ToLower() == scriptFileItem.Filename.Trim().ToLower());

                if (lastExecutedCurrnetFileRow == null)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.NotExist;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"]) != scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.Different;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"]) == scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = eHashDiffType.Equal;
                }
            }
        }

        private void createNotExistInFileSystemButExistInDB(ScriptFilesManager scriptFilesManager)
        {
            NotExistInFileSystemButExistInDB = new List<RuntimeScriptFileBase>();

            foreach (DataRow dbExecutedFileRow in _dbExecutedFilesManager.ExecutedFilesList)
            {
                string dbFilename = Convert.ToString(dbExecutedFileRow["Filename"]);

                RuntimeScriptFileBase fileSystemFile =
                    AllFileSystemScriptFiles.FirstOrDefault(e => dbFilename.Trim().ToLower() == e.Filename.Trim().ToLower());

                if (fileSystemFile == null)
                {
                    string fileFullPath = Path.Combine(scriptFilesManager.FolderPath, dbFilename);
                    RuntimeScriptFileBase misssingFileSystemFileItem = scriptFilesManager.CreateRuntimeScriptFileInstanceByFilename(fileFullPath);
                    NotExistInFileSystemButExistInDB.Add(misssingFileSystemFileItem);
                }

                if(dbFilename == _dbExecutedFilesManager.LastFileOfLastExecutedFilename)
                {
                    break;
                }
            }
        }



        public abstract List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename);


        public RuntimeScriptFileBase CreateNextNewScriptFile(string scriptName)
        {
            RuntimeScriptFileBase lasetExecutedFileItem = createLasetExecutedFileItem();

            ScriptFilePropertiesBase lastExecutedFileProperties = null;
            if (lasetExecutedFileItem != null)
            {
                lastExecutedFileProperties = lasetExecutedFileItem.ScriptFileProperties;
            }

            return _scriptFilesManager.CreateNextNewScriptFile(lastExecutedFileProperties, scriptName);
        }


        protected RuntimeScriptFileBase createLasetExecutedFileItem()
        {
            RuntimeScriptFileBase prevExecutionLastScriptFile = null;
            if (!string.IsNullOrWhiteSpace(_dbExecutedFilesManager.LastFileOfLastExecutedFilename))
            {
                string lastFileFullPath = Path.Combine(_scriptFilesManager.FolderPath, _dbExecutedFilesManager.LastFileOfLastExecutedFilename);
                prevExecutionLastScriptFile = _scriptFilesManager.CreateRuntimeScriptFileInstanceByFilename(lastFileFullPath);
            }

            return prevExecutionLastScriptFile;
        }
    }
}



