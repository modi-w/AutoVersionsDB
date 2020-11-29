using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStepFactory
    {
        public ExecuteScriptBlockStepFactory()
        {

        }

        public virtual ExecuteScriptBlockStep Craete(DBCommands dbCommands, string scriptBlockToExecute)
        {
            return new ExecuteScriptBlockStep(dbCommands, scriptBlockToExecute);
        }
    }
}
