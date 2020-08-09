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
        public override string StepName => $"Run Scripts";
        public override bool HasInternalStep => true;

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        private ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;


        public ExecuteScriptsStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            executeSingleFileScriptStepFactory.ThrowIfNull(nameof(executeSingleFileScriptStepFactory));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
        }



        public override int GetNumOfInternalSteps(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
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


        public override void Execute(ProjectConfig projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));

            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
            {
                List<RuntimeScriptFileBase> incScriptFilesToExecute = processState.ScriptFilesState.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);
                runScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, incScriptFilesToExecute, "Incremental");

                string lastIncStriptFilename = getLastIncFilename(processState);

                if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                    || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
                {
                    List<RuntimeScriptFileBase> rptScriptFilesToExecute = processState.ScriptFilesState.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null);
                    runScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, rptScriptFilesToExecute, "Repeatable");

                    if (processState.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                    {
                        List<RuntimeScriptFileBase> dddScriptFilesToExecute = processState.ScriptFilesState.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null);
                        runScriptsFilesList(dbCommands, notificationExecutersProvider, projectConfig, processState, dddScriptFilesToExecute, "DevDummyData");
                    }
                }
            }


        }


        private string getLastIncFilename(AutoVersionsDbProcessState processState)
        {
            string lastIncStriptFilename = "";


            RuntimeScriptFileBase lastIncScriptFiles = processState.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }

        private void runScriptsFilesList(IDBCommands dbCommands, NotificationExecutersProvider notificationExecutersProvider, ProjectConfig projectConfig, AutoVersionsDbProcessState processState, List<RuntimeScriptFileBase> scriptFilesList, string fileType)
        {

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(scriptFilesList.Count))
            {
                foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
                {
                    if (!notificationExecutersProvider.NotifictionStatesHistory.HasError)
                    {
                        string ignoreStr = "";
                        if (isVirtualExecution)
                        {
                            ignoreStr = " - Ignore (virtual execution)";
                        }

                        string stepName = $"{fileType} - {scriptFile.Filename}{ignoreStr}";

                        ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(dbCommands, stepName, scriptFile);


                        notificationWrapperExecuter.ExecuteStep(executeSingleFileScriptStep, projectConfig, processState);
                    }
                }
            }

        }

    }
}
