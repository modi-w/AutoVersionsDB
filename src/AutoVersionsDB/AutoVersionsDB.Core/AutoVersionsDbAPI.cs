using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using AutoVersionsDB.Core.Utils;

namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDbAPI
    {

        private static readonly object _processSyncLock = new object();


        public static List<ProjectConfigItem> GetProjectsList()
        {
            return ProjectConfigs.GetAllProjectConfigs().Values.ToList();
        }


        public static List<DBType> GetDbTypesList()
        {
            DBCommandsFactoryProvider dbCommandsFactoryProvider = NinjectUtils.KernelInstance.Get<DBCommandsFactoryProvider>();

            return dbCommandsFactoryProvider.GetDbTypesList();
        }


        #region Config


        public static void SaveProjectConfig(ProjectConfigItem projectConfigItem)
        {
            lock (_processSyncLock)
            {
                ProjectConfigs.AddOrUpdateProjectConfig(projectConfigItem);
            }
        }


        public static void RemoveProjectConfig(string projectGuid)
        {
            lock (_processSyncLock)
            {
                ProjectConfigs.RemoveProjectConfig(projectGuid);
            }
        }




        #endregion


        #region Validation

        public static ProcessTrace ValidateAll(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                processTrace = ValidateArtifactFile(projectConfigItem, onNotificationStateChanged);

                if (!processTrace.HasError)
                {
                    processTrace = ValidateProjectConfig(projectConfigItem, onNotificationStateChanged);

                    if (!processTrace.HasError)
                    {
                        processTrace = ValidateSystemTableExist(projectConfigItem, onNotificationStateChanged);

                        if (!processTrace.HasError)
                        {
                            processTrace = ValidateDBState(projectConfigItem, onNotificationStateChanged);
                        }
                    }
                }
            }

            return processTrace;
        }

        public static ProcessTrace ValidateProjectConfig(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<ProjectConfigValidationEngine>(projectConfigItem, null, onNotificationStateChanged);
        }

        private static ProcessTrace ValidateArtifactFile(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<ArtifactFileValidationEngineSetting>(projectConfigItem, null, onNotificationStateChanged);
        }

        private static ProcessTrace ValidateSystemTableExist(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<SystemTableExsitValidationEngine>(projectConfigItem, null, onNotificationStateChanged);
        }


        private static ProcessTrace ValidateDBState(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<DBStateValidationEngine>(projectConfigItem, null, onNotificationStateChanged);
        }

        public static bool ValdiateTargetStateAlreadyExecuted(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            ProcessTrace processTrace = runProcess<TargetStateScriptFileValidationEngine>(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
            return !processTrace.HasError;
        }

        #endregion


        #region Run Change Db State

        public static ProcessTrace SyncDB(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<SyncDBEngine>(projectConfigItem, null, onNotificationStateChanged);
        }

        public static ProcessTrace SetDBToSpecificState(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {      
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                if (isIgnoreHistoryWarning)
                {
                    processTrace = RecreateDBFromScratch(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
                }
                else
                {
                    processTrace = runProcess<SyncDBToSpecificStateEngine>(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        public static ProcessTrace RecreateDBFromScratch(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<RecreateDBFromScratchEngine>(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
        }

        public static ProcessTrace SetDBStateByVirtualExecution(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<CreateVirtualExecutionsEngine>(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
        }




        #endregion


        #region Deploy

        public static ProcessTrace Deploy(ProjectConfigItem projectConfigItem, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return runProcess<DeployEngine>(projectConfigItem, null, onNotificationStateChanged);
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





        private static ProcessTrace runProcess<TEngineSettings>(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
            where TEngineSettings : EngineSettings
        {
            ProcessTrace processTrace;
            lock (_processSyncLock)
            {
                var engineRunner = NinjectUtils.KernelInstance.Get<NotificationEngineRunner<TEngineSettings, AutoVersionsDbEngineContext>>();
                processTrace = engineRunner.Run(new AutoVersionsDBExecutionParams(projectConfigItem, targetStateScriptFilename), onNotificationStateChanged);
            }

            return processTrace;
        }


    }
}
