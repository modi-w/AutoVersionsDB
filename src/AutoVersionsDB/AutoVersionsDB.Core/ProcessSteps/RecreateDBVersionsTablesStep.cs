using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public static class RecreateDBVersionsTablesStepFluent
    {
        public static AutoVersionsDbEngine RecreateDBVersionsTables(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                                    IDBCommands dbCommands,
                                                                    ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep =
                new RecreateDBVersionsTablesStep(dbCommands,
                                                            scriptFilesComparersProvider);


            autoVersionsDbEngine.AppendProcessStep(recreateDBVersionsTablesStep);

            return autoVersionsDbEngine;
        }
    }


    public class RecreateDBVersionsTablesStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Recreate System Tables";

        private IDBCommands _dbCommands;
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        public RecreateDBVersionsTablesStep(IDBCommands dbCommands,
                                                        ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            _dbCommands = dbCommands;
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            _dbCommands.RecreateDBVersionsTables();
            _scriptFilesComparersProvider.Reload();
        }

    }
}
