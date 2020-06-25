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
    public static class RestoreDatabaseStepFluent
    {
        public static AutoVersionsDbEngine RestoreDatabase(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                        IDBCommands dbCommands,
                                                        IDBBackupRestoreCommands dbBackupRestoreCommands,
                                                        IDBQueryStatus dbQueryStatus)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            dbQueryStatus.ThrowIfNull(nameof(dbQueryStatus));

            NotificationExecutersFactoryManager notificationExecutersFactoryManager = NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>();

            DBRestoreStatusNotifyer dbRestoreStatusNotifyer =
                DBProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus) as DBRestoreStatusNotifyer;


            RestoreDatabaseStep restoreDatabaseStep =
                new RestoreDatabaseStep(notificationExecutersFactoryManager,
                                                            dbCommands,
                                                            dbBackupRestoreCommands,
                                                            dbRestoreStatusNotifyer);


            autoVersionsDbEngine.SetRollbackStep(restoreDatabaseStep);

            return autoVersionsDbEngine;
        }
    }


    public class RestoreDatabaseStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public const string StepNameStr = "Rollback (Restore) Database";

        public override string StepName => StepNameStr;



        private readonly NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private readonly IDBCommands _dbCommands;
        private readonly IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private readonly DBRestoreStatusNotifyer _dbRestoreStatusNotifyer;

        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;


        public RestoreDatabaseStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            IDBCommands dbCommands,
                                            IDBBackupRestoreCommands dbBackupRestoreCommands,
                                            DBRestoreStatusNotifyer dbRestoreStatusNotifyer)
        {
            dbRestoreStatusNotifyer.ThrowIfNull(nameof(dbRestoreStatusNotifyer));


            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommands = dbCommands;
            _dbBackupRestoreCommands = dbBackupRestoreCommands;
            _dbRestoreStatusNotifyer = dbRestoreStatusNotifyer;

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


    }
}
