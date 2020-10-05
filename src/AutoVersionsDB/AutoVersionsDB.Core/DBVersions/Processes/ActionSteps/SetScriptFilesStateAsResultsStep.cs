using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class SetScriptFilesStateAsResultsStep : DBVersionsStep
    {
        public override string StepName => "Set ScriptFilesState As Results";


        public SetScriptFilesStateAsResultsStep()
        {
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            processContext.Results = processContext.ScriptFilesState;

        }



    }
}
