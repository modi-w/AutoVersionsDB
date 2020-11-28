using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Integration;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class RecreateDBVersionsTablesStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactoryProvider;

        public override string StepName => "Recreate System Tables";

        public RecreateDBVersionsTablesStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactoryProvider = dbCommandsFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                dbCommands.Instance.RecreateDBVersionsTables();
            }

            processContext.ScriptFilesState.Reload(processContext.ProjectConfig);
        }


    }
}
