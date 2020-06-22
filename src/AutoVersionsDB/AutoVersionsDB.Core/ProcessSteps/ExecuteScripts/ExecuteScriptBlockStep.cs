﻿using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ScriptBlockStepArgs>
    {
        public override string StepName => "Execute Script Block";

        public bool IsVirtualExecution { get; private set; }

        private IDBCommands _dbCommands;



        public ExecuteScriptBlockStep(IDBCommands dbCommands,
                                                    bool isVirtualExecution)
        {
            _dbCommands = dbCommands;
            IsVirtualExecution = isVirtualExecution;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ScriptBlockStepArgs actionStepArgs)
        {
            return 1;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ScriptBlockStepArgs scriptBlockStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));
            scriptBlockStepArgs.ThrowIfNull(nameof(scriptBlockStepArgs));

            if (!IsVirtualExecution)
            {
                _dbCommands.ExecSQLCommandStr(scriptBlockStepArgs.ScriptBlockStr);
            }
        }



    }
}