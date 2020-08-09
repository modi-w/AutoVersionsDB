using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesStateFactory
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;

        public ScriptFilesStateFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                       ScriptFilesComparerFactory scriptFilesComparerFactory)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;
        }

        public ScriptFilesState Create()
        {
            ScriptFilesState scriptFilesState =   new ScriptFilesState(_dbCommandsFactoryProvider, _scriptFilesComparerFactory);

            return scriptFilesState;
        }
    }
}
