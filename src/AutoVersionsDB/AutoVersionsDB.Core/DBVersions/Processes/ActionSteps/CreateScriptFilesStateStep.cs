﻿using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class CreateScriptFilesStateStep : DBVersionsStep
    {
        public const string Name = "Create Script Files State";
        public override string StepName => Name;


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
