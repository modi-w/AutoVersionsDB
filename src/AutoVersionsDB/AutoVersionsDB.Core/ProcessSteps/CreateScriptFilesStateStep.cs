﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
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



        public override void Execute(AutoVersionsDbEngineContext processState)
        {
            processState.ThrowIfNull(nameof(processState));

            processState.ScriptFilesState = _scriptFilesStateFactory.Create();
            processState.ScriptFilesState.Reload(processState.ProjectConfig);
        }



    }
}
