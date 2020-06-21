﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class DeployEngine_Factory : NotificationableEngine_FactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Deploy";

        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;
        private ScriptFilesComparer_Factory _scriptFilesComparer_Factory;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public DeployEngine_Factory(DBCommands_FactoryProvider dbCommands_FactoryProvider,
                                                        ScriptFilesComparer_Factory scriptFilesComparer_Factory,
                                                        NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
            _scriptFilesComparer_Factory = scriptFilesComparer_Factory;
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }


        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            ScriptFilesComparersProvider scriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparer_Factory, dbCommands, projectConfig);


            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .ProjectConfigValidation(projectConfig)
                .CheckDeliveryEnvValidation(projectConfig)
                .SystemTableValidation(dbCommands, projectConfig.IsDevEnvironment)
                .DBStateValidation(scriptFilesComparersProvider)
                .BuildDeployArtifactFile(scriptFilesComparersProvider, dbCommands.GetDataBaseName(), projectConfig);


            
            AddDisposableReferenceForEngine(engine, dbCommands);
            

            return engine;
        }


    }
}
