using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class RestoreDatabaseStep : AutoVersionsDbStep, IDisposable
    {
        public const string StepNameStr = "Rollback (Restore) Database";

        public override string StepName => StepNameStr;


        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        private IDBCommands _dbCommands;
        private IDBQueryStatus _dbQueryStatus;
        private IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private DBRestoreStatusNotifyer _dbRestoreStatusNotifyer;


        public RestoreDatabaseStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            _dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout);

            _dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB);
            _dbRestoreStatusNotifyer = DBProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), _dbQueryStatus) as DBRestoreStatusNotifyer;

        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }



        public override void Execute(NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));


            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(100))
            {
                notificationWrapperExecuter.SetStepStartManually("Restore process", "");

                _dbRestoreStatusNotifyer.Start((precents) =>
                {

                    if (notificationWrapperExecuter.CurrentNotificationStateItem != null)
                    {
                        notificationWrapperExecuter.ForceStepProgress(Convert.ToInt32(precents));
                    }
                });

                try
                {
                    _dbBackupRestoreCommands.RestoreDbFromBackup(processState.DBBackupFileFullPath, _dbCommands.GetDataBaseName());
                }
                catch (Exception ex)
                {
                    string errorInstructionsMessage = "The process fail when trying to 'Restore the Database', try to change the Timeout parameter and restore the database manually.";

                    throw new NotificationEngineException(this.StepName, ex.Message, errorInstructionsMessage, ex);
                }

                _dbRestoreStatusNotifyer.Stop();
            }
        }

     


        #region IDisposable

        private bool _disposed = false;

        ~RestoreDatabaseStep() => Dispose(false);

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

                if (_dbBackupRestoreCommands != null)
                {
                    _dbBackupRestoreCommands.Dispose();
                }


                if (_dbQueryStatus != null)
                {
                    _dbQueryStatus.Dispose();
                }


            }

            _disposed = true;
        }

        #endregion


    }
}
