using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions
{
    public class DBVersionsAPI
    {
        private readonly NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> _dbVersionsValidationsRunner;
        private readonly NotificationProcessRunner<ProjectConfigValidationProcessDefinition, DBVersionsProcessContext> _projectConfigValidationRunner;
        private readonly NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> _targetStateScriptFileValidationRunner;

        private readonly NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> _syncDBProcessRunner;
        private readonly NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> _recreateDBFromScratchRunner;
        private readonly NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> _syncDBToSpecificStateRunner;
        private readonly NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> _createVirtualExecutionsRunner;

        private readonly NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> _deployVirtualExecutionsRunner;

        private readonly ScriptFilesStateFactory _scriptFilesStateFactory;



        public DBVersionsAPI(NotificationProcessRunner<DBVersionsValidationsProcessDefinitions, DBVersionsProcessContext> dbVersionsValidationsRunner,
                               NotificationProcessRunner<ProjectConfigValidationProcessDefinition, DBVersionsProcessContext> projectConfigValidationRunner,
                               NotificationProcessRunner<TargetStateScriptFileValidationProcessDefinition, DBVersionsProcessContext> targetStateScriptFileValidationRunner,
                               NotificationProcessRunner<SyncDBProcessDefinition, DBVersionsProcessContext> syncDBProcessRunner,
                               NotificationProcessRunner<RecreateDBFromScratchProcessDefinition, DBVersionsProcessContext> recreateDBFromScratchRunner,
                               NotificationProcessRunner<SyncDBToSpecificStateProcessDefinition, DBVersionsProcessContext> syncDBToSpecificStateRunner,
                               NotificationProcessRunner<CreateVirtualExecutionsProcessDefinition, DBVersionsProcessContext> createVirtualExecutionsRunner,
                               NotificationProcessRunner<DeployProcessDefinition, DBVersionsProcessContext> deployVirtualExecutionsRunner,
                               ScriptFilesStateFactory scriptFilesStateFactory)
        {
            _dbVersionsValidationsRunner = dbVersionsValidationsRunner;
            _projectConfigValidationRunner = projectConfigValidationRunner;
            _targetStateScriptFileValidationRunner = targetStateScriptFileValidationRunner;
            
            _syncDBProcessRunner = syncDBProcessRunner;
            _recreateDBFromScratchRunner = recreateDBFromScratchRunner;
            _syncDBToSpecificStateRunner = syncDBToSpecificStateRunner;
            _createVirtualExecutionsRunner = createVirtualExecutionsRunner;

            _deployVirtualExecutionsRunner = deployVirtualExecutionsRunner;

            _scriptFilesStateFactory = scriptFilesStateFactory;
        }




        #region Validation

        public ProcessTrace ValidateDBVersions(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _dbVersionsValidationsRunner.Run(new DBVersionsProcessParams(projectCode, null), onNotificationStateChanged);
        }

        public ProcessTrace ValidateProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _projectConfigValidationRunner.Run(new DBVersionsProcessParams(projectCode, null), onNotificationStateChanged);
        }


        public bool ValdiateTargetStateAlreadyExecuted(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace = _targetStateScriptFileValidationRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename), onNotificationStateChanged);
            return !processTrace.HasError;
        }

        #endregion


        #region Run Change Db State

        public ProcessTrace SyncDB(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _syncDBProcessRunner.Run(new DBVersionsProcessParams(projectCode, null), onNotificationStateChanged);
        }

        public ProcessTrace RecreateDBFromScratch(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _recreateDBFromScratchRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename), onNotificationStateChanged);
        }


        public ProcessTrace SetDBToSpecificState(string projectCode, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            if (isIgnoreHistoryWarning)
            {
                processTrace = RecreateDBFromScratch(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            }
            else
            {
                processTrace = _syncDBToSpecificStateRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename), onNotificationStateChanged);
            }

            return processTrace;
        }


        public ProcessTrace SetDBStateByVirtualExecution(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _createVirtualExecutionsRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename), onNotificationStateChanged);
        }


        #endregion


        #region Deploy

        public ProcessTrace Deploy(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return _deployVirtualExecutionsRunner.Run(new DBVersionsProcessParams(projectCode, null), onNotificationStateChanged);
        }

        #endregion



        #region Scripts

        public ScriptFilesState CreateScriptFilesState(ProjectConfigItem projectConfig)
        {
            ScriptFilesState scriptFilesState;

            scriptFilesState = _scriptFilesStateFactory.Create();
            scriptFilesState.Reload(projectConfig);

            return scriptFilesState;
        }

        public string CreateNewIncrementalScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            ScriptFilesState scriptFilesState = this.CreateScriptFilesState(projectConfig);
            scriptFileItem = scriptFilesState.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewRepeatableScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            ScriptFilesState scriptFilesState = this.CreateScriptFilesState(projectConfig);
            scriptFileItem = scriptFilesState.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewDevDummyDataScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            if (!projectConfig.IsDevEnvironment)
            {
                throw new Exception("DevdummyData Scripts not allow in Delivery environment");
            }

            RuntimeScriptFileBase scriptFileItem;

            ScriptFilesState scriptFilesState = this.CreateScriptFilesState(projectConfig);
            scriptFileItem = scriptFilesState.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);

            return scriptFileItem.FileFullPath;
        }


        #endregion

    }
}
