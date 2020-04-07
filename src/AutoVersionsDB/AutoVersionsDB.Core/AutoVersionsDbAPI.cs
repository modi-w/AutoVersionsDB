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
        public static readonly AutoVersionsDbAPI Instance =
            new AutoVersionsDbAPI(
                NinjectUtils.KernelInstance.Get<ConfigProjectsManager>(),
                NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<DBStateValidationEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<SyncDBEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<DeployEngine_Factory>(),
                NinjectUtils.KernelInstance.Get<ArtifactExtractor_Factory>(),
                NinjectUtils.KernelInstance.Get<DBCommands_FactoryProvider>(),
                NinjectUtils.KernelInstance.Get<ScriptFilesComparer_Factory>(),
                NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>()
            );

        private object _processSyncLockObj = new object();


        private ProjectConfigValidationEngine_Factory _projectConfigValidationEngineFactory;
        private SystemTableExsitValidationEngine_Factory _systemTableExsitValidationEngineFactory;
        private DBStateValidationEngine_Factory _dbStateValidationEngineFactory;
        private TargetStateScriptFileValidationEngine_Factory _notificationableEngine_TargetStateScriptFileValidation_Factory;
        private ArtifactFileValidationEngine_Factory _artifactFileValidationEngineFactory;
        private SyncDBEngine_Factory _syncDBEngineFactory;
        private RecreateDBFromScratchEngine_Factory _recreateDBFromScratchEngineFactory;
        private CreateVirtualExecutionsEngine_Factory _createVirtualExecutionsEngineFactory;
        private SyncDBToSpecificStateEngine_Factory _setDBToSpecificStateFactory;
        private DeployEngine_Factory _deployEngineStateFactory;

        private ArtifactExtractor_Factory _artifactExtractor_Factory;

        private ScriptFilesComparer_Factory _scriptFilesComparer_Factory;


        public DBCommands_FactoryProvider DBCommandsFactoryProvider { get; private set; }
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

        public string InstructionsMessage_StepName
        {
            get
            {
                return NotificationExecutersFactoryManager.InstructionsMessage_StepName;
            }
        }





        public AutoVersionsDbAPI(ConfigProjectsManager configProjectsManager,
                                ProjectConfigValidationEngine_Factory projectConfigValidationEngineFactory,
                                SystemTableExsitValidationEngine_Factory systemTableExsitValidationEngineFactory,
                                DBStateValidationEngine_Factory dbStateValidationEngineFactory,
                                TargetStateScriptFileValidationEngine_Factory notificationableEngine_TargetStateScriptFileValidation_Factory,
                                ArtifactFileValidationEngine_Factory artifactFileValidationEngineFactory,
                                SyncDBEngine_Factory syncDBEngineFactory,
                                RecreateDBFromScratchEngine_Factory recreateDBFromScratchEngineFactory,
                                CreateVirtualExecutionsEngine_Factory createVirtualExecutionsEngineFactory,
                                SyncDBToSpecificStateEngine_Factory setDBToSpecificStateFactory,
                                DeployEngine_Factory deployEngineStateFactory,
                                ArtifactExtractor_Factory artifactExtractor_Factory,
                                DBCommands_FactoryProvider dbCommandsFactoryProvider,
                                ScriptFilesComparer_Factory scriptFilesComparer_Factory,
                                NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            ConfigProjectsManager = configProjectsManager;

            _projectConfigValidationEngineFactory = projectConfigValidationEngineFactory;
            _systemTableExsitValidationEngineFactory = systemTableExsitValidationEngineFactory;
            _dbStateValidationEngineFactory = dbStateValidationEngineFactory;
            _notificationableEngine_TargetStateScriptFileValidation_Factory = notificationableEngine_TargetStateScriptFileValidation_Factory;
            _artifactFileValidationEngineFactory = artifactFileValidationEngineFactory;
            _syncDBEngineFactory = syncDBEngineFactory;
            _recreateDBFromScratchEngineFactory = recreateDBFromScratchEngineFactory;
            _createVirtualExecutionsEngineFactory = createVirtualExecutionsEngineFactory;
            _setDBToSpecificStateFactory = setDBToSpecificStateFactory;
            _deployEngineStateFactory = deployEngineStateFactory;

            _artifactExtractor_Factory = artifactExtractor_Factory;
            DBCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparer_Factory = scriptFilesComparer_Factory;

            NotificationExecutersFactoryManager = notificationExecutersFactoryManager;

        }


        #region Config

        public void SetProjectConfigItem(ProjectConfigItem projectConfigItem)
        {
            lock (_processSyncLockObj)
            {
                ProjectConfigItem = projectConfigItem;

                Refresh();
            }
        }

        public void SaveProjectConfig()
        {
            lock (_processSyncLockObj)
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
                _currentArtifactExtractor = _artifactExtractor_Factory.Create(ProjectConfigItem);

                recreateScriptFilesComparersProvider();
            }
        }


        private void recreateScriptFilesComparersProvider()
        {
            using (IDBCommands dbCommands = DBCommandsFactoryProvider.CreateDBCommand(ProjectConfigItem.DBTypeCode, ProjectConfigItem.ConnStr, ProjectConfigItem.DBCommandsTimeout))
            {
                ScriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparer_Factory, dbCommands, ProjectConfigItem);
            }
        }


        #endregion


        #region Validation

        public void ValidateAll()
        {
            lock (_processSyncLockObj)
            {
                validateArtifactFile();

                if (!HasError)
                {
                    ValidateProjectConfig();

                    if (!HasError)
                    {
                        validateSystemTableExist();

                        if (!HasError)
                        {
                            validateDBState();
                        }
                    }
                }
            }
        }

        public void ValidateProjectConfig()
        {
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _projectConfigValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _projectConfigValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        private void validateArtifactFile()
        {
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _artifactFileValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _projectConfigValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        private void validateSystemTableExist()
        {
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _systemTableExsitValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _systemTableExsitValidationEngineFactory.ReleaseEngine(engine);
            }
        }


        private void validateDBState()
        {
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _dbStateValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _dbStateValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        public bool ValdiateTargetStateAlreadyExecuted(string targetStateScriptFilename)
        {
            lock (_processSyncLockObj)
            {
                ExecutionParams executionParams = createTargetStepExectionParams(targetStateScriptFilename);

                AutoVersionsDbEngine engine = _notificationableEngine_TargetStateScriptFileValidation_Factory.Create(ProjectConfigItem);
                engine.Run(executionParams);
                _notificationableEngine_TargetStateScriptFileValidation_Factory.ReleaseEngine(engine);
            }

            return !NotificationExecutersFactoryManager.HasError;
        }

        #endregion


        #region Run Change Db State

        public void SyncDB()
        {
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _syncDBEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _syncDBEngineFactory.ReleaseEngine(engine);
                recreateScriptFilesComparersProvider();
            }
        }

        public void SetDBToSpecificState(string targetStateScriptFilename, bool isIgnoreHistoryWarning)
        {
            lock (_processSyncLockObj)
            {
                if (isIgnoreHistoryWarning)
                {
                    RecreateDBFromScratch(targetStateScriptFilename);
                }
                else
                {
                    ExecutionParams executionParams = createTargetStepExectionParams(targetStateScriptFilename);

                    AutoVersionsDbEngine engine = _setDBToSpecificStateFactory.Create(ProjectConfigItem);
                    engine.Run(executionParams);
                    _setDBToSpecificStateFactory.ReleaseEngine(engine);
                }
                recreateScriptFilesComparersProvider();
            }
        }

        public void RecreateDBFromScratch(string targetStateScriptFilename)
        {
            lock (_processSyncLockObj)
            {
                ExecutionParams executionParams = createTargetStepExectionParams(targetStateScriptFilename);

                AutoVersionsDbEngine engine = _recreateDBFromScratchEngineFactory.Create(ProjectConfigItem);
                engine.Run(executionParams);
                _recreateDBFromScratchEngineFactory.ReleaseEngine(engine);
                recreateScriptFilesComparersProvider();
            }
        }

        public void SetDBStateByVirtualExecution(string targetStateScriptFilename)
        {
            lock (_processSyncLockObj)
            {
                ExecutionParams executionParams = createTargetStepExectionParams(targetStateScriptFilename);

                AutoVersionsDbEngine engine = _createVirtualExecutionsEngineFactory.Create(ProjectConfigItem);
                engine.Run(executionParams);
                _createVirtualExecutionsEngineFactory.ReleaseEngine(engine);
                recreateScriptFilesComparersProvider();
            }
        }



        private ExecutionParams createTargetStepExectionParams(string targetStateScriptFilename)
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
            lock (_processSyncLockObj)
            {
                AutoVersionsDbEngine engine = _deployEngineStateFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _syncDBEngineFactory.ReleaseEngine(engine);
            }
        }

        #endregion



        #region Script


        public string CreateNewIncrementalScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLockObj)
            {
                scriptFileItem = ScriptFilesComparersProvider.IncrementalScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                recreateScriptFilesComparersProvider();
            }

            return scriptFileItem.FileFullPath;
        }

        public string CreateNewRepeatableScriptFile(string scriptName)
        {
            RuntimeScriptFileBase scriptFileItem;

            lock (_processSyncLockObj)
            {
                scriptFileItem = ScriptFilesComparersProvider.RepeatableScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                recreateScriptFilesComparersProvider();
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

            lock (_processSyncLockObj)
            {
                scriptFileItem = ScriptFilesComparersProvider.DevDummyDataScriptFilesComparer.CreateNextNewScriptFile(scriptName);
                recreateScriptFilesComparersProvider();
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
