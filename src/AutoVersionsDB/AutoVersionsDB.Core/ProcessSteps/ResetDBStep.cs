using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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


        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.ProjectConfig.IsDevEnvironment)
            {
                throw new Exception("Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).");
            }

            IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout);
            try
            {
                dbCommands.DropAllDB();
            }
            finally
            {
                _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbCommands);
            }

        }



    }
}
