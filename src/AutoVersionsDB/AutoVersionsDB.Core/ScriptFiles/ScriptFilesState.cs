using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;

namespace AutoVersionsDB.Core.ScriptFiles
{
    public class ScriptFilesState
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;

        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesState(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                            ScriptFilesComparerFactory scriptFilesComparerFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparerFactory.ThrowIfNull(nameof(scriptFilesComparerFactory));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;

        }



        public void Reload(ProjectConfig projectConfig)
        {
            using(IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
            {
                IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(dbCommands, projectConfig.IncrementalScriptsFolderPath);
                RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(dbCommands, projectConfig.RepeatableScriptsFolderPath);

                if (projectConfig.IsDevEnvironment)
                {
                    DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(dbCommands, projectConfig.DevDummyDataScriptsFolderPath);
                }
                else
                {
                    DevDummyDataScriptFilesComparer = null;
                }
            }

        }



        //#region IDisposable

        //private bool _disposed = false;

        //~ScriptFilesComparersProvider() => Dispose(false);

        //// Public implementation of Dispose pattern callable by consumers.
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //// Protected implementation of Dispose pattern.
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (_disposed)
        //    {
        //        return;
        //    }

        //    if (disposing)
        //    {
        //        if (_dbCommands != null)
        //        {
        //            _dbCommands.Dispose();
        //        }

        //    }

        //    _disposed = true;
        //}

        //#endregion

    }
}
