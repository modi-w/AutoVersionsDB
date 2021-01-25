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
    public class ExecuteAllScriptsStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly ExecuteScriptsByTypeStepFactory _executeScriptsByTypeStepFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public const string Name = "Run Scripts";
        public override string StepName => Name;



        public ExecuteAllScriptsStep(DBCommandsFactory dbCommandsFactory,
                                    ExecuteScriptsByTypeStepFactory executeScriptsByTypeStepFactory,
                                    ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));
            executeScriptsByTypeStepFactory.ThrowIfNull(nameof(executeScriptsByTypeStepFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactory = dbCommandsFactory;
            _executeScriptsByTypeStepFactory = executeScriptsByTypeStepFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(processContext.ProjectConfig))
            {
                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
                {

                    ScriptFileTypeBase incrementalFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    ExecuteScriptsByTypeStep incrementalExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(incrementalFileType.FileTypeCode, dbCommands);
                    internalSteps.Add(incrementalExecuteScriptsByTypeStep);


                    string lastIncStriptFilename = GetLastIncFilename(processContext);

                    TargetScripts targetScripts = null;
                    if (processContext.ProcessArgs != null)
                    {
                        targetScripts = (processContext.ProcessArgs as DBVersionsProcessArgs).TargetScripts;
                    }

                    //Comment: We run the repeatable files and ddd files, onlyif we have all the schema on the DB. mean, only if we executed all incremiental files.
                    if (targetScripts.IncScriptFileName.Trim().ToUpperInvariant() == lastIncStriptFilename.Trim().ToUpperInvariant()
                        || targetScripts.IncScriptFileName.Trim().ToUpperInvariant() == RuntimeScriptFile.TargetLastScriptFileName.Trim().ToUpperInvariant())
                    {
                        ScriptFileTypeBase repeatableFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
                        ExecuteScriptsByTypeStep repeatableFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(repeatableFileType.FileTypeCode, dbCommands);
                        internalSteps.Add(repeatableFileTypeExecuteScriptsByTypeStep);


                        if (processContext.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                        {
                            ScriptFileTypeBase devDummyDataFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                            ExecuteScriptsByTypeStep devDummyDataFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(devDummyDataFileType.FileTypeCode, dbCommands);
                            internalSteps.Add(devDummyDataFileTypeExecuteScriptsByTypeStep);
                        }
                    }


                    ExecuteInternalSteps(internalSteps, false);
                }
            }


        }


        private static string GetLastIncFilename(DBVersionsProcessContext processContext)
        {
            string lastIncStriptFilename = "";


            RuntimeScriptFile lastIncScriptFiles = processContext.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }


    }
}
