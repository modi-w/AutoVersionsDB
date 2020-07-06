using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public class BuildDeployArtifactFileStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => "Build Deploy Artifact File";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private IDBCommands _dbCommands;

        private string _dbName;

        private string _incrementalScriptsFolderPath;
        private string _repeatableScriptsFolderPath;

        private string _deployArtifactFolderPath;

        public BuildDeployArtifactFileStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;

        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            _dbName = _dbCommands.GetDataBaseName();
            _incrementalScriptsFolderPath = projectConfig.IncrementalScriptsFolderPath;
            _repeatableScriptsFolderPath = projectConfig.RepeatableScriptsFolderPath;
            _deployArtifactFolderPath = projectConfig.DeployArtifactFolderPath;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            string tempFolderForDeploy = Path.Combine(AutoVersionsDBSettings.TempFolderPath, $"Deploy_{_dbName}_{DateTime.Now:HH-mm-dd-fff}");
            if (!Directory.Exists(tempFolderForDeploy))
            {
                Directory.CreateDirectory(tempFolderForDeploy);
            }



            ScriptFileTypeBase incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();

            string incrementalSubFolderToDeploy = Path.Combine(tempFolderForDeploy, incrementalScriptFileType.RelativeFolderName);
            if (!Directory.Exists(incrementalSubFolderToDeploy))
            {
                Directory.CreateDirectory(incrementalSubFolderToDeploy);
            }

            DirectoryInfo diIncremental = new DirectoryInfo(_incrementalScriptsFolderPath);
            foreach (FileInfo scriptFileToCopy in diIncremental.GetFiles())
            {
                string targetFilename = Path.Combine(incrementalSubFolderToDeploy, scriptFileToCopy.Name);
                scriptFileToCopy.CopyTo(targetFilename, true);
            }


            ScriptFileTypeBase repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();

            string repeatableSubFolderToDeploy = Path.Combine(tempFolderForDeploy, repeatableScriptFileType.RelativeFolderName);
            if (!Directory.Exists(repeatableSubFolderToDeploy))
            {
                Directory.CreateDirectory(repeatableSubFolderToDeploy);
            }

            DirectoryInfo diRepeatable = new DirectoryInfo(_repeatableScriptsFolderPath);
            foreach (FileInfo scriptFileToCopy in diRepeatable.GetFiles())
            {
                string targetFilename = Path.Combine(repeatableSubFolderToDeploy, scriptFileToCopy.Name);
                scriptFileToCopy.CopyTo(targetFilename, true);
            }




            if (!Directory.Exists(_deployArtifactFolderPath))
            {
                Directory.CreateDirectory(_deployArtifactFolderPath);
            }

            string targetFileFullPath = Path.Combine(_deployArtifactFolderPath, $"{_dbName}{ArtifactExtractor.ArtifactFilenameExtension}");

            if (File.Exists(targetFileFullPath))
            {
                File.Delete(targetFileFullPath);
            }

            ZipFile.CreateFromDirectory(tempFolderForDeploy, targetFileFullPath);
        }





        #region IDisposable

        private bool _disposed = false;

        ~BuildDeployArtifactFileStep() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dbCommands != null)
                {
                    _dbCommands.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion

    }
}
