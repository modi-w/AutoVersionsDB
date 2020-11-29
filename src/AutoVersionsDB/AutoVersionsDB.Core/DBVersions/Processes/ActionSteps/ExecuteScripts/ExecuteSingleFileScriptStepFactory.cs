using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStepFactory
    {
        private readonly ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;
        public ExecuteSingleFileScriptStepFactory(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory)
        {
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
        public ExecuteSingleFileScriptStep Create(DBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            return new ExecuteSingleFileScriptStep(_executeScriptBlockStepFactory, dbCommands, stepName, scriptFile);
        }
    }
}
