using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStepFactory
    {
        public ExecuteScriptBlockStepFactory()
        {

        }

        public ExecuteScriptBlockStep Craete(IDBCommands dbCommands, string scriptBlockToExecute)
        {
            return new ExecuteScriptBlockStep(dbCommands, scriptBlockToExecute);
        }
    }
}
