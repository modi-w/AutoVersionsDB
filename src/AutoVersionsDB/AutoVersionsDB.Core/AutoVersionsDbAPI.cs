using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;

namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDbAPI
    {

        private static readonly object _processSyncLock = new object();


        public static List<DBType> GetDbTypesList()
        {
            DBCommandsFactoryProvider dbCommandsFactoryProvider = NinjectUtils.KernelInstance.Get<DBCommandsFactoryProvider>();

            return dbCommandsFactoryProvider.GetDbTypesList();
        }






        #region Config

        public static List<ProjectConfigItem> GetProjectsList()
        {
            ProjectConfigs projectConfigs = NinjectUtils.KernelInstance.Get<ProjectConfigs>();

            return projectConfigs.GetAllProjectConfigs().Values.ToList();
        }

        public static ProjectConfigItem GetProjectConfigByProjectCode(string projectCode)
        {
            ProjectConfigs projectConfigs = NinjectUtils.KernelInstance.Get<ProjectConfigs>();

            return projectConfigs.GetProjectConfigByProjectCode(projectCode);
        }



        public static ProcessTrace SaveNewProjectConfig(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return runProjectConfigProcess<SaveNewProjectConfigProcessDefinition>(projectConfigItem, onNotificationStateChanged);
            }
        }

        public static ProcessTrace UpdateProjectConfig(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return runProjectConfigProcess<UpdateProjectConfigProcessDefinition>(projectConfigItem, onNotificationStateChanged);
            }
        }

        public static ProcessTrace ChangeProjectCode(string prevProjectCode, string newProjectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return runProjectConfigProcess<ChangeProjectCodeProcessDefinition>(prevProjectCode, newProjectCode, onNotificationStateChanged);
            }
        }

        public static ProcessTrace RemoveProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return runProjectConfigProcess<RemoveProjectConfigProcessDefinition>(projectCode, onNotificationStateChanged);
            }
        }






        #endregion


        #region Validation

        public static ProcessTrace ValidateAll(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                processTrace = ValidateArtifactFile(projectCode, onNotificationStateChanged);

                if (!processTrace.HasError)
                {
                    processTrace = ValidateProjectConfig(projectCode, onNotificationStateChanged);

                    if (!processTrace.HasError)
                    {
                        processTrace = ValidateSystemTableExist(projectCode, onNotificationStateChanged);

                        if (!processTrace.HasError)
                        {
                            processTrace = ValidateDBState(projectCode, onNotificationStateChanged);
                        }
                    }
                }
            }

            return processTrace;
        }

        public static ProcessTrace ValidateProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<ProjectConfigValidationProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }

        private static ProcessTrace ValidateArtifactFile(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<ArtifactFileValidationProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }

        private static ProcessTrace ValidateSystemTableExist(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<SystemTableExsitValidationProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }


        private static ProcessTrace ValidateDBState(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<DBStateValidationProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }

        public static bool ValdiateTargetStateAlreadyExecuted(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace = runDBVersionsProcess<TargetStateScriptFileValidationProcessDefinition>(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            return !processTrace.HasError;
        }

        #endregion


        #region Run Change Db State
    
        public static ProcessTrace SyncDB(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<SyncDBProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }

        public static ProcessTrace SetDBToSpecificState(string projectCode, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {      
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                if (isIgnoreHistoryWarning)
                {
                    processTrace = RecreateDBFromScratch(projectCode, targetStateScriptFilename, onNotificationStateChanged);
                }
                else
                {
                    processTrace = runDBVersionsProcess<SyncDBToSpecificStateProcessDefinition>(projectCode, targetStateScriptFilename, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        public static ProcessTrace RecreateDBFromScratch(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<RecreateDBFromScratchProcessDefinition>(projectCode, targetStateScriptFilename, onNotificationStateChanged);
        }

        public static ProcessTrace SetDBStateByVirtualExecution(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<CreateVirtualExecutionsProcessDefinition>(projectCode, targetStateScriptFilename, onNotificationStateChanged);
        }




        #endregion


        #region Deploy

        public static ProcessTrace Deploy(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runDBVersionsProcess<DeployProcessDefinition>(projectCode, null, onNotificationStateChanged);
        }

        #endregion



        #region Scripts

        public static ScriptFilesState CreateScriptFilesState(ProjectConfigItem projectConfigItem)
        {
            ScriptFilesState scriptFilesState;

            ScriptFilesStateFactory scriptFilesStateFactory = NinjectUtils.KernelInstance.Get<ScriptFilesStateFactory>();

            scriptFilesState = scriptFilesStateFactory.Create();
            scriptFilesState.Reload(projectConfigItem);

            return scriptFilesState;
        }

        public static string CreateNewIncrementalScriptFile(ProjectConfigItem projectConfigItem, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesState scriptFilesState = AutoVersionsDbAPI.CreateScriptFilesState(projectConfigItem);
                scriptFileItem = scriptFilesState.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }

        public static string CreateNewRepeatableScriptFile(ProjectConfigItem projectConfigItem, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesState scriptFilesState = AutoVersionsDbAPI.CreateScriptFilesState(projectConfigItem);
                scriptFileItem = scriptFilesState.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }

        public static string CreateNewDevDummyDataScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            if (!projectConfig.IsDevEnvironment)
            {
                throw new Exception("DevdummyData Scripts not allow in Delivery environment");
            }

            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesState scriptFilesState = AutoVersionsDbAPI.CreateScriptFilesState(projectConfig);
                scriptFileItem = scriptFilesState.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }


        #endregion




        private static ProcessTrace runProjectConfigProcess<TProcessDefinition>(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
            where TProcessDefinition : ProcessDefinition
        {
            ProcessTrace processTrace;
            lock (_processSyncLock)
            {
                var processRunner = NinjectUtils.KernelInstance.Get<NotificationProcessRunner<TProcessDefinition, ProjectConfigProcessContext>>();
                processTrace = processRunner.Run(new ProjectConfigProcessParams(projectConfig), onNotificationStateChanged);
            }

            return processTrace;
        }
        private static ProcessTrace runProjectConfigProcess<TProcessDefinition>(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
            where TProcessDefinition : ProcessDefinition
        {
            ProcessTrace processTrace;
            lock (_processSyncLock)
            {
                var processRunner = NinjectUtils.KernelInstance.Get<NotificationProcessRunner<TProcessDefinition, ProjectConfigProcessContext>>();
                processTrace = processRunner.Run(new ProjectConfigProcessParams(projectCode), onNotificationStateChanged);
            }

            return processTrace;
        }
        private static ProcessTrace runProjectConfigProcess<TProcessDefinition>(string projectCode, string newProjectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
            where TProcessDefinition : ProcessDefinition
        {
            ProcessTrace processTrace;
            lock (_processSyncLock)
            {
                var processRunner = NinjectUtils.KernelInstance.Get<NotificationProcessRunner<TProcessDefinition, ProjectConfigProcessContext>>();
                processTrace = processRunner.Run(new ProjectConfigProcessParams(projectCode, newProjectCode), onNotificationStateChanged);
            }

            return processTrace;
        }

        private static ProcessTrace runDBVersionsProcess<TProcessDefinition>(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
            where TProcessDefinition : ProcessDefinition
        {
            ProcessTrace processTrace;
            lock (_processSyncLock)
            {
                var processRunner = NinjectUtils.KernelInstance.Get<NotificationProcessRunner<TProcessDefinition, DBVersionsProcessContext>>();
                processTrace = processRunner.Run(new DBVersionsProcessParams(projectCode, targetStateScriptFilename), onNotificationStateChanged);
            }

            return processTrace;
        }


    }
}
