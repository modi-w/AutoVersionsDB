using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class ResetDBStep : AutoVersionsDbStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string StepName => "Resolve Reset Database";



        public ResetDBStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
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
