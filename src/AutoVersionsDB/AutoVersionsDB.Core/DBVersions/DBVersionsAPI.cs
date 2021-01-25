using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.NotificationableEngine;
using System;

namespace AutoVersionsDB.Core.DBVersions
{
    public class DBVersionsAPI
    {
        private readonly NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> _dbVersionsValidationsRunner;
        private readonly NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> _targetStateScriptFileValidationRunner;

        private readonly NotificationProcessRunner<GetScriptFilesStateProcessDefinitions, DBVersionsProcessContext> _getScriptFilesStateRunner;

        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<IncrementalScriptFileType>, DBVersionsProcessContext> _createIncrementalNextScriptFileRunner;
        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<RepeatableScriptFileType>, DBVersionsProcessContext> _createRepeatableNextScriptFileRunner;
        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<DevDummyDataScriptFileType>, DBVersionsProcessContext> _createDevDummyDataNextScriptFileRunner;

        private readonly NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> _syncDBRunner;
        private readonly NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> _recreateDBFromScratchRunner;
        private readonly NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> _syncDBToSpecificStateRunner;
        private readonly NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> _createVirtualExecutionsRunner;

        private readonly NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> _deployExecutionsRunner;



        public DBVersionsAPI(NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> dbVersionsValidationsRunner,
                               NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> targetStateScriptFileValidationRunner,
                               NotificationProcessRunner<GetScriptFilesStateProcessDefinitions, DBVersionsProcessContext> getScriptFilesStateRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<IncrementalScriptFileType>, DBVersionsProcessContext> createIncrementalNextScriptFileRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<RepeatableScriptFileType>, DBVersionsProcessContext> createRepeatableNextScriptFileRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<DevDummyDataScriptFileType>, DBVersionsProcessContext> createDevDummyDataNextScriptFileRunner,
                               NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> syncRunner,
                               NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> recreateDBFromScratchRunner,
                               NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> syncDBToSpecificStateRunner,
                               NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> createVirtualExecutionsRunner,
                               NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> deployVirtualExecutionsRunner)
        {
            _dbVersionsValidationsRunner = dbVersionsValidationsRunner;
            _targetStateScriptFileValidationRunner = targetStateScriptFileValidationRunner;

            _getScriptFilesStateRunner = getScriptFilesStateRunner;

            _createIncrementalNextScriptFileRunner = createIncrementalNextScriptFileRunner;
            _createRepeatableNextScriptFileRunner = createRepeatableNextScriptFileRunner;
            _createDevDummyDataNextScriptFileRunner = createDevDummyDataNextScriptFileRunner;

            _syncDBRunner = syncRunner;
            _recreateDBFromScratchRunner = recreateDBFromScratchRunner;
            _syncDBToSpecificStateRunner = syncDBToSpecificStateRunner;
            _createVirtualExecutionsRunner = createVirtualExecutionsRunner;

            _deployExecutionsRunner = deployVirtualExecutionsRunner;

        }




        #region Validation

        public ProcessResults ValidateDBVersions(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _dbVersionsValidationsRunner.Run(new DBVersionsProcessArgs(id, null, null), onNotificationStateChanged);
        }


        public ProcessResults ValdiateTargetStateAlreadyExecuted(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _targetStateScriptFileValidationRunner.Run(new DBVersionsProcessArgs(id, null, new TargetScripts(targetStateScriptFilename)), onNotificationStateChanged);
        }

        #endregion


        #region Run Change DB State

        public ProcessResults SyncDB(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _syncDBRunner.Run(new DBVersionsProcessArgs(id, null, TargetScripts.CreateLastState()), onNotificationStateChanged);
        }

        public ProcessResults RecreateDBFromScratch(string id, TargetScripts targetScripts, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _recreateDBFromScratchRunner.Run(new DBVersionsProcessArgs(id, null, targetScripts), onNotificationStateChanged);
        }


        public ProcessResults SetDBToSpecificState(string id, TargetScripts targetScripts, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessResults processTrace;

            if (isIgnoreHistoryWarning)
            {
                processTrace = RecreateDBFromScratch(id, targetScripts, onNotificationStateChanged);
            }
            else
            {
                processTrace = _syncDBToSpecificStateRunner.Run(new DBVersionsProcessArgs(id, null, targetScripts), onNotificationStateChanged);
            }

            return processTrace;
        }


        public ProcessResults SetDBStateByVirtualExecution(string id, TargetScripts targetScripts, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createVirtualExecutionsRunner.Run(new DBVersionsProcessArgs(id, null, targetScripts), onNotificationStateChanged);
        }


        #endregion


        #region Deploy

        public ProcessResults Deploy(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _deployExecutionsRunner.Run(new DBVersionsProcessArgs(id, null, null), onNotificationStateChanged);
        }

        #endregion



        #region Scripts

        public ProcessResults GetScriptFilesState(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {

            return _getScriptFilesStateRunner.Run(new DBVersionsProcessArgs(id, null, null), onNotificationStateChanged);
        }

        public ProcessResults CreateNewIncrementalScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createIncrementalNextScriptFileRunner.Run(new DBVersionsProcessArgs(id, scriptName, null), onNotificationStateChanged);
        }

        public ProcessResults CreateNewRepeatableScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createRepeatableNextScriptFileRunner.Run(new DBVersionsProcessArgs(id, scriptName, null), onNotificationStateChanged);
        }

        public ProcessResults CreateNewDevDummyDataScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createDevDummyDataNextScriptFileRunner.Run(new DBVersionsProcessArgs(id, scriptName, null), onNotificationStateChanged);
        }


        #endregion

    }
}
