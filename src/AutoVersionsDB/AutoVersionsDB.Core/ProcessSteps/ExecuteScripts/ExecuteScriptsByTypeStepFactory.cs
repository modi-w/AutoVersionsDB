using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptsByTypeStepFactory
    {
        private readonly ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;
        public ExecuteScriptsByTypeStepFactory(ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory)
        {
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
        }
        public ExecuteScriptsByTypeStep Create(string fileTypeCode, IDBCommands dbCommands)
        {
            return new ExecuteScriptsByTypeStep(fileTypeCode, dbCommands, _executeSingleFileScriptStepFactory);
        }
    }
}
