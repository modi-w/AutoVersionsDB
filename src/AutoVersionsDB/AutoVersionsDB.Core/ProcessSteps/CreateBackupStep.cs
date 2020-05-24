using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System;
using System.IO;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public static class CreateBackupStepFluent
    {
        public static AutoVersionsDbEngine CreateBackup(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                        IDBCommands dbCommands,
                                                        IDBBackupRestoreCommands dbBackupRestoreCommands,
                                                        IDBQueryStatus dbQueryStatus,
                                                        string dbBackupBaseFolderPath)
        {
            NotificationExecutersFactoryManager notificationExecutersFactoryManager = NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>();

            DBProcessStatusNotifyer_Factory dbProcessStatusNotifyer_Factory = NinjectUtils.KernelInstance.Get<DBProcessStatusNotifyer_Factory>();

            DBBackupStatusNotifyer dbBackupStatusNotifyer =
                dbProcessStatusNotifyer_Factory.Create(typeof(DBBackupStatusNotifyer), dbQueryStatus) as DBBackupStatusNotifyer;


            CreateBackupStep createBackupStep =
                new CreateBackupStep(notificationExecutersFactoryManager,
                                                dbCommands,
                                                dbBackupRestoreCommands,
                                                dbBackupStatusNotifyer,
                                                dbBackupBaseFolderPath);


            autoVersionsDbEngine.AppendProcessStep(createBackupStep);

            return autoVersionsDbEngine;
        }
    }


    public class CreateBackupStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Create Backup";

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private IDBCommands _dbCommands;
        private IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private DBBackupStatusNotifyer _dbBackupStatusNotifyer;

        private string _dbBackupBaseFolderPath;


        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;

        public CreateBackupStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            IDBCommands dbCommands,
                                            IDBBackupRestoreCommands dbBackupRestoreCommands,
                                            DBBackupStatusNotifyer dbBackupStatusNotifyer,
                                            string dbBackupBaseFolderPath)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommands = dbCommands;
            _dbBackupRestoreCommands = dbBackupRestoreCommands;
            _dbBackupStatusNotifyer = dbBackupStatusNotifyer;
            _dbBackupBaseFolderPath = dbBackupBaseFolderPath;

            _dbBackupStatusNotifyer.OnDBProcessStatus += _dbBackupStatusNotifyer_OnDBProcessStatus;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }



        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            string timeStampStr = string.Format("{0:yyyy-MM-dd-HH-mm-ss}", DateTime.Now);

            string targetFileName = string.Format("bu_{0}_{1}.bak", _dbCommands.GetDataBaseName(), timeStampStr);
            string targetFileFullPath = Path.Combine(_dbBackupBaseFolderPath, targetFileName);
            FileSystemPathUtils.ResloveFilePath(targetFileFullPath);

            using (_tempNotificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(100))
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepStart("Backup process", "");

                _dbBackupStatusNotifyer.Start();

                _dbBackupRestoreCommands.CreateDbBackup(targetFileFullPath, _dbCommands.GetDataBaseName());

                _dbBackupStatusNotifyer.Stop();
            }


            processState.DBBackupFileFullPath = targetFileFullPath;
        }

        private void _dbBackupStatusNotifyer_OnDBProcessStatus(double precent)
        {
            if (_tempNotificationWrapperExecuter.CurrentNotificationStateItem!= null)
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepsProgressByValue(Convert.ToInt32(precent));
                _tempNotificationWrapperExecuter.CallHandleNotificationStateChanged();
            }
        }


    }
}
