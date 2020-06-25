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
                NinjectUtils.KernelInstance.Get<ProjectConfigValidationEngineFactory>(),
                NinjectUtils.KernelInstance.Get<SystemTableExsitValidationEngineFactory>(),
                NinjectUtils.KernelInstance.Get<DBStateValidationEngineFactory>(),
                NinjectUtils.KernelInstance.Get<TargetStateScriptFileValidationEngineFactory>(),
                NinjectUtils.KernelInstance.Get<ArtifactFileValidationEngineFactory>(),
                NinjectUtils.KernelInstance.Get<SyncDBEngineFactory>(),
                NinjectUtils.KernelInstance.Get<RecreateDBFromScratchEngineFactory>(),
                NinjectUtils.KernelInstance.Get<CreateVirtualExecutionsEngineFactory>(),
                NinjectUtils.KernelInstance.Get<SyncDBToSpecificStateEngineFactory>(),
                NinjectUtils.KernelInstance.Get<DeployEngineFactory>(),
                NinjectUtils.KernelInstance.Get<DBCommandsFactoryProvider>(),
                NinjectUtils.KernelInstance.Get<ScriptFilesComparerFactory>(),
                NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>()
            );

        private readonly object _processSyncLock = new object();


        private readonly ProjectConfigValidationEngineFactory _projectConfigValidationEngineFactory;
        private readonly SystemTableExsitValidationEngineFactory _systemTableExsitValidationEngineFactory;
        private readonly DBStateValidationEngineFactory _dbStateValidationEngineFactory;
        private readonly TargetStateScriptFileValidationEngineFactory _notificationableEngine_TargetStateScriptFileValidation_Factory;
        private readonly ArtifactFileValidationEngineFactory _artifactFileValidationEngineFactory;
        private readonly SyncDBEngineFactory _syncDBEngineFactory;
        private readonly RecreateDBFromScratchEngineFactory _recreateDBFromScratchEngineFactory;
        private readonly CreateVirtualExecutionsEngineFactory _createVirtualExecutionsEngineFactory;
        private readonly SyncDBToSpecificStateEngineFactory _setDBToSpecificStateFactory;
        private readonly DeployEngineFactory _deployEngineStateFactory;

        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;


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
                                ProjectConfigValidationEngineFactory projectConfigValidationEngineFactory,
                                SystemTableExsitValidationEngineFactory systemTableExsitValidationEngineFactory,
                                DBStateValidationEngineFactory dbStateValidationEngineFactory,
                                TargetStateScriptFileValidationEngineFactory notificationableEngineTargetStateScriptFileValidationFactory,
                                ArtifactFileValidationEngineFactory artifactFileValidationEngineFactory,
                                SyncDBEngineFactory syncDBEngineFactory,
                                RecreateDBFromScratchEngineFactory recreateDBFromScratchEngineFactory,
                                CreateVirtualExecutionsEngineFactory createVirtualExecutionsEngineFactory,
                                SyncDBToSpecificStateEngineFactory setDBToSpecificStateFactory,
                                DeployEngineFactory deployEngineStateFactory,
                                DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                ScriptFilesComparerFactory scriptFilesComparerFactory,
                                NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            ConfigProjectsManager = configProjectsManager;

            _projectConfigValidationEngineFactory = projectConfigValidationEngineFactory;
            _systemTableExsitValidationEngineFactory = systemTableExsitValidationEngineFactory;
            _dbStateValidationEngineFactory = dbStateValidationEngineFactory;
            _notificationableEngine_TargetStateScriptFileValidation_Factory = notificationableEngineTargetStateScriptFileValidationFactory;
            _artifactFileValidationEngineFactory = artifactFileValidationEngineFactory;
            _syncDBEngineFactory = syncDBEngineFactory;
            _recreateDBFromScratchEngineFactory = recreateDBFromScratchEngineFactory;
            _createVirtualExecutionsEngineFactory = createVirtualExecutionsEngineFactory;
            _setDBToSpecificStateFactory = setDBToSpecificStateFactory;
            _deployEngineStateFactory = deployEngineStateFactory;

            DBCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;

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
            using (IDBCommands dbCommands = DBCommandsFactoryProvider.CreateDBCommand(ProjectConfigItem.DBTypeCode, ProjectConfigItem.ConnStr, ProjectConfigItem.DBCommandsTimeout))
            {
                ScriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparerFactory, dbCommands, ProjectConfigItem);
            }
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
                AutoVersionsDbEngine engine = _projectConfigValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _projectConfigValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        private void ValidateArtifactFile()
        {
            lock (_processSyncLock)
            {
                AutoVersionsDbEngine engine = _artifactFileValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _projectConfigValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        private void ValidateSystemTableExist()
        {
            lock (_processSyncLock)
            {
                AutoVersionsDbEngine engine = _systemTableExsitValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _systemTableExsitValidationEngineFactory.ReleaseEngine(engine);
            }
        }


        private void ValidateDBState()
        {
            lock (_processSyncLock)
            {
                AutoVersionsDbEngine engine = _dbStateValidationEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _dbStateValidationEngineFactory.ReleaseEngine(engine);
            }
        }

        public bool ValdiateTargetStateAlreadyExecuted(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

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
            lock (_processSyncLock)
            {
                AutoVersionsDbEngine engine = _syncDBEngineFactory.Create(ProjectConfigItem);
                engine.Run(null);
                _syncDBEngineFactory.ReleaseEngine(engine);
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

                    AutoVersionsDbEngine engine = _setDBToSpecificStateFactory.Create(ProjectConfigItem);
                    engine.Run(executionParams);
                    _setDBToSpecificStateFactory.ReleaseEngine(engine);
                }
                RecreateScriptFilesComparersProvider();
            }
        }

        public void RecreateDBFromScratch(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                AutoVersionsDbEngine engine = _recreateDBFromScratchEngineFactory.Create(ProjectConfigItem);
                engine.Run(executionParams);
                _recreateDBFromScratchEngineFactory.ReleaseEngine(engine);
                RecreateScriptFilesComparersProvider();
            }
        }

        public void SetDBStateByVirtualExecution(string targetStateScriptFilename)
        {
            lock (_processSyncLock)
            {
                ExecutionParams executionParams = CreateTargetStepExectionParams(targetStateScriptFilename);

                AutoVersionsDbEngine engine = _createVirtualExecutionsEngineFactory.Create(ProjectConfigItem);
                engine.Run(executionParams);
                _createVirtualExecutionsEngineFactory.ReleaseEngine(engine);
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
