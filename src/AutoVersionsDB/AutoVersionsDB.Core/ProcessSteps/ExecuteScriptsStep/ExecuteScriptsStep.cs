using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScriptsStep
{
    public class ExecuteScriptsStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => $"Run Scripts";



        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        private bool _isVirtualExecution;

        public ExecuteScriptsStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                        ScriptFilesComparersProvider scriptFilesComparersProvider,
                                        ExecuteSingleFileScriptStep runSingleFileScriptStep,
                                        bool isVirtualExecution)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
            InternalNotificationableAction = runSingleFileScriptStep;

            _isVirtualExecution = isVirtualExecution;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            int numOfFiles = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName).Count;
            numOfFiles += _scriptFilesComparersProvider.RepeatableScriptFilesComparer.GetPendingFilesToExecute(null).Count;

            if (_scriptFilesComparersProvider.DevDummyDataScriptFilesComparer != null)
            {
                numOfFiles += _scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.GetPendingFilesToExecute(null).Count;
            }

            return numOfFiles;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            List<RuntimeScriptFileBase> incScriptFilesToExecute = _scriptFilesComparersProvider.IncrementalScriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);
            runScriptsFilesList(processState, incScriptFilesToExecute, "Incremental");

            string lastIncStriptFilename = getLastIncFilename();

            if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                || lastIncStriptFilename.Trim().ToLower() == targetStateScriptFileName.Trim().ToLower())
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
           
            using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(scriptFilesList.Count))
            {
                foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
                {
                    if (!_notificationExecutersFactoryManager.HasError)
                    {
                        ScriptFileInfoStepArgs scriptFileInfoStep = new ScriptFileInfoStepArgs(scriptFile);

                        string stepInfo = scriptFile.Filename;
                        if (_isVirtualExecution)
                        {
                            stepInfo += " - Ignore (virtual execution)";
                        }

                        notificationWrapperExecuter.ExecuteStep(InternalNotificationableAction, stepInfo, processState, scriptFileInfoStep);
                    }
                }
            }

        }
    }
}
