using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

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
