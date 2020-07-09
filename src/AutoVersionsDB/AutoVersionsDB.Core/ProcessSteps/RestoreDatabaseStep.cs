﻿using AutoVersionsDB.Core.ConfigProjects;
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



        private readonly NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        private IDBCommands _dbCommands;
        private IDBQueryStatus _dbQueryStatus;
        private IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private DBRestoreStatusNotifyer _dbRestoreStatusNotifyer;

        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;


        public RestoreDatabaseStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            notificationExecutersFactoryManager.ThrowIfNull(nameof(notificationExecutersFactoryManager));
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            _dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout);

            _dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB);
            _dbRestoreStatusNotifyer = DBProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), _dbQueryStatus) as DBRestoreStatusNotifyer;

            _dbRestoreStatusNotifyer.OnDBProcessStatus += DBRestoreStatusNotifyer_OnDBProcessStatus;
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }



        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));


            using (_tempNotificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(100))
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepStart("Restore process", "");

                _dbRestoreStatusNotifyer.Start();

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

        private void DBRestoreStatusNotifyer_OnDBProcessStatus(double precent)
        {
            if (_tempNotificationWrapperExecuter.CurrentNotificationStateItem != null)
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepsProgressByValue(Convert.ToInt32(precent));
                _tempNotificationWrapperExecuter.CallHandleNotificationStateChanged();
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

                if (_dbRestoreStatusNotifyer != null)
                {
                    _dbRestoreStatusNotifyer.OnDBProcessStatus -= DBRestoreStatusNotifyer_OnDBProcessStatus;
                }

                if (_dbQueryStatus != null)
                {
                    _dbQueryStatus.Dispose();
                }

                if (_tempNotificationWrapperExecuter != null)
                {
                    _tempNotificationWrapperExecuter.Dispose();
                }

            }

            _disposed = true;
        }

        #endregion


    }
}
