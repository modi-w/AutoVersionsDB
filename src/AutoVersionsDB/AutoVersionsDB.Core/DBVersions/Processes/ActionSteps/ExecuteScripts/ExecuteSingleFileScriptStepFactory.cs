﻿using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStepFactory
    {
        private readonly ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;
        public ExecuteSingleFileScriptStepFactory(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory)
        {
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
        public ExecuteSingleFileScriptStep Create(IDBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            return new ExecuteSingleFileScriptStep(_executeScriptBlockStepFactory, dbCommands, stepName, scriptFile);
        }
    }
}