using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class RecreateDBVersionsTablesStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactory;

        public const string Name = "Recreate System Tables";
        public override string StepName => Name;


        public RecreateDBVersionsTablesStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactory = dbCommandsFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
            {
                dbCommands.RecreateDBVersionsTables();
            }

            processContext.ScriptFilesState.Reload(processContext.ProjectConfig);
        }


    }
}
