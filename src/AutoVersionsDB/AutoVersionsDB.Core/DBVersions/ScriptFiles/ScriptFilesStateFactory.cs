﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.DbCommands.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.ScriptFiles
{
    public class ScriptFilesStateFactory
    {
        private readonly DBCommandsFactory dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;
        private readonly ArtifactExtractorFactory _artifactExtractorFactory;

        public ScriptFilesStateFactory(DBCommandsFactory dbCommandsFactory,
                                       ScriptFilesComparerFactory scriptFilesComparerFactory,
                                        ArtifactExtractorFactory artifactExtractorFactory)
        {
            dbCommandsFactoryProvider = dbCommandsFactory;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
            _artifactExtractorFactory = artifactExtractorFactory;
        }

        public ScriptFilesState Create()
        {
            ScriptFilesState scriptFilesState = new ScriptFilesState(dbCommandsFactoryProvider, _scriptFilesComparerFactory, _artifactExtractorFactory);

            return scriptFilesState;
        }
    }
}
