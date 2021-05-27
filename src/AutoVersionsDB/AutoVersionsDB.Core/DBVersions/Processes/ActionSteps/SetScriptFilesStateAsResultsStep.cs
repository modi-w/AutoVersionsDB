using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class SetScriptFilesStateAsResultsStep : DBVersionsStep
    {
        public const string Name = "Set ScriptFilesState As Results";
        public override string StepName => Name;


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
