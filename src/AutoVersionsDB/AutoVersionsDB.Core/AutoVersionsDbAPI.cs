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



        public DBCommandsFactoryProvider DBCommandsFactoryProvider { get; private set; }
        public ScriptFilesComparersProvider ScriptFilesComparersProvider { get; private set; }
        public NotificationExecutersFactoryManager NotificationExecutersFactoryManager { get; private set; }

        public ProjectConfigItem ProjectConfigItem { get; private set; }
        private ArtifactExtractor _currentArtifactExtractor;

        public ConfigProjectsManager ConfigProjectsManager { get; private set; }





        public bool HasError
        {
            get
            {
                return NotificationExecutersFactoryManager.HasError;
            }
        }
        public string ErrorCode
        {
            get
            {
                return NotificationExecutersFactoryManager.ErrorCode;
            }
        }

        public string InstructionsMessage
        {
            get
            {
                return NotificationExecutersFactoryManager.InstructionsMessage;
            }
        }

        public string InstructionsMessageStepName
        {
            get
            {
                return NotificationExecutersFactoryManager.InstructionsMessageStepName;
            }
        }





        public AutoVersionsDbAPI(ConfigProjectsManager configProjectsManager,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                ScriptFilesComparersProvider scriptFilesComparersProvider,
                                NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            ConfigProjectsManager = configProjectsManager;

            DBCommandsFactoryProvider = dbCommandsFactoryProvider;
            ScriptFilesComparersProvider = scriptFilesComparersProvider;

            NotificationExecutersFactoryManager = notificationExecutersFactoryManager;

        }


        #region Config

        public void SetProjectConfigItem(ProjectConfigItem projectConfigItem)
        {
            lock (_processSyncLock)
            {
                ProjectConfigItem = projectConfigItem;

                Refresh();
            }
        }

        public void SaveProjectConfig()
        {
            lock (_processSyncLock)
            {
                ConfigProjectsManager.AddOrUpdateProjectConfig(ProjectConfigItem);
            }
        }

        public void Refresh()
        {
            if (_currentArtifactExtractor != null)
            {
                _currentArtifactExtractor.Dispose();
            }

            ValidateProjectConfig();

            if (!HasError)
            {
                _currentArtifactExtractor = ArtifactExtractorFactory.Create(ProjectConfigItem);

                RecreateScriptFilesComparersProvider();
            }
        }


        private void RecreateScriptFilesComparersProvider()
        {
         //   ScriptFilesComparersProvider.SetProjectConfig(ProjectConfigItem);
            ScriptFilesComparersProvider.Reload(ProjectConfigItem);
        }


        #endregion


        #region Validation

        public void ValidateAll()
        {
            lock (_processSyncLock)
            {
                ValidateArtifactFile();

                if (!HasError)
                {
                    ValidateProjectConfig();

                    if (!HasError)
                    {
                        ValidateSystemTableExist();

                        if (!HasError)
                        {
                            ValidateDBState();
                        }
                    }
                }
            }
        }

        public void ValidateProjectConfig()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }
           }
        }

        private void ValidateArtifactFile()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }
            }
        }

        private void ValidateSystemTableExist()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }
            }
        }


        private void ValidateDBState()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DBStateValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }
            }
        }

        public bool ValdiateTargetStateAlreadyExecuted(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(executionParams);
                }
            }

            return !NotificationExecutersFactoryManager.HasError;
        }

        #endregion


        #region Run Change Db State

        public void SyncDB()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }

                RecreateScriptFilesComparersProvider();
            }
        }

        public void SetDBToSpecificState(string targetStateScriptFilename, bool isIgnoreHistoryWarning)
        {
            lock (_processSyncLock)
            {
                if (isIgnoreHistoryWarning)
                {
                    RecreateDBFromScratch(targetStateScriptFilename);
                }
                else
                {
                    ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                    using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngine>())
                    {
                        engine.Prepare(ProjectConfigItem);
                        engine.Run(executionParams);
                    }
                }

                RecreateScriptFilesComparersProvider();
            }
        }

        public void RecreateDBFromScratch(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(executionParams);
                }

                RecreateScriptFilesComparersProvider();
            }
        }

        public void SetDBStateByVirtualExecution(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(executionParams);
                }

                RecreateScriptFilesComparersProvider();
            }
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

        public void Deploy()
        {
            lock (_processSyncLock)
            {
                using (AutoVersionsDbEngine engine = NinjectUtils.KernelInstance.Get<DeployEngine>())
                {
                    engine.Prepare(ProjectConfigItem);
                    engine.Run(null);
                }
            }
        }

        #endregion



        #region Script


        public string CreateNewIncrementalScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                scriptFileItem = ScriptFilesComparersProvider.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                RecreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewRepeatableScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLock)
            {
                scriptFileItem = ScriptFilesComparersProvider.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);
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
                scriptFileItem = ScriptFilesComparersProvider.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                RecreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }


        #endregion





        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AutoVersionsDbAPI()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_currentArtifactExtractor != null)
                {
                    _currentArtifactExtractor.Dispose();
                    _currentArtifactExtractor = null;
                }
            }
        }

        #endregion

    }
}
