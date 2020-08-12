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

#pragma warning disable CA1822 // Mark members as static
        public ExecuteScriptBlockStep Craete(IDBCommands dbCommands, string scriptBlockToExecute)
#pragma warning restore CA1822 // Mark members as static
        {
            return new ExecuteScriptBlockStep(dbCommands, scriptBlockToExecute);
        }
    }
}
