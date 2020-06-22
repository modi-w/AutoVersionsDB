using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesManager
    {
        private FileChecksumManager _fileChecksumManager;
        private ScriptFileTypeBase _scriptFileTypeBase;
        private RuntimeScriptFileFactoryBase _runtimeScriptFileFactory { get; }

        public List<RuntimeScriptFileBase> ScriptFilesList { get; private set; }
        public Dictionary<string, RuntimeScriptFileBase> ScriptFilesDictionary { get; private set; }


        public string FolderPath { get; private set; }

        public ScriptFilesManager(FileChecksumManager fileChecksumManager,
                                        ScriptFileTypeBase scriptFileTypeBase,
                                        RuntimeScriptFileFactoryBase runtimeScriptFileFactory,
                                        string folderPath)
        {
            _fileChecksumManager = fileChecksumManager;
            _scriptFileTypeBase = scriptFileTypeBase;
            _runtimeScriptFileFactory = runtimeScriptFileFactory;
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
                string[] arrAllScriptFiles = Directory.GetFiles(FolderPath, $"{_scriptFileTypeBase.Prefix}*.sql", SearchOption.TopDirectoryOnly);

                foreach (string fileFullPath in arrAllScriptFiles)
                {
                    RuntimeScriptFileBase currScriptFile = _runtimeScriptFileFactory.CreateRuntimeScriptFileInstanceByFilename(FolderPath,fileFullPath);

                    string computedFileHash = _fileChecksumManager.GetHashByFilePath(fileFullPath);
                    currScriptFile.ComputedHash = computedFileHash;
                    currScriptFile.ComputedHashDateTime = DateTime.Now;

                    newScriptFilesList.Add(currScriptFile);
                }
            }

            ScriptFilesList = newScriptFilesList.OrderBy(e => e.SortKey).ToList();
        }


        public RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string fileFullPath)
        {
            return _runtimeScriptFileFactory.CreateRuntimeScriptFileInstanceByFilename(FolderPath, fileFullPath);
        }

        public RuntimeScriptFileBase CreateNextNewScriptFile(ScriptFilePropertiesBase lastExecutedFileProperties, string scriptName)
        {
            RuntimeScriptFileBase newRuntimeScriptFile;

            if (string.IsNullOrWhiteSpace(scriptName))
            {
                throw new ArgumentNullException(nameof(scriptName));
            }

            newRuntimeScriptFile = _runtimeScriptFileFactory.CreateNextNewScriptFileInstance(lastExecutedFileProperties,FolderPath, scriptName);

            Load();

            File.AppendAllText(newRuntimeScriptFile.FileFullPath, "", Encoding.UTF8);


            return newRuntimeScriptFile;
        }



    }

}
