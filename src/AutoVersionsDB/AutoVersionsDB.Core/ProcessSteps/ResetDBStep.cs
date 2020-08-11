using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class ResetDBStep : AutoVersionsDbStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string StepName => "Resolve Reset Database";
        public override bool HasInternalStep => false;



        public ResetDBStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            return 1;
        }

        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            notificationExecutersProvider.ThrowIfNull(nameof(notificationExecutersProvider));
            processState.ThrowIfNull(nameof(processState));

            if (!projectConfig.IsDevEnvironment)
            {
                throw new Exception("Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).");
            }

            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
            {
                dbCommands.DropAllDB();
            }

        }



    }
}
