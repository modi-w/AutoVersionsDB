﻿using AutoVersionsDB.Core.ScriptFiles;
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
        protected FileSystemScriptFiles FileSystemScriptFiles { get; private set; }

        protected DBExecutedFiles DbExecutedFiles { get; private set; }

        public string LastFileOfLastExecutedFilename => DbExecutedFiles.LastFileOfLastExecutedFilename;

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

            this.FileSystemScriptFiles = fileSystemScriptFiles;
            this.DbExecutedFiles = dbExecutedFiles;

            SetIsHashDifferentFlag();

            CreateFileExistInDBButNotExistInSystemList(fileSystemScriptFiles);
        }


        private void SetIsHashDifferentFlag()
        {
            foreach (RuntimeScriptFileBase scriptFileItem in AllFileSystemScriptFiles)
            {
                DataRow lastExecutedCurrnetFileRow =
                            DbExecutedFiles.ExecutedFilesList
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

            foreach (DataRow dbExecutedFileRow in DbExecutedFiles.ExecutedFilesList)
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

                if (dbFilename == DbExecutedFiles.LastFileOfLastExecutedFilename)
                {
                    break;
                }
            }
        }



        public abstract List<RuntimeScriptFileBase> GetPendingFilesToExecute(string targetScriptFilename);


        public RuntimeScriptFileBase CreateNextNewScriptFile(string scriptName)
        {
            return FileSystemScriptFiles.CreateNextRuntimeScriptFileInstance(scriptName, LastScriptFile);
        }


        protected RuntimeScriptFileBase CreateLasetExecutedFileItem()
        {
            RuntimeScriptFileBase prevExecutionLastScriptFile = null;
            if (!string.IsNullOrWhiteSpace(DbExecutedFiles.LastFileOfLastExecutedFilename))
            {
                string lastFileFullPath = Path.Combine(FileSystemScriptFiles.FolderPath, DbExecutedFiles.LastFileOfLastExecutedFilename);
                prevExecutionLastScriptFile = FileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(lastFileFullPath);
            }

            return prevExecutionLastScriptFile;
        }
    }
}



