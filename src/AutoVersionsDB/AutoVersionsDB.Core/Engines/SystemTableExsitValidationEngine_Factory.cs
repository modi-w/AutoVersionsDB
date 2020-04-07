using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.ValidationsStep;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class SystemTableExsitValidationEngine_Factory : NotificationableEngine_FactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public SystemTableExsitValidationEngine_Factory(DBCommands_FactoryProvider dbCommands_FactoryProvider,
                                                                        NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }


        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .SystemTableValidation(dbCommands, projectConfig.IsDevEnvironment);


            AddDisposableReferenceForEngine(engine, dbCommands);


            return engine;
        }


    }
}
