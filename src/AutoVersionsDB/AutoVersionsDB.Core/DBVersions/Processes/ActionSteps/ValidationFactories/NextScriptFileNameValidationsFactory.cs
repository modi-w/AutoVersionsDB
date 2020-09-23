using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class NextScriptFileNameValidationsFactory<TScriptFileType> : ValidationsFactory
      where TScriptFileType : ScriptFileTypeBase
    {
        private readonly TScriptFileType _scriptFileType;

        public override string ValidationName => "NextRuntimeScriptFileName";

        public NextScriptFileNameValidationsFactory(TScriptFileType scriptFileType)
        {
            _scriptFileType = scriptFileType;
        }

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;
            DBVersionsProcessParams dbVersionsProcessParams = dbVersionsProcessContext.ProcessParams as DBVersionsProcessParams;

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            NextScriptFileNameValidator nextScriptFileNameValidator = new NextScriptFileNameValidator(dbVersionsProcessContext.ScriptFilesState.ScriptFilesComparers[_scriptFileType.FileTypeCode], dbVersionsProcessParams.NewScriptName);
            validationsGroup.Add(nextScriptFileNameValidator);


            return validationsGroup;
        }
    }
}
