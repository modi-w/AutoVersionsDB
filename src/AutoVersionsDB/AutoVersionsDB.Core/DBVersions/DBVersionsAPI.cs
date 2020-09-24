using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;

namespace AutoVersionsDB.Core.DBVersions
{
    public class DBVersionsAPI
    {
        private readonly NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> _dbVersionsValidationsRunner;
        private readonly NotificationProcessRunner<ProjectConfigValidationProcessDefinition, DBVersionsProcessContext> _projectConfigValidationRunner;
        private readonly NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> _targetStateScriptFileValidationRunner;

        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<IncrementalScriptFileType>, DBVersionsProcessContext> _createIncrementalNextScriptFileRunner;
        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<RepeatableScriptFileType>, DBVersionsProcessContext> _createRepeatableNextScriptFileRunner;
        private readonly NotificationProcessRunner<CreateNextScriptFileProcessDefinition<DevDummyDataScriptFileType>, DBVersionsProcessContext> _createDevDummyDataNextScriptFileRunner;

        private readonly NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> _syncDBRunner;
        private readonly NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> _recreateDBFromScratchRunner;
        private readonly NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> _syncDBToSpecificStateRunner;
        private readonly NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> _createVirtualExecutionsRunner;

        private readonly NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> _deployVirtualExecutionsRunner;

        private readonly ScriptFilesStateFactory _scriptFilesStateFactory;



        public DBVersionsAPI(NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> dbVersionsValidationsRunner,
                               NotificationProcessRunner<ProjectConfigValidationProcessDefinition, DBVersionsProcessContext> projectConfigValidationRunner,
                               NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> targetStateScriptFileValidationRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<IncrementalScriptFileType>, DBVersionsProcessContext> createIncrementalNextScriptFileRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<RepeatableScriptFileType>, DBVersionsProcessContext> createRepeatableNextScriptFileRunner,
                               NotificationProcessRunner<CreateNextScriptFileProcessDefinition<DevDummyDataScriptFileType>, DBVersionsProcessContext> createDevDummyDataNextScriptFileRunner,
                               NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> syncRunner,
                               NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> recreateDBFromScratchRunner,
                               NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> syncDBToSpecificStateRunner,
                               NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> createVirtualExecutionsRunner,
                               NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> deployVirtualExecutionsRunner,
                               ScriptFilesStateFactory scriptFilesStateFactory)
        {
            _dbVersionsValidationsRunner = dbVersionsValidationsRunner;
            _projectConfigValidationRunner = projectConfigValidationRunner;
            _targetStateScriptFileValidationRunner = targetStateScriptFileValidationRunner;

            _createIncrementalNextScriptFileRunner = createIncrementalNextScriptFileRunner;
            _createRepeatableNextScriptFileRunner = createRepeatableNextScriptFileRunner;
            _createDevDummyDataNextScriptFileRunner = createDevDummyDataNextScriptFileRunner;

            _syncDBRunner = syncRunner;
            _recreateDBFromScratchRunner = recreateDBFromScratchRunner;
            _syncDBToSpecificStateRunner = syncDBToSpecificStateRunner;
            _createVirtualExecutionsRunner = createVirtualExecutionsRunner;

            _deployVirtualExecutionsRunner = deployVirtualExecutionsRunner;

            _scriptFilesStateFactory = scriptFilesStateFactory;
        }




        #region Validation

        public ProcessResults ValidateDBVersions(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _dbVersionsValidationsRunner.Run(new DBVersionsProcessParams(projectCode, null, null), onNotificationStateChanged);
        }

        public ProcessResults ValidateProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _projectConfigValidationRunner.Run(new DBVersionsProcessParams(projectCode, null, null), onNotificationStateChanged);
        }


        public bool ValdiateTargetStateAlreadyExecuted(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessResults processResults = _targetStateScriptFileValidationRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename, null), onNotificationStateChanged);
            return !processResults.Trace.HasError;
        }

        #endregion


        #region Run Change Db State

        public ProcessResults SyncDB(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _syncDBRunner.Run(new DBVersionsProcessParams(projectCode, null, null), onNotificationStateChanged);
        }

        public ProcessResults RecreateDBFromScratch(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _recreateDBFromScratchRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename, null), onNotificationStateChanged);
        }


        public ProcessResults SetDBToSpecificState(string projectCode, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessResults processTrace;

            if (isIgnoreHistoryWarning)
            {
                processTrace = RecreateDBFromScratch(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            }
            else
            {
                processTrace = _syncDBToSpecificStateRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename, null), onNotificationStateChanged);
            }

            return processTrace;
        }


        public ProcessResults SetDBStateByVirtualExecution(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createVirtualExecutionsRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename, null), onNotificationStateChanged);
        }


        #endregion


        #region Deploy

        public ProcessResults Deploy(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _deployVirtualExecutionsRunner.Run(new DBVersionsProcessParams(projectCode, null, null), onNotificationStateChanged);
        }

        #endregion



        #region Scripts

        public ScriptFilesState CreateScriptFilesState(ProjectConfigItem projectConfig)
        {
            ScriptFilesState scriptFilesState = _scriptFilesStateFactory.Create();

            scriptFilesState.Reload(projectConfig);

            return scriptFilesState;
        }

        public ProcessResults CreateNewIncrementalScriptFile(string projectCode, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
           return _createIncrementalNextScriptFileRunner.Run(new DBVersionsProcessParams(projectCode, null, scriptName), onNotificationStateChanged);
        }

        public ProcessResults CreateNewRepeatableScriptFile(string projectCode, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createRepeatableNextScriptFileRunner.Run(new DBVersionsProcessParams(projectCode, null, scriptName), onNotificationStateChanged);
        }

        public ProcessResults CreateNewDevDummyDataScriptFile(string projectCode, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createDevDummyDataNextScriptFileRunner.Run(new DBVersionsProcessParams(projectCode, null, scriptName), onNotificationStateChanged);
        }


        #endregion

    }
}
