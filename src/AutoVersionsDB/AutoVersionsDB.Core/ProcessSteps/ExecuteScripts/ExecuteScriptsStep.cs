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


        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        private IDBCommands _dbCommands;


        public ExecuteScriptsStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    ScriptFilesComparersProvider scriptFilesComparersProvider,
                                    ExecuteSingleFileScriptStep executeSingleFileScriptStep)
        {
            notificationExecutersFactoryManager.ThrowIfNull(nameof(notificationExecutersFactoryManager));
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparersProvider.ThrowIfNull(nameof(scriptFilesComparersProvider));

            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparersProvider = scriptFilesComparersProvider;

            InternalNotificationableAction = executeSingleFileScriptStep;
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _scriptFilesComparersProvider.SetProjectConfig(projectConfig);
            _scriptFilesComparersProvider.Reload();

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            
            (InternalNotificationableAction as ExecuteSingleFileScriptStep).SetDBCommands(_dbCommands);
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));


            int numOfFiles = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            numOfFiles += _scriptFilesComparersProvider.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null).Count;

            if (_scriptFilesComparersProvider.DevDummyDataScriptFilesComparer != null)
            {
                numOfFiles += _scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            }

            return numOfFiles;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));


            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            List<RuntimeScriptFileBase> incScriptFilesToExecute = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);
            runScriptsFilesList(processState, incScriptFilesToExecute, "Incremental");

            string lastIncStriptFilename = getLastIncFilename();

            if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
            {
                List<RuntimeScriptFileBase> rptScriptFilesToExecute = _scriptFilesComparersProvider.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null);
                runScriptsFilesList(processState, rptScriptFilesToExecute, "Repeatable");

                if (_scriptFilesComparersProvider.DevDummyDataScriptFilesComparer != null)
                {
                    List<RuntimeScriptFileBase> dddScriptFilesToExecute = _scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null);
                    runScriptsFilesList(processState, dddScriptFilesToExecute, "DevDummyData");
                }
            }

        }


        private string getLastIncFilename()
        {
            string lastIncStriptFilename = "";

            RuntimeScriptFileBase lastIncScriptFiles = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }

        private void runScriptsFilesList(AutoVersionsDbProcessState processState, List<RuntimeScriptFileBase> scriptFilesList, string additionalStepInfo)
        {
            (InternalNotificationableAction as ExecuteSingleFileScriptStep).OverrideStepName(additionalStepInfo);

           bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(scriptFilesList.Count))
            {
                foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
                {
                    if (!_notificationExecutersFactoryManager.HasError)
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

        ~CreateBackupStep() => Dispose(false);

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
