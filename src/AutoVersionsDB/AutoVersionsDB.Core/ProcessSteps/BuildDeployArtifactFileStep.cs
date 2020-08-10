﻿using AutoVersionsDB.Core.ArtifactFile;
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
    public class BuildDeployArtifactFileStep : AutoVersionsDbStep
    {
        public override string StepName => "Build Deploy Artifact File";
        public override bool HasInternalStep => false;

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public BuildDeployArtifactFileStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;

        }

      

        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            return 1;
        }

        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
            {
                string dbName = dbCommands.GetDataBaseName();


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

                DirectoryInfo diIncremental = new DirectoryInfo(projectConfig.IncrementalScriptsFolderPath);
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

                DirectoryInfo diRepeatable = new DirectoryInfo(projectConfig.RepeatableScriptsFolderPath);
                foreach (FileInfo scriptFileToCopy in diRepeatable.GetFiles())
                {
                    string targetFilename = Path.Combine(repeatableSubFolderToDeploy, scriptFileToCopy.Name);
                    scriptFileToCopy.CopyTo(targetFilename, true);
                }




                if (!Directory.Exists(projectConfig.DeployArtifactFolderPath))
                {
                    Directory.CreateDirectory(projectConfig.DeployArtifactFolderPath);
                }

                string targetFileFullPath = Path.Combine(projectConfig.DeployArtifactFolderPath, $"{dbName}{ArtifactExtractor.ArtifactFilenameExtension}");

                if (File.Exists(targetFileFullPath))
                {
                    File.Delete(targetFileFullPath);
                }

                ZipFile.CreateFromDirectory(tempFolderForDeploy, targetFileFullPath);
            }

        }



     

    }
}
