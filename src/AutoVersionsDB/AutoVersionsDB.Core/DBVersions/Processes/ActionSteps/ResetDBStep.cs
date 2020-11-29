using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.Helpers;
using System;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class ResetDBStep : DBVersionsStep
    {
        private readonly DBCommandsFactory dbCommandsFactoryProvider;

        public override string StepName => "Resolve Reset Database";



        public ResetDBStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            dbCommandsFactoryProvider = dbCommandsFactory;
        }


        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.ProjectConfig.DevEnvironment)
            {
                throw new Exception("Can't Drop DB when running on none dev enviroment (you can change the parameter in project setting).");
            }

            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
            {
                dbCommands.DropAllDBObject();
            }

        }



    }
}
