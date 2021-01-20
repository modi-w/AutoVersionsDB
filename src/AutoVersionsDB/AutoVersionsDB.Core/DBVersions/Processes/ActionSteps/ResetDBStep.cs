using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using System;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class ResetDBStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactory;

        public const string Name = "Resolve Reset Database";
        public override string StepName => Name;




        public ResetDBStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactory = dbCommandsFactory;
        }


        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.ProjectConfig.DevEnvironment)
            {
                throw new Exception(CoreTextResources.CantDropDBOnDelEnvExecption);
            }

            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
            {
                dbCommands.DropAllDBObject();
            }

        }



    }
}
