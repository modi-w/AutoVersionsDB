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

namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDbAPI //: IDisposable
    {

        private static readonly object _processSyncLock = new object();

        //private static ArtifactExtractor _currentArtifactExtractor;


        private static DBCommandsFactoryProvider _dbCommandsFactoryProvider = NinjectUtils.KernelInstance.Get<DBCommandsFactoryProvider>();
        private static NotificationExecutersProviderFactory _notificationExecutersProviderFactory= NinjectUtils.KernelInstance.Get<NotificationExecutersProviderFactory>();

        //public ProjectConfigItem ProjectConfigItem { get; private set; }

        private static ConfigProjectsManager _configProjectsManager= NinjectUtils.KernelInstance.Get<ConfigProjectsManager>();

        private static ScriptFilesComparersManager _scriptFilesComparersManager = NinjectUtils.KernelInstance.Get<ScriptFilesComparersManager>();
        //public ScriptFilesComparersProvider CurrentScriptFilesComparersProvider
        //{
        //    get
        //    {
        //        return _scriptFilesComparersManager.GetScriptFilesComparersProvider(ProjectConfigItem.ProjectGuid);
        //    }
        //}





        //public bool HasError
        //{
        //    get
        //    {
        //        return NotificationExecutersFactoryManager.HasError;
        //    }
        //}
        //public string ErrorCode
        //{
        //    get
        //    {
        //        return NotificationExecutersFactoryManager.ErrorCode;
        //    }
        //}

        //public string InstructionsMessage
        //{
        //    get
        //    {
        //        return NotificationExecutersFactoryManager.InstructionsMessage;
        //    }
        //}

        //public string InstructionsMessageStepName
        //{
        //    get
        //    {
        //        return NotificationExecutersFactoryManager.InstructionsMessageStepName;
        //    }
        //}



        public static List<ProjectConfigItem> GetProjectsList()
        {
            return _configProjectsManager.ProjectConfigsList;
        }


        public static List<DBType> GetDbTypesList()
        {

            return _dbCommandsFactoryProvider.GetDbTypesList();
        }


        #region Config

        //private static void setProjectConfigItem(ProjectConfigItem projectConfigItem, Action<NotifictionStatesHistory, NotificationStateItem> onNotificationStateChanged)
        //{
        //    lock (_processSyncLock)
        //    {
        //        ProjectConfigItem = projectConfigItem;

        //        Refresh(onNotificationStateChanged);
        //    }
        //}

        public static void SaveProjectConfig(ProjectConfigItem projectConfigItem)
        {
            lock (_processSyncLock)
            {
                _configProjectsManager.AddOrUpdateProjectConfig(projectConfigItem);
            }
        }


        public static void RemoveProjectConfig(string projectGuid)
        {
            lock (_processSyncLock)
            {
                _configProjectsManager.RemoveProjectConfig(projectGuid);
            }
        }




        //public NotifictionStatesHistory Refresh(ProjectConfigItem projectConfigItem, Action<NotifictionStatesHistory, NotificationStateItem> onNotificationStateChanged)
        //{
        //    NotifictionStatesHistory processTrace;

        //    //if (_currentArtifactExtractor != null)
        //    //{
        //    //    _currentArtifactExtractor.Dispose();
        //    //}

        //    processTrace = ValidateProjectConfig(onNotificationStateChanged);

        //    if (!processTrace.HasError)
        //    {
        //        ArtifactExtractor _currentArtifactExtractor = ArtifactExtractorFactory.Create(projectConfigItem);

        //        RecreateScriptFilesComparersProvider();
        //    }

        //    return processTrace;
        //}


        //private void RecreateScriptFilesComparersProvider()
        //{
        //    _scriptFilesComparersManager.Load(ProjectConfigItem);
        //}


        #endregion


        #region Validation

        public static ProcessTrace ValidateAll(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                processTrace = ValidateArtifactFile(projectConfigItem,onNotificationStateChanged);

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

        public static ProcessTrace ValidateProjectConfig(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        private static ProcessTrace ValidateArtifactFile(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        private static ProcessTrace ValidateSystemTableExist(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processTrace;
        }


        private static ProcessTrace ValidateDBState(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DBStateValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        public static bool ValdiateTargetStateAlreadyExecuted(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(executionParams, onNotificationStateChanged);
                }
            }

            return !processTrace.HasError;
        }

        #endregion


        #region Run Change Db State

        public static ProcessTrace SyncDB(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }

 //               RecreateScriptFilesComparersProvider();
            }

            return processTrace;
        }

        public static ProcessTrace SetDBToSpecificState(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
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
                    ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                    using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngine>())
                    {
                        engine.Prepare(projectConfigItem);
                        processTrace = engine.Run(executionParams, onNotificationStateChanged);
                    }
                }

       //         RecreateScriptFilesComparersProvider();
            }

            return processTrace;
        }

        public static ProcessTrace RecreateDBFromScratch(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(executionParams, onNotificationStateChanged);
                }

    //            RecreateScriptFilesComparersProvider();
            }

            return processTrace;
        }

        public static ProcessTrace SetDBStateByVirtualExecution(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(executionParams, onNotificationStateChanged);
                }

  //              RecreateScriptFilesComparersProvider();
            }

            return processTrace;
        }



        private static ExecutionParams CreateTargetStepExectionParams(string targetStateScriptFilename)
        {
            AutoVersionsDBExecutionParams targetStateScriptExecutionParam = new AutoVersionsDBExecutionParams()
            {
                TargetStateScriptFileName = targetStateScriptFilename
            };

            return targetStateScriptExecutionParam;
        }

        #endregion


        #region Deploy

        public static ProcessTrace Deploy(ProjectConfigItem projectConfigItem, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessTrace processTrace;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DeployEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processTrace = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processTrace;
        }

        #endregion



        #region Scripts

        public static ScriptFilesComparersProvider CreateScriptFilesState(ProjectConfigItem projectConfigItem)
        {
            _scriptFilesComparersManager.Load(projectConfigItem);

            ScriptFilesComparersProvider scriptFilesComparersProvider;

            using (ArtifactExtractor _currentArtifactExtractor = ArtifactExtractorFactory.Create(projectConfigItem))
            {
                scriptFilesComparersProvider = _scriptFilesComparersManager.GetScriptFilesComparersProvider(projectConfigItem.ProjectGuid);
            }

            return scriptFilesComparersProvider;
        }

        public static string CreateNewIncrementalScriptFile(ProjectConfigItem projectConfigItem, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesComparersProvider scriptFilesComparersProvider = AutoVersionsDbAPI.CreateScriptFilesState(projectConfigItem);
                scriptFileItem = scriptFilesComparersProvider.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }

        public static string CreateNewRepeatableScriptFile(ProjectConfigItem projectConfigItem, string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesComparersProvider scriptFilesComparersProvider = AutoVersionsDbAPI.CreateScriptFilesState(projectConfigItem);
                scriptFileItem = scriptFilesComparersProvider.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }

        public static string CreateNewDevDummyDataScriptFile(ProjectConfigItem projectConfigItem, string scriptName)
        {
            if (!projectConfigItem.IsDevEnvironment)
            {
                throw new Exception("DevdummyData Scripts not allow in Delivery environment");
            }

            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                ScriptFilesComparersProvider scriptFilesComparersProvider = AutoVersionsDbAPI.CreateScriptFilesState(projectConfigItem);
                scriptFileItem = scriptFilesComparersProvider.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);
            }

            return scriptFileItem.FileFullPath;
        }


        #endregion



        //#region IDisposable

        //private bool _disposed = false;

        //~AutoVersionsDbAPI() => Dispose(false);

        //// Public implementation of Dispose pattern callable by consumers.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (_disposed)
        //    {
        //        return;
        //    }

        //    if (disposing)
        //    {
        //        if (_currentArtifactExtractor != null)
        //        {
        //            _currentArtifactExtractor.Dispose();
        //            _currentArtifactExtractor = null;
        //        }

        //        if (ScriptFilesComparersManager != null)
        //        {
        //            ScriptFilesComparersManager.Dispose();
        //        }

        //    }

        //    _disposed = true;
        //}

        //#endregion


    }
}
