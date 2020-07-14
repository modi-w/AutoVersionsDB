using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Globalization;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptBlockStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ProjectConfigItem, ScriptBlockStepArgs>
    {
        public override string StepName => "Execute Script Block";

        private IDBCommands _dbCommands;



        public ExecuteScriptBlockStep()
        {
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
        }

        public void SetDBCommands(IDBCommands dbCommands)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _dbCommands = dbCommands;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ScriptBlockStepArgs actionStepArgs)
        {
            return 1;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ScriptBlockStepArgs scriptBlockStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));
            scriptBlockStepArgs.ThrowIfNull(nameof(scriptBlockStepArgs));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                _dbCommands.ExecSQLCommandStr(scriptBlockStepArgs.ScriptBlockStr);
            }
        }



    }
}
