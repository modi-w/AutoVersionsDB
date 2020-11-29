using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class CreateScriptFilesStateStep : DBVersionsStep
    {
        public override string StepName => "Create Script Files State";
        private readonly ScriptFilesStateFactory _scriptFilesStateFactory;


        public CreateScriptFilesStateStep(ScriptFilesStateFactory scriptFilesStateFactory)
        {
            scriptFilesStateFactory.ThrowIfNull(nameof(scriptFilesStateFactory));

            _scriptFilesStateFactory = scriptFilesStateFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            processContext.ScriptFilesState = _scriptFilesStateFactory.Create();
            processContext.ScriptFilesState.Reload(processContext.ProjectConfig);
        }



    }
}
