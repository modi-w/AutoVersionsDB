using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteAllScriptsStep : AutoVersionsDbStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ExecuteScriptsByTypeStepFactory _executeScriptsByTypeStepFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public override string StepName => $"Run Scripts";



        public ExecuteAllScriptsStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    ExecuteScriptsByTypeStepFactory executeScriptsByTypeStepFactory,
                                    ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            executeScriptsByTypeStepFactory.ThrowIfNull(nameof(executeScriptsByTypeStepFactory));
            artifactExtractorFactory.ThrowIfNull(nameof(artifactExtractorFactory));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _executeScriptsByTypeStepFactory = executeScriptsByTypeStepFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }




        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processState.ThrowIfNull(nameof(processState));



            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(projectConfig))
            {

                using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                {
                    List<NotificationableActionStepBase> internalSteps = new List<NotificationableActionStepBase>();

                    ScriptFileTypeBase incrementalFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    ExecuteScriptsByTypeStep incrementalExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(incrementalFileType.FileTypeCode, dbCommands);
                    internalSteps.Add(incrementalExecuteScriptsByTypeStep);


                    string lastIncStriptFilename = GetLastIncFilename(processState);

                    string targetStateScriptFileName = null;
                    if (processState.ExecutionParams != null)
                    {
                        targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
                    }


                    if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                        || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
                    {
                        ScriptFileTypeBase repeatableFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
                        ExecuteScriptsByTypeStep repeatableFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(repeatableFileType.FileTypeCode, dbCommands);
                        internalSteps.Add(repeatableFileTypeExecuteScriptsByTypeStep);


                        if (processState.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                        {
                            ScriptFileTypeBase devDummyDataFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                            ExecuteScriptsByTypeStep devDummyDataFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(devDummyDataFileType.FileTypeCode, dbCommands);
                            internalSteps.Add(devDummyDataFileTypeExecuteScriptsByTypeStep);

                        }
                    }


                    onExecuteStepsList.Invoke(internalSteps, false);
                }
            }


        }


        private static string GetLastIncFilename(AutoVersionsDbProcessState processState)
        {
            string lastIncStriptFilename = "";


            RuntimeScriptFileBase lastIncScriptFiles = processState.ScriptFilesState.IncrementalScriptFilesComparer.AllFileSystemScriptFiles.LastOrDefault();
            if (lastIncScriptFiles != null)
            {
                lastIncStriptFilename = lastIncScriptFiles.Filename;
            }

            return lastIncStriptFilename;
        }


    }
}
