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
        private readonly DBCommandsFactory dbCommandsFactoryProvider;
        private readonly ExecuteScriptsByTypeStepFactory _executeScriptsByTypeStepFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public override string StepName => $"Run Scripts";



        public ExecuteAllScriptsStep(DBCommandsFactory dbCommandsFactory,
                                    ExecuteScriptsByTypeStepFactory executeScriptsByTypeStepFactory,
                                    ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));
            executeScriptsByTypeStepFactory.ThrowIfNull(nameof(executeScriptsByTypeStepFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            dbCommandsFactoryProvider = dbCommandsFactory;
            _executeScriptsByTypeStepFactory = executeScriptsByTypeStepFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(processContext.ProjectConfig))
            {
                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
                {

                    ScriptFileTypeBase incrementalFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    ExecuteScriptsByTypeStep incrementalExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(incrementalFileType.FileTypeCode, dbCommands);
                    internalSteps.Add(incrementalExecuteScriptsByTypeStep);


                    string lastIncStriptFilename = GetLastIncFilename(processContext);

                    string targetStateScriptFileName = null;
                    if (processContext.ProcessArgs != null)
                    {
                        targetStateScriptFileName = (processContext.ProcessArgs as DBVersionsProcessArgs).TargetStateScriptFileName;
                    }


                    if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                        || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
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


            RuntimeScriptFileBase lastIncScriptFiles = processContext.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }


    }
}
