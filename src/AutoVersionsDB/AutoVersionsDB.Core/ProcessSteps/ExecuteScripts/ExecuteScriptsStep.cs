using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptsStep : AutoVersionsDbStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public override string StepName => $"Run Scripts";
        public override bool HasInternalStep => true;



        public ExecuteScriptsStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory,
                                    ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            executeSingleFileScriptStepFactory.ThrowIfNull(nameof(executeSingleFileScriptStepFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }



        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));


            int numOfFiles = processState.ScriptFilesState.IncrementalScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            numOfFiles += processState.ScriptFilesState.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null).Count;

            if (processState.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
            {
                numOfFiles += processState.ScriptFilesState.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            }

            return numOfFiles;
        }


        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            notificationExecutersProvider.ThrowIfNull(nameof(notificationExecutersProvider));
            processState.ThrowIfNull(nameof(processState));

            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {

                using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                {
                    List<RuntimeScriptFileBase> incScriptFilesToExecute = processState.ScriptFilesState.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);
                    RunScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, incScriptFilesToExecute, "Incremental");

                    string lastIncStriptFilename = GetLastIncFilename(processState);

                    if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                        || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
                    {
                        List<RuntimeScriptFileBase> rptScriptFilesToExecute = processState.ScriptFilesState.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null);
                        RunScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, rptScriptFilesToExecute, "Repeatable");

                        if (processState.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                        {
                            List<RuntimeScriptFileBase> dddScriptFilesToExecute = processState.ScriptFilesState.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null);
                            RunScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, dddScriptFilesToExecute, "DevDummyData");
                        }
                    }
                }
            }


        }


        private static string GetLastIncFilename(AutoVersionsDbProcessState processState)
        {
            string lastIncStriptFilename = "";


            RuntimeScriptFileBase lastIncScriptFiles = processState.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }

        private void RunScriptsFilesList(IDBCommands dbCommands, NotificationExecutersProvider notificationExecutersProvider, ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, List<RuntimeScriptFileBase> scriptFilesList, string fileType)
        {

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            List<NotificationableActionStepBase> internalSteps = new List<NotificationableActionStepBase>();
            foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
            {
                string ignoreStr = "";
                if (isVirtualExecution)
                {
                    ignoreStr = " - Ignore (virtual execution)";
                }

                string stepName = $"{fileType} - {scriptFile.Filename}{ignoreStr}";

                ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(dbCommands, stepName, scriptFile);
                
                internalSteps.Add(executeSingleFileScriptStep);
            }

            using (NotificationWrapperExecuter notificationWrapperExecuter =
                notificationExecutersProvider.CreateNotificationWrapperExecuter(this.StepName, internalSteps, false))
            {
                notificationWrapperExecuter.Execute(projectConfig, notificationExecutersProvider, processState);
            }

        }

    }
}
