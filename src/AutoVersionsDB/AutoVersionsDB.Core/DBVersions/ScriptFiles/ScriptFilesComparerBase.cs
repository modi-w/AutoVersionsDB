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
        public List<RuntimeScriptFileBase> ExecutedFilesAll => 
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.Equal 
                    || e.HashDiffType == HashDiffType.EqualVirtual)
            .ToList();
        public List<RuntimeScriptFileBase> ExecutedFilesActual =>
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.Equal)
            .ToList();
        public List<RuntimeScriptFileBase> ExecutedFilesVirtual =>
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.EqualVirtual)
            .ToList();

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


        protected RuntimeScriptFileBase GetTargetRuntimeScriptFile(string targetScriptFilename)
        {
            targetScriptFilename.ThrowIfNull(nameof(targetScriptFilename));


            RuntimeScriptFileBase targetRuntimeScriptFile = null;

            if (targetScriptFilename.Trim().ToUpperInvariant() == RuntimeScriptFileBase.TargetLastScriptFileName.Trim().ToUpperInvariant())
            {
                targetRuntimeScriptFile = AllFileSystemScriptFiles.LastOrDefault();
            }
            else
            {
                targetRuntimeScriptFile = AllFileSystemScriptFiles.FirstOrDefault(e => e.Filename.Trim().ToUpperInvariant() == targetScriptFilename.Trim().ToUpperInvariant());
                if (targetRuntimeScriptFile == null)
                {
                    throw new ArgumentException($"{targetRuntimeScriptFile} is not exist in the {ScriptFileType.FileTypeCode} files", nameof(targetScriptFilename));
                }
            }

            //RuntimeScriptFileBase targetScriptFile = null;
            //if (!string.IsNullOrWhiteSpace(targetScriptFilename))
            //{
            //    string targetFileFullPath = Path.Combine(FileSystemScriptFiles.FolderPath, targetScriptFilename);
            //    targetScriptFile = FileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(targetFileFullPath);
            //}

            return targetRuntimeScriptFile;
        }


        protected virtual List<RuntimeScriptFileBase> FilterPendingScriptsFilesByTarget(RuntimeScriptFileBase prevExecutionLastScriptFile, RuntimeScriptFileBase targetScriptFile, List<RuntimeScriptFileBase> sourceRuntimeFiles)
        {
            sourceRuntimeFiles.ThrowIfNull(nameof(sourceRuntimeFiles));
            targetScriptFile.ThrowIfNull(nameof(targetScriptFile));

            List<RuntimeScriptFileBase> pendingScriptFilesList = new List<RuntimeScriptFileBase>();


            foreach (RuntimeScriptFileBase scriptFileItem in sourceRuntimeFiles)
            {
                // Comment:  We return all the files that after the preve executed file and before (and equal) to the target file.
                if ((prevExecutionLastScriptFile == null || 0 < string.Compare(scriptFileItem.SortKey, prevExecutionLastScriptFile.SortKey, StringComparison.Ordinal))
                    && (string.Compare(scriptFileItem.SortKey, targetScriptFile.SortKey, StringComparison.Ordinal) <= 0))
                {
                    pendingScriptFilesList.Add(scriptFileItem);
                }
            }

            return pendingScriptFilesList;
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
                    bool isVirtualExecution = Convert.ToBoolean(lastExecutedCurrnetFileRow["IsVirtualExecution"], CultureInfo.InvariantCulture);
                    if (isVirtualExecution)
                    {
                        scriptFileItem.HashDiffType = HashDiffType.EqualVirtual;
                    }
                    else
                    {
                        scriptFileItem.HashDiffType = HashDiffType.Equal;
                    }
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



