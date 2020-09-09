using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class CreateScriptFilesStateStep : AutoVersionsDbStep
    {
        public override string StepName => "Create Script Files State";
        private readonly ScriptFilesStateFactory _scriptFilesStateFactory;


        public CreateScriptFilesStateStep(ScriptFilesStateFactory scriptFilesStateFactory)
        {
            scriptFilesStateFactory.ThrowIfNull(nameof(scriptFilesStateFactory));

            _scriptFilesStateFactory = scriptFilesStateFactory;
        }



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            processContext.ScriptFilesState = _scriptFilesStateFactory.Create();
            processContext.ScriptFilesState.Reload(processContext.ProjectConfig);
        }



    }
}
