﻿using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public class BuildDeployArtifactFileStep : AutoVersionsDbStep
    {
        public override string StepName => "Build Deploy Artifact File";

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public BuildDeployArtifactFileStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;

        }



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            string dbName = getDBName(processContext);

            string tempFolderForDeploy = Path.Combine(AutoVersionsDBSettings.TempFolderPath, $"Deploy_{dbName}_{DateTime.Now:HH-mm-dd-fff}");
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

            DirectoryInfo diIncremental = new DirectoryInfo(processContext.ProjectConfig.IncrementalScriptsFolderPath);
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

            DirectoryInfo diRepeatable = new DirectoryInfo(processContext.ProjectConfig.RepeatableScriptsFolderPath);
            foreach (FileInfo scriptFileToCopy in diRepeatable.GetFiles())
            {
                string targetFilename = Path.Combine(repeatableSubFolderToDeploy, scriptFileToCopy.Name);
                scriptFileToCopy.CopyTo(targetFilename, true);
            }




            if (!Directory.Exists(processContext.ProjectConfig.DeployArtifactFolderPath))
            {
                Directory.CreateDirectory(processContext.ProjectConfig.DeployArtifactFolderPath);
            }

            string targetFileFullPath = Path.Combine(processContext.ProjectConfig.DeployArtifactFolderPath, $"{dbName}{ArtifactExtractor.ArtifactFilenameExtension}");

            if (File.Exists(targetFileFullPath))
            {
                File.Delete(targetFileFullPath);
            }

            ZipFile.CreateFromDirectory(tempFolderForDeploy, targetFileFullPath);

        }

        private string getDBName(AutoVersionsDbProcessContext processContext)
        {
            string dbName;

            IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout);
            try
            {
                dbName = dbCommands.GetDataBaseName();
            }
            finally
            {
                _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbCommands);
            }

            return dbName;
        }
    }
}
