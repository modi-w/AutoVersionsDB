using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class RecreateDBVersionsTablesStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => "Recreate System Tables";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        private IDBCommands _dbCommands;


        public RecreateDBVersionsTablesStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                            ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));
            scriptFilesComparersProvider.ThrowIfNull(nameof(scriptFilesComparersProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            //_scriptFilesComparersProvider.SetProjectConfig(projectConfig);
            _scriptFilesComparersProvider.Reload(projectConfig);

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            _dbCommands.RecreateDBVersionsTables();
            
            //_scriptFilesComparersProvider.Reload();
        }


        #region IDisposable

        private bool _disposed = false;

        ~RecreateDBVersionsTablesStep() => Dispose(false);

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
