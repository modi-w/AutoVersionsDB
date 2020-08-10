using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class FileSystemScriptFiles
    {
        private readonly FileChecksum _fileChecksum;
        private readonly ScriptFileTypeBase _scriptFileType;

        public List<RuntimeScriptFileBase> ScriptFilesList { get; private set; }
        public Dictionary<string, RuntimeScriptFileBase> ScriptFilesDictionary { get; private set; }


        public string FolderPath { get; private set; }

        public FileSystemScriptFiles(FileChecksum fileChecksum,
                                        ScriptFileTypeBase scriptFileType,
                                        string folderPath)
        {
            fileChecksum.ThrowIfNull(nameof(fileChecksum));
            scriptFileType.ThrowIfNull(nameof(scriptFileType));
            folderPath.ThrowIfNull(nameof(folderPath));


            _fileChecksum = fileChecksum;
            _scriptFileType = scriptFileType;
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
            List<RuntimeScriptFileBase> newScriptFilesList = new List<RuntimeScriptFileBase>();

            if (Directory.Exists(FolderPath))
            {
                string[] arrAllScriptFiles = Directory.GetFiles(FolderPath, $"{_scriptFileType.Prefix}*.sql", SearchOption.TopDirectoryOnly);

                foreach (string fileFullPath in arrAllScriptFiles)
                {
                    RuntimeScriptFileBase currScriptFile = _scriptFileType.RuntimeScriptFileFactory.CreateRuntimeScriptFileInstanceByFilename(FolderPath, fileFullPath);

                    string computedFileHash = _fileChecksum.GetHashByFilePath(fileFullPath);
                    currScriptFile.ComputedHash = computedFileHash;
                    currScriptFile.ComputedHashDateTime = DateTime.Now;

                    newScriptFilesList.Add(currScriptFile);
                }
            }

            ScriptFilesList = newScriptFilesList.OrderBy(e => e.SortKey).ToList();
        }


        public RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string fileFullPath)
        {
            return _scriptFileType.RuntimeScriptFileFactory.CreateRuntimeScriptFileInstanceByFilename(FolderPath, fileFullPath);
        }

        public RuntimeScriptFileBase CreateNextRuntimeScriptFileInstance(string scriptName, RuntimeScriptFileBase prevRuntimeScriptFile)
        {
            scriptName.ThrowIfNull(nameof(scriptName));

            RuntimeScriptFileBase newRuntimeScriptFile = _scriptFileType.RuntimeScriptFileFactory.CreateNextRuntimeScriptFileInstance(FolderPath, scriptName, prevRuntimeScriptFile);

            File.AppendAllText(newRuntimeScriptFile.FileFullPath, "", Encoding.UTF8);

            Load();


            return newRuntimeScriptFile;
        }



    }

}
