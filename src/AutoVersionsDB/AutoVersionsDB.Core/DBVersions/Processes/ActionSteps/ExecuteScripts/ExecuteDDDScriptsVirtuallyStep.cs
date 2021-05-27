using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteDDDScriptsVirtuallyStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly ExecuteScriptsByTypeStepFactory _executeScriptsByTypeStepFactory;

        public const string Name = "Execute DDD Scripts Virtually";
        public override string StepName => Name;



        public ExecuteDDDScriptsVirtuallyStep(DBCommandsFactory dbCommandsFactory,
                                    ExecuteScriptsByTypeStepFactory executeScriptsByTypeStepFactory,
                                    ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));
            executeScriptsByTypeStepFactory.ThrowIfNull(nameof(executeScriptsByTypeStepFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactory = dbCommandsFactory;
            _executeScriptsByTypeStepFactory = executeScriptsByTypeStepFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            if (processContext.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
            {
                using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
                {
                    List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                    (processContext.ProcessDefinition as DBVersionsProcessDefinition).IsVirtualExecution = true;

                    ScriptFileTypeBase devDummyDataFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                    ExecuteScriptsByTypeStep devDummyDataFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(devDummyDataFileType.FileTypeCode, dbCommands);
                    internalSteps.Add(devDummyDataFileTypeExecuteScriptsByTypeStep);

                    ExecuteInternalSteps(internalSteps, false);
                }
            }

        }




    }
}
