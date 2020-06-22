using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class DBStateValidationEngineFactory : NotificationableEngineFactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "DB State Validation";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;


        public DBStateValidationEngineFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider,
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

            IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            ScriptFilesComparersProvider scriptFilesComparersProvider = new ScriptFilesComparersProvider(_scriptFilesComparerFactory, dbCommands, projectConfig);

            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .DBStateValidation(scriptFilesComparersProvider);


            AddDisposableReferenceForEngine(engine, dbCommands);


            return engine;
        }


    }
}
