using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class RecreateDBVersionsTablesStep : AutoVersionsDbStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string StepName => "Recreate System Tables";

        public RecreateDBVersionsTablesStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }



        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processState.ThrowIfNull(nameof(processState));

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
            {
                dbCommands.RecreateDBVersionsTables();
            }

            processState.ScriptFilesState.Reload(projectConfig);
        }


    }
}
