using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Text;

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
