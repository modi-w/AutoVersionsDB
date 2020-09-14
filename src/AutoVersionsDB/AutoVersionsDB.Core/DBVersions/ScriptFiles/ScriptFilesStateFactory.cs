using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.DbCommands.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class ScriptFilesStateFactory
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public ScriptFilesStateFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                       ScriptFilesComparerFactory scriptFilesComparerFactory,
                                        ArtifactExtractorFactory artifactExtractorFactory)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }

        public ScriptFilesState Create()
        {
            ScriptFilesState scriptFilesState = new ScriptFilesState(_dbCommandsFactoryProvider, _scriptFilesComparerFactory, _artifactExtractorFactory);

            return scriptFilesState;
        }
    }
}
