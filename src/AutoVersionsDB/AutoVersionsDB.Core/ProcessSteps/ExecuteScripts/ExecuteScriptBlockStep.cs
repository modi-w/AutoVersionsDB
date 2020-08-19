using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStep : AutoVersionsDbStep
    {
        private readonly IDBCommands _dbCommands;
        private readonly string _scriptBlockToExecute;

        public override string StepName => "Execute Script Block";

        public ExecuteScriptBlockStep(IDBCommands dbCommands, string scriptBlockToExecute)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _dbCommands = dbCommands;
            _scriptBlockToExecute = scriptBlockToExecute;
        }




        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            processState.ThrowIfNull(nameof(processState));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                _dbCommands.ExecSQLCommandStr(_scriptBlockToExecute);
            }
        }



    }
}
