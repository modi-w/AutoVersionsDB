using AutoVersionsDB.Core.Engines;
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
                                                        IDBQueryStatus dbQueryStatus,
                                                        string dbBackupBaseFolderPath)
        {
            NotificationExecutersFactoryManager notificationExecutersFactoryManager = NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>();

            DBProcessStatusNotifyer_Factory dbProcessStatusNotifyer_Factory = NinjectUtils.KernelInstance.Get<DBProcessStatusNotifyer_Factory>();

            DBRestoreStatusNotifyer dbRestoreStatusNotifyer =
                dbProcessStatusNotifyer_Factory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus) as DBRestoreStatusNotifyer;


            RestoreDatabaseStep restoreDatabaseStep =
                new RestoreDatabaseStep(notificationExecutersFactoryManager,
                                                            dbCommands,
                                                            dbBackupRestoreCommands,
                                                            dbRestoreStatusNotifyer,
                                                            dbBackupBaseFolderPath);


            autoVersionsDbEngine.SetRollbackStep(restoreDatabaseStep);

            return autoVersionsDbEngine;
        }
    }


    public class RestoreDatabaseStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Rollback (Restore) Database";



        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private IDBCommands _dbCommands;
        private IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private DBRestoreStatusNotifyer _dbRestoreStatusNotifyer;

        private string _dbBackupBaseFolderPath;

        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;


        public RestoreDatabaseStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            IDBCommands dbCommands,
                                            IDBBackupRestoreCommands dbBackupRestoreCommands,
                                            DBRestoreStatusNotifyer dbRestoreStatusNotifyer,
                                            string dbBackupBaseFolderPath)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommands = dbCommands;
            _dbBackupRestoreCommands = dbBackupRestoreCommands;
            _dbRestoreStatusNotifyer = dbRestoreStatusNotifyer;
            _dbBackupBaseFolderPath = dbBackupBaseFolderPath;

            _dbRestoreStatusNotifyer.OnDBProcessStatus += _dbRestoreStatusNotifyer_OnDBProcessStatus;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }



        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
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

        private void _dbRestoreStatusNotifyer_OnDBProcessStatus(double precent)
        {
            if (_tempNotificationWrapperExecuter.CurrentNotificationStateItem != null)
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepsProgressByValue(Convert.ToInt32(precent));
                _tempNotificationWrapperExecuter.CallHandleNotificationStateChanged();
            }
        }


    }
}
