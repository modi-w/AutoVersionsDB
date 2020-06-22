using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class SystemTableExsitValidationEngineFactory : NotificationableEngineFactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "System Table Exsit Validation";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public SystemTableExsitValidationEngineFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                                                        NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }


        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .SystemTableValidation(dbCommands, projectConfig.IsDevEnvironment);


            AddDisposableReferenceForEngine(engine, dbCommands);


            return engine;
        }


    }
}
