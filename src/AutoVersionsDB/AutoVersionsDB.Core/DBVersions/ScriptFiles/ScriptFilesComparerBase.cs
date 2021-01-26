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

        public List<RuntimeScriptFile> AllFileSystemScriptFiles => FileSystemScriptFiles.ScriptFilesList;
        public List<RuntimeScriptFile> ExecutedFilesAll => 
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.Equal 
                    || e.HashDiffType == HashDiffType.EqualVirtual)
            .ToList();
        public List<RuntimeScriptFile> ExecutedFilesActual =>
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.Equal)
            .ToList();
        public List<RuntimeScriptFile> ExecutedFilesVirtual =>
            AllFileSystemScriptFiles
            .Where(e => e.HashDiffType == HashDiffType.EqualVirtual)
            .ToList();

        public bool IsAllFilesExecuted => ExecutedFilesAll.Count == AllFileSystemScriptFiles.Count;

        public List<RuntimeScriptFile> ChangedFiles => AllFileSystemScriptFiles.Where(e => e.HashDiffType == HashDiffType.Different).ToList();
        public List<RuntimeScriptFile> NotExistInDBButExistInFileSystem => AllFileSystemScriptFiles.Where(e => e.HashDiffType == HashDiffType.NotExist).ToList();

        public List<RuntimeScriptFile> NotExistInFileSystemButExistInDB { get; private set; }

        public RuntimeScriptFile LastScriptFile => AllFileSystemScriptFiles.LastOrDefault();


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


        protected RuntimeScriptFile GetTargetRuntimeScriptFile(string targetScriptFilename)
        {
            targetScriptFilename.ThrowIfNull(nameof(targetScriptFilename));


            RuntimeScriptFile targetRuntimeScriptFile = null;

            if (targetScriptFilename.Trim().ToUpperInvariant() != RuntimeScriptFile.TargetNoneScriptFileName.Trim().ToUpperInvariant())
            {
                if (targetScriptFilename.Trim().ToUpperInvariant() == RuntimeScriptFile.TargetLastScriptFileName.Trim().ToUpperInvariant())
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
            }
   


            return targetRuntimeScriptFile;
        }


        protected virtual List<RuntimeScriptFile> FilterPendingScriptsFilesByTarget(RuntimeScriptFile prevExecutionLastScriptFile, RuntimeScriptFile targetScriptFile, List<RuntimeScriptFile> sourceRuntimeFiles)
        {
            sourceRuntimeFiles.ThrowIfNull(nameof(sourceRuntimeFiles));
            //targetScriptFile.ThrowIfNull(nameof(targetScriptFile));

            List<RuntimeScriptFile> pendingScriptFilesList = new List<RuntimeScriptFile>();


            foreach (RuntimeScriptFile scriptFileItem in sourceRuntimeFiles)
            {
                // Comment:  We return all the files that after the preve executed file and before (and equal) to the target file.
                if ((prevExecutionLastScriptFile == null || 0 < string.Compare(scriptFileItem.SortKey, prevExecutionLastScriptFile.SortKey, StringComparison.Ordinal))
                    && (targetScriptFile== null || string.Compare(scriptFileItem.SortKey, targetScriptFile.SortKey, StringComparison.Ordinal) <= 0))
                {
                    pendingScriptFilesList.Add(scriptFileItem);
                }
            }

            return pendingScriptFilesList;
        }



        private void SetIsHashDifferentFlag()
        {
            foreach (RuntimeScriptFile scriptFileItem in AllFileSystemScriptFiles)
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
            NotExistInFileSystemButExistInDB = new List<RuntimeScriptFile>();

            foreach (DataRow dbExecutedFileRow in DBExecutedFiles.ExecutedFilesList)
            {
                string dbFilename = Convert.ToString(dbExecutedFileRow["Filename"], CultureInfo.InvariantCulture);

                RuntimeScriptFile fileSystemFile =
                    AllFileSystemScriptFiles.FirstOrDefault(e => dbFilename.Trim().ToUpperInvariant() == e.Filename.Trim().ToUpperInvariant());

                if (fileSystemFile == null)
                {
                    string fileFullPath = Path.Combine(fileSystemScriptFiles.FolderPath, dbFilename);
                    RuntimeScriptFile misssingFileSystemFileItem = fileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(fileFullPath);
                    NotExistInFileSystemButExistInDB.Add(misssingFileSystemFileItem);
                }

                if (dbFilename == DBExecutedFiles.LastFileOfLastExecutedFilename)
                {
                    break;
                }
            }
        }



        public abstract List<RuntimeScriptFile> GetPendingFilesToExecute(string targetScriptFilename);


        public bool TryParseNextRuntimeScriptFileName(string scriptName, out RuntimeScriptFile newRuntimeScriptFile)
        {
            return FileSystemScriptFiles.TryParseNextRuntimeScriptFileName(scriptName, LastScriptFile, out newRuntimeScriptFile);
        }

        public RuntimeScriptFile CreateNextNewScriptFile(string scriptName)
        {
            return FileSystemScriptFiles.CreateNextRuntimeScriptFileInstance(scriptName, LastScriptFile);
        }


        protected RuntimeScriptFile CreateLasetExecutedFileItem()
        {
            RuntimeScriptFile prevExecutionLastScriptFile = null;
            if (!string.IsNullOrWhiteSpace(DBExecutedFiles.LastFileOfLastExecutedFilename))
            {
                string lastFileFullPath = Path.Combine(FileSystemScriptFiles.FolderPath, DBExecutedFiles.LastFileOfLastExecutedFilename);
                prevExecutionLastScriptFile = FileSystemScriptFiles.CreateRuntimeScriptFileInstanceByFilename(lastFileFullPath);
            }

            return prevExecutionLastScriptFile;
        }
    }
}



