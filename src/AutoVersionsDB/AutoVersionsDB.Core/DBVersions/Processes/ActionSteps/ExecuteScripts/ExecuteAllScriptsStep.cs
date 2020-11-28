using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteAllScriptsStep : DBVersionsStep
    {
        private readonly DBCommandsFactory _dbCommandsFactoryProvider;
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

            _dbCommandsFactoryProvider = dbCommandsFactory;
            _executeScriptsByTypeStepFactory = executeScriptsByTypeStepFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(processContext.ProjectConfig))
            {
                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
                {

                    ScriptFileTypeBase incrementalFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    ExecuteScriptsByTypeStep incrementalExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(incrementalFileType.FileTypeCode, dbCommands.Instance);
                    internalSteps.Add(incrementalExecuteScriptsByTypeStep);


                    string lastIncStriptFilename = GetLastIncFilename(processContext);

                    string targetStateScriptFileName = null;
                    if (processContext.ProcessParams != null)
                    {
                        targetStateScriptFileName = (processContext.ProcessParams as DBVersionsProcessParams).TargetStateScriptFileName;
                    }


                    if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                        || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
                    {
                        ScriptFileTypeBase repeatableFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
                        ExecuteScriptsByTypeStep repeatableFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(repeatableFileType.FileTypeCode, dbCommands.Instance);
                        internalSteps.Add(repeatableFileTypeExecuteScriptsByTypeStep);


                        if (processContext.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                        {
                            ScriptFileTypeBase devDummyDataFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                            ExecuteScriptsByTypeStep devDummyDataFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(devDummyDataFileType.FileTypeCode, dbCommands.Instance);
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
