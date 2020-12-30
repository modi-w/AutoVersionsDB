using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public abstract class ScriptFilesComparerBase
    {
        protected FileSystemScriptFiles FileSystemScriptFiles { get; private set; }

        protected DBExecutedFiles DBExecutedFiles { get; private set; }

        public ScriptFileTypeBase ScriptFileType { get; }


        public string LastFileOfLastExecutedFilename => DBExecutedFiles.LastFileOfLastExecutedFilename;

        public List<RuntimeScriptFileBase> AllFileSystemScriptFiles => FileSystemScriptFiles.ScriptFilesList;
        public List<RuntimeScriptFileBase> ExecutedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == HashDiffType.Equal).ToList();
        public List<RuntimeScriptFileBase> ChangedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == HashDiffType.Different).ToList();
        public List<RuntimeScriptFileBase> NotExistInDBButExistInFileSystem => AllFileSystemScriptFiles.Where(e => e.HashDiffType == HashDiffType.NotExist).ToList();

        public List<RuntimeScriptFileBase> NotExistInFileSystemButExistInDB { get; private set; }

        public RuntimeScriptFileBase LastScriptFile => AllFileSystemScriptFiles.LastOrDefault();


        public ScriptFilesComparerBase(FileSystemScriptFiles fileSystemScriptFiles,
                                        DBExecutedFiles dbExecutedFiles)
        {
            fileSystemScriptFiles.ThrowIfNull(nameof(fileSystemScriptFiles));
            dbExecutedFiles.ThrowIfNull(nameof(dbExecutedFiles));

            ScriptFileType = fileSystemScriptFiles.ScriptFileType;
            FileSystemScriptFiles = fileSystemScriptFiles;
            DBExecutedFiles = dbExecutedFiles;

            SetIsHashDifferentFlag();

            CreateFileExistInDBButNotExistInSystemList(fileSystemScriptFiles);
        }


        private void SetIsHashDifferentFlag()
        {
            foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
            {
                DataRow lastExecutedCurrnetFileRow =
                            DBExecutedFiles.ExecutedFilesList
                            .LastOrDefault(row => Convert.ToString(row["Filename"], CultureInfo.InvariantCulture).Trim().ToUpperInvariant() == scriptFileItem.Filename.Trim().ToUpperInvariant());

                if (lastExecutedCurrnetFileRow == null)
                {
                    scriptFileItem.HashDiffType = HashDiffType.NotExist;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"], CultureInfo.InvariantCulture) != scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = HashDiffType.Different;
                }
                else if (Convert.ToString(lastExecutedCurrnetFileRow["ComputedFileHash"], CultureInfo.InvariantCulture) == scriptFileItem.ComputedHash)
                {
                    scriptFileItem.HashDiffType = HashDiffType.Equal;
                }
            }
        }

        private void CreateFileExistInDBButNotExistInSystemList(FileSystemScriptFiles fileSystemScriptFiles)
        {
            NotExistInFileSystemButExistInDB = new List<RuntimeScriptFileBase>();

            foreach (DataRow dbExecutedFileRow in DBExecutedFiles.ExecutedFilesList)
            {
                string dbFilename = Convert.ToString(dbExecutedFileRow["Filename"], CultureInfo.InvariantCulture);

                RuntimeScriptFileBase fileSystemFile =
                    AllFileSystemScriptFiles.FirstOrDefault(e => dbFilename.Trim().ToUpperInvariant() == e.Filename.Trim().ToUpperInvariant());

                if (fileSystemFile == null)
                {
                    string fileFullPath = Path.Combine(fileSystemScriptFiles.FolderPath, dbFilename);
                    RuntimeScriptFileBase misssingFileSystemFileItem = fileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(fileFullPath);
                    NotExistInFileSystemButExistInDB.Add(misssingFileSystemFileItem);
                }

                if (dbFilename == DBExecutedFiles.LastFileOfLastExecutedFilename)
                {
                    break;
                }
            }
        }



        public abstract List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename);


        public bool TryParseNextRuntimeScriptFileName(string scriptName, out RuntimeScriptFileBase newRuntimeScriptFile)
        {
            return FileSystemScriptFiles.TryParseNextRuntimeScriptFileName(scriptName, LastScriptFile, out newRuntimeScriptFile);
        }

        public RuntimeScriptFileBase CreateNextNewScriptFile(string scriptName)
        {
            return FileSystemScriptFiles.CreateNextRuntimeScriptFileInstance(scriptName, LastScriptFile);
        }


        protected RuntimeScriptFileBase CreateLasetExecutedFileItem()
        {
            RuntimeScriptFileBase prevExecutionLastScriptFile = null;
            if (!string.IsNullOrWhiteSpace(DBExecutedFiles.LastFileOfLastExecutedFilename))
            {
                string lastFileFullPath = Path.Combine(FileSystemScriptFiles.FolderPath, DBExecutedFiles.LastFileOfLastExecutedFilename);
                prevExecutionLastScriptFile = FileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(lastFileFullPath);
            }

            return prevExecutionLastScriptFile;
        }
    }
}



