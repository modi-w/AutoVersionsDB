using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;

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

            DBVersionsProcessParams dbVersionsProcessParams = processContext.ProcessParams as DBVersionsProcessParams;

            var scriptFilesComparer = processContext.ScriptFilesState.ScriptFilesComparers[_scriptFileType.FileTypeCode];

            RuntimeScriptFileBase newScriptFile = scriptFilesComparer.CreateNextNewScriptFile(dbVersionsProcessParams.NewScriptName);

            processContext.Results = newScriptFile.FileFullPath;
        }



    }
}
