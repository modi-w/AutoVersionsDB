using AutoVersionsDB.DB;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteScriptsByTypeStepFactory
    {
        private readonly ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;
        public ExecuteScriptsByTypeStepFactory(ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory)
        {
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
        }
        public ExecuteScriptsByTypeStep Create(string fileTypeCode, DBCommands dbCommands)
        {
            return new ExecuteScriptsByTypeStep(fileTypeCode, dbCommands, _executeSingleFileScriptStepFactory);
        }
    }
}
