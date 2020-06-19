using AutoVersionsDB.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesManager
    {
        private FileChecksumManager _fileChecksumManager;
        private ScriptFileTypeBase _scriptFileTypeBase;
        private RuntimeScriptFile_FactoryBase _runtimeScriptFile_Factory { get; }

        public List<RuntimeScriptFileBase> ScriptFilesList { get; protected set; }
        public Dictionary<string, RuntimeScriptFileBase> ScriptFilesDictionary { get; private set; }


        public string FolderPath { get; private set; }

        public ScriptFilesManager(FileChecksumManager fileChecksumManager,
                                        ScriptFileTypeBase scriptFileTypeBase,
                                        RuntimeScriptFile_FactoryBase runtimeScriptFile_Factory,
                                        string folderPath)
        {
            _fileChecksumManager = fileChecksumManager;
            _scriptFileTypeBase = scriptFileTypeBase;
            _runtimeScriptFile_Factory = runtimeScriptFile_Factory;
            FolderPath = folderPath;

            Load();
        }


        public void Load()
        {
            loadScriptFilesList();

            ScriptFilesDictionary = ScriptFilesList.ToDictionary(e => e.Filename.Trim().ToLower());
        }

        protected void loadScriptFilesList()
        {
            List<RuntimeScriptFileBase> newScriptFilesList = new List<RuntimeScriptFileBase>();

            if (Directory.Exists(FolderPath))
            {
                string[] arrAllScriptFiles = Directory.GetFiles(FolderPath, $"{_scriptFileTypeBase.Prefix}*.sql", SearchOption.TopDirectoryOnly);

                foreach (string fileFullPath in arrAllScriptFiles)
                {
                    RuntimeScriptFileBase currScriptFile = _runtimeScriptFile_Factory.CreateRuntimeScriptFileInstanceByFilename(FolderPath,fileFullPath);

                    string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fileFullPath);
                    currScriptFile.ComputedHash = computedFileHash;
                    currScriptFile.ComputedHashDateTime = DateTime.Now;

                    newScriptFilesList.Add(currScriptFile);
                }
            }

            ScriptFilesList = newScriptFilesList.OrderBy(e => e.SortKey).ToList();
        }


        public RuntimeScriptFileBase CreateRuntimeScriptFileInstanceByFilename(string fileFullPath)
        {
            return _runtimeScriptFile_Factory.CreateRuntimeScriptFileInstanceByFilename(FolderPath, fileFullPath);
        }

        public RuntimeScriptFileBase CreateNextNewScriptFile(ScriptFilePropertiesBase lastExecutedFileProperties, string scriptName)
        {
            RuntimeScriptFileBase newRuntimeScriptFile;

            if (string.IsNullOrWhiteSpace(scriptName))
            {
                throw new ArgumentNullException("scriptName");
            }

            newRuntimeScriptFile = _runtimeScriptFile_Factory.CreateNextNewScriptFileInstance(lastExecutedFileProperties,FolderPath, scriptName);

            Load();

            File.AppendAllText(newRuntimeScriptFile.FileFullPath, "", Encoding.UTF8);


            return newRuntimeScriptFile;
        }



    }

}
