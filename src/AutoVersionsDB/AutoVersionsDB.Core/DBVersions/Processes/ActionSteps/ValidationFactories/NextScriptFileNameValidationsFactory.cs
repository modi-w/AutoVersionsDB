using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class NextScriptFileNameValidationsFactory<TScriptFileType> : ValidationsFactory
      where TScriptFileType : ScriptFileTypeBase
    {
        private readonly TScriptFileType _scriptFileType;

        public const string Name = "Next Runtime Script File Name";
        public override string ValidationName => Name;


        public NextScriptFileNameValidationsFactory(TScriptFileType scriptFileType)
        {
            _scriptFileType = scriptFileType;
        }

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessArgs dbVersionsProcessArgs = dbVersionsProcessContext.ProcessArgs as DBVersionsProcessArgs;

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            NextScriptFileNameValidator nextScriptFileNameValidator = new NextScriptFileNameValidator(dbVersionsProcessContext.ScriptFilesState.ScriptFilesComparers[_scriptFileType.FileTypeCode], dbVersionsProcessArgs.NewScriptName);
            validationsGroup.Add(nextScriptFileNameValidator);


            return validationsGroup;
        }
    }
}
