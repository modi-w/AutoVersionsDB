using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStepFactory
    {
        private ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;
        public ExecuteSingleFileScriptStepFactory(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory)
        {
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
        public ExecuteSingleFileScriptStep Create(IDBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            return new ExecuteSingleFileScriptStep(_executeScriptBlockStepFactory, dbCommands, stepName, scriptFile);
        }
    }
}
