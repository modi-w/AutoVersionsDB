using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class ResetDBStep : DBVersionsStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string StepName => "Resolve Reset Database";



        public ResetDBStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.ProjectConfig.IsDevEnvironment)
            {
                throw new Exception("Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).");
            }

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout).AsDisposable())
            {
                dbCommands.Instance.DropAllDB();
            }

        }



    }
}
