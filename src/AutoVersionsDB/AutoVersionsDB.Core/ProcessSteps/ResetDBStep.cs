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
        public override string StepName => "Resolve Reset Database";
        public override bool HasInternalStep => false;

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;


        public ResetDBStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override int GetNumOfInternalSteps(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            return 1;
        }

        public override void Execute(ProjectConfig projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {

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
