using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout))
            {
                dbCommands.RecreateDBVersionsTables();
            }

            processContext.ScriptFilesState.Reload(processContext.ProjectConfig);
        }


    }
}
