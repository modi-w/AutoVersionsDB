using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public static class BuildDeployArtifactFileStepFluent
    {
        public static AutoVersionsDbEngine BuildDeployArtifactFile(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                        ScriptFilesComparersProvider scriptFilesComparersProvider,
                                                        string dbName,
                                                        ProjectConfigItem projectConfig)
        {
            BuildDeployArtifactFileStep buildDeployArtifactFileStep =
                new BuildDeployArtifactFileStep(scriptFilesComparersProvider,
                                                            dbName,
                                                            projectConfig.IncrementalScriptsFolderPath,
                                                            projectConfig.RepeatableScriptsFolderPath,
                                                            projectConfig.DeployArtifactFolderPath);


            autoVersionsDbEngine.AppendProcessStep(buildDeployArtifactFileStep);

            return autoVersionsDbEngine;
        }
    }


    public class BuildDeployArtifactFileStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Build Deploy Artifact File";

        private ScriptFilesComparersProvider _scriptFilesComparersProvider;
        private string _dbName;
        private string _incrementalScriptsFolderPath;
        private string _repeatableScriptsFolderPath;

        private string _deployArtifactFolderPath;

        public BuildDeployArtifactFileStep(ScriptFilesComparersProvider scriptFilesComparersProvider,
                                                        string dbName,
                                                        string incrementalScriptsFolderPath,
                                                        string repeatableScriptsFolderPath,
                                                        string deployArtifactFolderPath)
        {
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
            _dbName = dbName;
            _incrementalScriptsFolderPath = incrementalScriptsFolderPath;
            _repeatableScriptsFolderPath = repeatableScriptsFolderPath;
            _deployArtifactFolderPath = deployArtifactFolderPath;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            string tempFolderForDeploy = Path.Combine(AutoVersionsDBSettings.TempFolderPath, $"Deploy_{_dbName}_{DateTime.Now.ToString("HH-mm-dd-fff")}");
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

            string targetFileFullPath = Path.Combine(_deployArtifactFolderPath, $"{_dbName}{ArtifactExtractor.C_ArtifactFilenameExtension}");

            if (File.Exists(targetFileFullPath))
            {
                File.Delete(targetFileFullPath);
            }

            ZipFile.CreateFromDirectory(tempFolderForDeploy, targetFileFullPath);
        }
    }
}
