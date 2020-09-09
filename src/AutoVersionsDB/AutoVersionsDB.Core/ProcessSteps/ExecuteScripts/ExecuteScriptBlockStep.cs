using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStep : AutoVersionsDbStep
    {
        private readonly IDBCommands _dbCommands;
        private readonly string _scriptBlockToExecute;

        public override string StepName => "Execute Script Block";

        public ExecuteScriptBlockStep(IDBCommands dbCommands, string scriptBlockToExecute)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _dbCommands = dbCommands;
            _scriptBlockToExecute = scriptBlockToExecute;
        }




        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.IsVirtualExecution)
            {
                _dbCommands.ExecSQLCommandStr(_scriptBlockToExecute);
            }
        }



    }
}
