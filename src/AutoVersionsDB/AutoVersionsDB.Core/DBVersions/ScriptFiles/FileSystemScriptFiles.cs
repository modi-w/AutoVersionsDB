using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class FileSystemScriptFiles
    {
        private readonly FileChecksum _fileChecksum;

        public ScriptFileTypeBase ScriptFileType { get; }

        public List<RuntimeScriptFile> ScriptFilesList { get; private set; }
        public Dictionary<string, RuntimeScriptFile> ScriptFilesDictionary { get; private set; }


        public string FolderPath { get; private set; }

        public FileSystemScriptFiles(FileChecksum fileChecksum,
                                        ScriptFileTypeBase scriptFileType,
                                        string folderPath)
        {
            fileChecksum.ThrowIfNull(nameof(fileChecksum));
            scriptFileType.ThrowIfNull(nameof(scriptFileType));
            folderPath.ThrowIfNull(nameof(folderPath));


            _fileChecksum = fileChecksum;
            ScriptFileType = scriptFileType;
            FolderPath = folderPath;

            Load();
        }


        public void Load()
        {
            LoadScriptFilesList();

            ScriptFilesDictionary = ScriptFilesList.ToDictionary(e => e.Filename.Trim().ToUpperInvariant());
        }

        protected void LoadScriptFilesList()
        {
            List<RuntimeScriptFile> newScriptFilesList = new List<RuntimeScriptFile>();

            if (Directory.Exists(FolderPath))
            {
                string[] arrAllScriptFiles = Directory.GetFiles(FolderPath, $"{ScriptFileType.Prefix}*.sql", SearchOption.TopDirectoryOnly);

                foreach (string fileFullPath in arrAllScriptFiles)
                {
                    RuntimeScriptFile currScriptFile = new RuntimeScriptFile(ScriptFileType, FolderPath, fileFullPath);

                    string computedFileHash = _fileChecksum.GetHashByFilePath(fileFullPath);
                    currScriptFile.ComputedHash = computedFileHash;
                    currScriptFile.ComputedHashDateTime = DateTime.Now;

                    newScriptFilesList.Add(currScriptFile);
                }
            }

            ScriptFilesList = newScriptFilesList.OrderBy(e => e.SortKey).ToList();
        }


        public RuntimeScriptFile CreateRuntimeScriptFileInstanceByFilename(string fileFullPath)
        {
            return new RuntimeScriptFile(ScriptFileType, FolderPath, fileFullPath);
        }

        public bool TryParseNextRuntimeScriptFileName(string scriptName, RuntimeScriptFile prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile)
        {
            return RuntimeScriptFile.TryParseNextRuntimeScriptFileInstance(ScriptFileType, FolderPath, scriptName, prevRuntimeScriptFile, out newRuntimeScriptFile);
        }

        public RuntimeScriptFile CreateNextRuntimeScriptFileInstance(string scriptName, RuntimeScriptFile prevRuntimeScriptFile)
        {
            scriptName.ThrowIfNull(nameof(scriptName));

            if (!RuntimeScriptFile.TryParseNextRuntimeScriptFileInstance(ScriptFileType,FolderPath, scriptName, prevRuntimeScriptFile, out RuntimeScriptFile newRuntimeScriptFile))
            {
                string errorMessage =
                    CoreTextResources
                    .InvalidFilenameErrorMessage
                    .Replace("[Filename]", newRuntimeScriptFile.Filename)
                    .Replace("[FileTypeCode]", ScriptFileType.FileTypeCode)
                    .Replace("[FilenamePattern]", ScriptFileType.FilenamePattern);

                throw new Exception(errorMessage);
            }

            File.AppendAllText(newRuntimeScriptFile.FileFullPath, "", Encoding.UTF8);

            Load();


            return newRuntimeScriptFile;
        }



    }

}
