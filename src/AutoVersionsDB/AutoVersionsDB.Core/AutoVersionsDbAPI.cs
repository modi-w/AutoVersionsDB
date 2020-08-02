using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using Ninject;

namespace AutoVersionsDB.Core
{
    public class AutoVersionsDbAPI : IDisposable
    {
        public static readonly AutoVersionsDbAPI Instance = NinjectUtils.KernelInstance.Get<AutoVersionsDbAPI>();


        private readonly object _processSyncLock = new object();

        private ArtifactExtractor _currentArtifactExtractor;


        public DBCommandsFactoryProvider DBCommandsFactoryProvider { get; }
        public NotificationExecutersProvider NotificationExecutersFactoryManager { get; }

        public ProjectConfigItem ProjectConfigItem { get; private set; }

        public ConfigProjectsManager ConfigProjectsManager { get; }

        public ScriptFilesComparersManager ScriptFilesComparersManager { get; }
        public ScriptFilesComparersProvider CurrentScriptFilesComparersProvider
        {
            get
            {
                return ScriptFilesComparersManager.GetScriptFilesComparersProvider(ProjectConfigItem.ProjectGuid);
            }
        }





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





        public AutoVersionsDbAPI(ConfigProjectsManager configProjectsManager,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                ScriptFilesComparersManager scriptFilesComparersManager,
                                NotificationExecutersProvider notificationExecutersFactoryManager)
        {
            ConfigProjectsManager = configProjectsManager;

            DBCommandsFactoryProvider = dbCommandsFactoryProvider;
            ScriptFilesComparersManager = scriptFilesComparersManager;

            NotificationExecutersFactoryManager = notificationExecutersFactoryManager;

        }


        #region Config

        public void SetProjectConfigItem(ProjectConfigItem projectConfigItem, Action<NotificationStateItem> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                ProjectConfigItem = projectConfigItem;

                Refresh(onNotificationStateChanged);
            }
        }

        public void SaveProjectConfig()
        {
            lock (_processSyncLock)
            {
                ConfigProjectsManager.AddOrUpdateProjectConfig(ProjectConfigItem);
            }
        }

        public NotifictionStatesHistory Refresh(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            if (_currentArtifactExtractor != null)
            {
                _currentArtifactExtractor.Dispose();
            }

            processResult = ValidateProjectConfig(onNotificationStateChanged);

            if (!processResult.HasError)
            {
                _currentArtifactExtractor = ArtifactExtractorFactory.Create(ProjectConfigItem);

                RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }


        private void RecreateScriptFilesComparersProvider()
        {
            ScriptFilesComparersManager.Load(ProjectConfigItem);
        }


        #endregion


        #region Validation

        public NotifictionStatesHistory ValidateAll(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                processResult = ValidateArtifactFile(onNotificationStateChanged);

                if (!processResult.HasError)
                {
                    processResult = ValidateProjectConfig(onNotificationStateChanged);

                    if (!processResult.HasError)
                    {
                        processResult = ValidateSystemTableExist(onNotificationStateChanged);

                        if (!processResult.HasError)
                        {
                            processResult = ValidateDBState(onNotificationStateChanged);
                        }
                    }
                }
            }

            return processResult;
        }

        public NotifictionStatesHistory ValidateProjectConfig(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        private NotifictionStatesHistory ValidateArtifactFile(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        private NotifictionStatesHistory ValidateSystemTableExist(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }


        private NotifictionStatesHistory ValidateDBState(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DBStateValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        public bool ValdiateTargetStateAlreadyExecuted(string targetStateScriptFilename, Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }
            }

            return !processResult.HasError;
        }

        #endregion


        #region Run Change Db State

        public NotifictionStatesHistory SyncDB(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(null, onNotificationStateChanged);
                }

                RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public NotifictionStatesHistory SetDBToSpecificState(string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                if (isIgnoreHistoryWarning)
                {
                    processResult = RecreateDBFromScratch(targetStateScriptFilename, onNotificationStateChanged);
                }
                else
                {
                    ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                    using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngine>())
                    {
                        engine.Prepare(ProjectConfigItem);
                        processResult = engine.Run(executionParams, onNotificationStateChanged);
                    }
                }

                RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public NotifictionStatesHistory RecreateDBFromScratch(string targetStateScriptFilename, Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }

                RecreateScriptFilesComparersProvider();
            }

            return processResult;
        }

        public NotifictionStatesHistory SetDBStateByVirtualExecution(string targetStateScriptFilename, Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult = engine.Run(executionParams, onNotificationStateChanged);
                }

                RecreateScriptFilesComparersProvider();
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

        public NotifictionStatesHistory Deploy(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory processResult;

            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DeployEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    processResult= engine.Run(null, onNotificationStateChanged);
                }
            }

            return processResult;
        }

        #endregion



        #region Script


        public string CreateNewIncrementalScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                scriptFileItem = CurrentScriptFilesComparersProvider.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                RecreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewRepeatableScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                scriptFileItem = CurrentScriptFilesComparersProvider.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                RecreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewDevDummyDataScriptFile(string scriptName)
        {
            if (!ProjectConfigItem.IsDevEnvironment)
            {
                throw new Exception("DevdummyData Scripts not allow in Delivery environment");
            }

            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                scriptFileItem = CurrentScriptFilesComparersProvider.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                RecreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }


        #endregion



        #region IDisposable

        private bool _disposed = false;

        ~AutoVersionsDbAPI() => Dispose(false);

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
                if (_currentArtifactExtractor != null)
                {
                    _currentArtifactExtractor.Dispose();
                    _currentArtifactExtractor = null;
                }

                if (ScriptFilesComparersManager != null)
                {
                    ScriptFilesComparersManager.Dispose();
                }

            }

            _disposed = true;
        }

        #endregion


    }
}
