﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{

    public class SyncDBEngine_Factory : NotificationableEngine_FactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Sync DB";

        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;
        private ScriptFilesComparer_Factory _scriptFilesComparer_Factory;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public SyncDBEngine_Factory(DBCommands_FactoryProvider dbCommands_FactoryProvider,
                                                        ScriptFilesComparer_Factory scriptFilesComparer_Factory,
                                                        NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
            _scriptFilesComparer_Factory = scriptFilesComparer_Factory;
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {

            bool isVirtualExecution = false;

            IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommands_FactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout);
            IDBQueryStatus dbQueryStatus = _dbCommands_FactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB);

            ScriptFilesComparersProvider scriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparer_Factory, dbCommands, projectConfig);


            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .ProjectConfigValidation(projectConfig)
                .ArtifactFileValidation(projectConfig)
                .SystemTableValidation(dbCommands, projectConfig.IsDevEnvironment)
                .DBStateValidation(scriptFilesComparersProvider)
                .CreateBackup(dbCommands, dbBackupRestoreCommands, dbQueryStatus, projectConfig.DBBackupBaseFolder)
                .ExecuteScripts(dbCommands, isVirtualExecution, scriptFilesComparersProvider)
                .FinalizeProcess(dbCommands, isVirtualExecution, this.EngineTypeName)
                .RestoreDatabase(dbCommands, dbBackupRestoreCommands, dbQueryStatus, projectConfig.DBBackupBaseFolder);



            AddDisposableReferenceForEngine(engine, dbCommands);
            AddDisposableReferenceForEngine(engine, dbBackupRestoreCommands);
            AddDisposableReferenceForEngine(engine, dbQueryStatus);


            return engine;
        }




    }
}
