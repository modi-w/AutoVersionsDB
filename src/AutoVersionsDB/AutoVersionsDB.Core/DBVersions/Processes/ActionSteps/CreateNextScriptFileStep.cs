using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class CreateNextScriptFileStep<TScriptFileType> : DBVersionsStep
      where TScriptFileType : ScriptFileTypeBase
    {
        private readonly TScriptFileType _scriptFileType;

        public override string StepName => "Create Next Script File";



        public CreateNextScriptFileStep(TScriptFileType scriptFileType)
        {
            _scriptFileType = scriptFileType;
        }


        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            DBVersionsProcessArgs dbVersionsProcessArgs = processContext.ProcessArgs as DBVersionsProcessArgs;

            var scriptFilesComparer = processContext.ScriptFilesState.ScriptFilesComparers[_scriptFileType.FileTypeCode];

            RuntimeScriptFileBase newScriptFile = scriptFilesComparer.CreateNextNewScriptFile(dbVersionsProcessArgs.NewScriptName);

            processContext.Results = newScriptFile.FileFullPath;
        }



    }
}
