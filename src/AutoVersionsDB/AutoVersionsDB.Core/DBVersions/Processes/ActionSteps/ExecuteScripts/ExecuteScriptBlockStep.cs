using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStep : DBVersionsStep
    {
        private readonly DBCommands _dbCommands;
        private readonly string _scriptBlockToExecute;

        public override string StepName => "Execute Script Block";

        public ExecuteScriptBlockStep(DBCommands dbCommands, string scriptBlockToExecute)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _dbCommands = dbCommands;
            _scriptBlockToExecute = scriptBlockToExecute;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.IsVirtualExecution)
            {
                _dbCommands.ExecSQLCommandStr(_scriptBlockToExecute);
            }
        }



    }
}
