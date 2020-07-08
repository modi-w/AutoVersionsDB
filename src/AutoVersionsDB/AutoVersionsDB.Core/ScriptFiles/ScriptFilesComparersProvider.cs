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
    public class ScriptFilesComparersProvider : IDisposable
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly ScriptFilesComparerFactory _scriptFilesComparerFactory;

        private ProjectConfigItem _projectConfig;

        private IDBCommands _dbCommands;

        public ScriptFilesComparerBase IncrementalScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase RepeatableScriptFilesComparer { get; private set; }
        public ScriptFilesComparerBase DevDummyDataScriptFilesComparer { get; private set; }

        public ScriptFilesComparersProvider(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                            ScriptFilesComparerFactory scriptFilesComparerFactory,
                                            ProjectConfigItem projectConfig)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparerFactory.ThrowIfNull(nameof(scriptFilesComparerFactory));
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparerFactory = scriptFilesComparerFactory;

            _projectConfig = projectConfig;

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(_projectConfig.DBTypeCode, _projectConfig.ConnStr, _projectConfig.DBCommandsTimeout);
        }



        public void Reload()
        {
            IncrementalScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<IncrementalScriptFileType>(_dbCommands, _projectConfig.IncrementalScriptsFolderPath);
            RepeatableScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<RepeatableScriptFileType>(_dbCommands, _projectConfig.RepeatableScriptsFolderPath);

            if (_projectConfig.IsDevEnvironment)
            {
                DevDummyDataScriptFilesComparer = _scriptFilesComparerFactory.CreateScriptFilesComparer<DevDummyDataScriptFileType>(_dbCommands, _projectConfig.DevDummyDataScriptsFolderPath);
            }
            else
            {
                DevDummyDataScriptFilesComparer = null;
            }
        }



        #region IDisposable

        private bool _disposed = false;

        ~ScriptFilesComparersProvider() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dbCommands != null)
                {
                    _dbCommands.Dispose();
                }

            }

            _disposed = true;
        }

        #endregion

    }
}
