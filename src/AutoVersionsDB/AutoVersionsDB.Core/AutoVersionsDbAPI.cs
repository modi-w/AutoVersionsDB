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
        //    NotifictionStatesHistory processResult;

        //    //if (_currentArtifactExtractor != null)
        //    //{
        //    //    _currentArtifactExtractor.Dispose();
        //    //}

        //    processResult = ValidateProjectConfig(onNotificationStateChanged);

        //    if (!processResult.HasError)
        //    {
        //        ArtifactExtractor _currentArtifactExtractor = ArtifactExtractorFactory.Create(projectConfigItem);

        //        RecreateScriptFilesComparersProvider();
        //    }

        //    return processResult;
        //}


        //private void RecreateScriptFilesComparersProvider()
        //{
        //    _scriptFilesComparersManager.Load(ProjectConfigItem);
        //}


        #endregion


        #region Validation

        public static ProcessStateResults ValidateAll(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                processResult = ValidateArtifactFile(projectConfigItem,onNotificationStateChanged);

                if (!processResult.HasError)
                {
                    processResult = ValidateProjectConfig(projectConfigItem, onNotificationStateChanged);

                    if (!processResult.HasError)
                    {
                        processResult = ValidateSystemTableExist(projectConfigItem, onNotificationStateChanged);

                        if (!processResult.HasError)
                        {
                            processResult = ValidateDBState(projectConfigItem, onNotificationStateChanged);
                        }
                    }
                }
            }

            return processResult;
        }

        public static ProcessStateResults ValidateProjectConfig(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        private static ProcessStateResults ValidateArtifactFile(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        private static ProcessStateResults ValidateSystemTableExist(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }


        private static ProcessStateResults ValidateDBState(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DBStateValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        public static bool ValdiateTargetStateAlreadyExecuted(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }
            }

            return !processResult.HasError;
        }

        #endregion


        #region Run Change Db State

        public static ProcessStateResults SyncDB(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }

 //               RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public static ProcessStateResults SetDBToSpecificState(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                if (isIgnoreHistoryWarning)
                {
                    processResult = RecreateDBFromScratch(projectConfigItem, targetStateScriptFilename, onNotificationStateChanged);
                }
                else
                {
                    ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                    using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngine>())
                    {
                        engine.Prepare(projectConfigItem);
                        processResult = engine.Run(executionParams, onNotificationStateChanged);
                    }
                }

       //         RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public static ProcessStateResults RecreateDBFromScratch(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }

    //            RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public static ProcessStateResults SetDBStateByVirtualExecution(ProjectConfigItem projectConfigItem, string targetStateScriptFilename, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }

  //              RecreateScriptFilesComparersProvider();
            }

            return processResult;
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

        public static ProcessStateResults Deploy(ProjectConfigItem projectConfigItem, Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            ProcessStateResults processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DeployEngine>())
                {
                    engine.Prepare(projectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
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
