using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
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




        public override void Execute( AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            using (ArtifactExtractor _currentArtifactExtractor = _artifactExtractorFactory.Create(processContext.ProjectConfig))
            {

                using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout))
                {

                    ScriptFileTypeBase incrementalFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    ExecuteScriptsByTypeStep incrementalExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(incrementalFileType.FileTypeCode, dbCommands);
                    AddInternalStep(incrementalExecuteScriptsByTypeStep);


                    string lastIncStriptFilename = GetLastIncFilename(processContext);

                    string targetStateScriptFileName = null;
                    if (processContext.ProcessParams != null)
                    {
                        targetStateScriptFileName = (processContext.ProcessParams as AutoVersionsDbProcessParams).TargetStateScriptFileName;
                    }


                    if (string.IsNullOrWhiteSpace(targetStateScriptFileName)
                        || lastIncStriptFilename.Trim().ToUpperInvariant() == targetStateScriptFileName.Trim().ToUpperInvariant())
                    {
                        ScriptFileTypeBase repeatableFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
                        ExecuteScriptsByTypeStep repeatableFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(repeatableFileType.FileTypeCode, dbCommands);
                        AddInternalStep(repeatableFileTypeExecuteScriptsByTypeStep);


                        if (processContext.ScriptFilesState.DevDummyDataScriptFilesComparer != null)
                        {
                            ScriptFileTypeBase devDummyDataFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                            ExecuteScriptsByTypeStep devDummyDataFileTypeExecuteScriptsByTypeStep = _executeScriptsByTypeStepFactory.Create(devDummyDataFileType.FileTypeCode, dbCommands);
                            AddInternalStep(devDummyDataFileTypeExecuteScriptsByTypeStep);

                        }
                    }

                    ExecuteInternalSteps( false);
                }
            }


        }


        private static string GetLastIncFilename(AutoVersionsDbProcessContext processContext)
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
