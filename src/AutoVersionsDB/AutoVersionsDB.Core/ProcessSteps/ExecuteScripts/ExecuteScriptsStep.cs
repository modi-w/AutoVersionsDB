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
    public class ExecuteScriptsStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => $"Run Scripts";


        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private ScriptFilesComparersManager _scriptFilesComparersManager;

        private ProjectConfigItem _projectConfig;
        private IDBCommands _dbCommands;


        public ExecuteScriptsStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    ScriptFilesComparersManager scriptFilesComparersManager,
                                    ExecuteSingleFileScriptStep executeSingleFileScriptStep)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparersManager.ThrowIfNull(nameof(scriptFilesComparersManager));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparersManager = scriptFilesComparersManager;

            InternalNotificationableAction = executeSingleFileScriptStep;
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _projectConfig = projectConfig;

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            (InternalNotificationableAction as ExecuteSingleFileScriptStep).SetDBCommands(_dbCommands);
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));

            ScriptFilesComparersProvider scriptFilesComparersProvider = _scriptFilesComparersManager.GetScriptFilesComparersProvider(_projectConfig.ProjectGuid);


            int numOfFiles = scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            numOfFiles += scriptFilesComparersProvider.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null).Count;

            if (scriptFilesComparersProvider.DevDummyDataScriptFilesComparer != null)
            {
                numOfFiles += scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            }

            return numOfFiles;
        }


        public override void Execute(NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));

            ScriptFilesComparersProvider scriptFilesComparersProvider = _scriptFilesComparersManager.GetScriptFilesComparersProvider(_projectConfig.ProjectGuid);

            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            List<RuntimeScriptFileBase> incScriptFilesToExecute = scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);
            runScriptsFilesList(notificationExecutersProvider, processState, incScriptFilesToExecute, "Incremental");

            string lastIncStriptFilename = getLastIncFilename();

            if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
            {
                List<RuntimeScriptFileBase> rptScriptFilesToExecute = scriptFilesComparersProvider.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null);
                runScriptsFilesList(notificationExecutersProvider, processState, rptScriptFilesToExecute, "Repeatable");

                if (scriptFilesComparersProvider.DevDummyDataScriptFilesComparer != null)
                {
                    List<RuntimeScriptFileBase> dddScriptFilesToExecute = scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null);
                    runScriptsFilesList(notificationExecutersProvider, processState, dddScriptFilesToExecute, "DevDummyData");
                }
            }

        }


        private string getLastIncFilename()
        {
            string lastIncStriptFilename = "";

            ScriptFilesComparersProvider scriptFilesComparersProvider = _scriptFilesComparersManager.GetScriptFilesComparersProvider(_projectConfig.ProjectGuid);

            RuntimeScriptFileBase lastIncScriptFiles = scriptFilesComparersProvider.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }

        private void runScriptsFilesList(NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState, List<RuntimeScriptFileBase> scriptFilesList, string additionalStepInfo)
        {
            (InternalNotificationableAction as ExecuteSingleFileScriptStep).OverrideStepName(additionalStepInfo);

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(scriptFilesList.Count))
            {
                foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
                {
                    if (!notificationExecutersProvider.NotifictionStatesHistory.HasError)
                    {
                        ScriptFileInfoStepArgs scriptFileInfoStep = new ScriptFileInfoStepArgs(scriptFile);

                        string stepInfo = scriptFile.Filename;
                        if (isVirtualExecution)
                        {
                            stepInfo += " - Ignore (virtual execution)";
                        }

                        notificationWrapperExecuter.ExecuteStep(InternalNotificationableAction, stepInfo, processState, scriptFileInfoStep);
                    }
                }
            }

        }



        #region IDisposable

        private bool _disposed = false;

        ~ExecuteScriptsStep() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
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
