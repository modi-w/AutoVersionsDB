using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class SyncDBToSpecificStateEngineFactory : NotificationableEngineFactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Set DB To Specific State";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;


        public SyncDBToSpecificStateEngineFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                                                    ScriptFilesComparerFactory scriptFilesComparerFactory,
                                                                    NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            bool isVirtualExecution = false;

            IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout);
            IDBQueryStatus dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB);

            ScriptFilesComparersProvider scriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparerFactory, dbCommands, projectConfig);

            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .ProjectConfigValidation(projectConfig)
                .CheckDeliveryEnvValidation(projectConfig)
                .SystemTableValidation(dbCommands, projectConfig.IsDevEnvironment)
                .DBStateValidation(scriptFilesComparersProvider)
                .TargetStateScriptFileValidation(scriptFilesComparersProvider)
                .CreateBackup(dbCommands, dbBackupRestoreCommands, dbQueryStatus, projectConfig.DBBackupBaseFolder)
                .ExecuteScripts(dbCommands, isVirtualExecution, scriptFilesComparersProvider)
                .FinalizeProcess(dbCommands, isVirtualExecution, this.EngineTypeName)
                .RestoreDatabase(dbCommands, dbBackupRestoreCommands, dbQueryStatus);


            AddDisposableReferenceForEngine(engine, dbCommands);
            AddDisposableReferenceForEngine(engine, dbBackupRestoreCommands);
            AddDisposableReferenceForEngine(engine, dbQueryStatus);

            return engine;
        }
    }
}
