using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DB;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStepFactory
    {
        private readonly ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;
        public ExecuteSingleFileScriptStepFactory(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory)
        {
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
        public ExecuteSingleFileScriptStep Create(DBCommands dbCommands, string stepName, RuntimeScriptFile scriptFile)
        {
            return new ExecuteSingleFileScriptStep(_executeScriptBlockStepFactory, dbCommands, stepName, scriptFile);
        }
    }
}
