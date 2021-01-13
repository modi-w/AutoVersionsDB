using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using System;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class BuildDeployArtifactFileStep : DBVersionsStep
    {
        private readonly AutoVersionsDBSettings _settings;

        public override string StepName => "Build Deploy Artifact File";

        private readonly DBCommandsFactory _dbCommandsFactory;

        public BuildDeployArtifactFileStep(DBCommandsFactory dbCommandsFactory,
                                            AutoVersionsDBSettings settings)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactory = dbCommandsFactory;
            _settings = settings;

        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
            {
                string dbName = dbCommands.DataBaseName;


                string tempFolderForDeploy = Path.Combine(_settings.TempFolderPath, $"Deploy_{dbName}_{DateTime.Now:HH-mm-dd-fff}");
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

                processContext.Results = targetFileFullPath;
            }

        }

    }
}
