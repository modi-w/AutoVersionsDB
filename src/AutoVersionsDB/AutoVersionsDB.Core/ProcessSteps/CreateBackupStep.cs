using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System;
using System.Globalization;
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
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            dbCommands.ThrowIfNull(nameof(dbCommands));
            dbBackupRestoreCommands.ThrowIfNull(nameof(dbBackupRestoreCommands));
            dbQueryStatus.ThrowIfNull(nameof(dbQueryStatus));
            dbBackupBaseFolderPath.ThrowIfNull(nameof(dbBackupBaseFolderPath));

            NotificationExecutersFactoryManager notificationExecutersFactoryManager = NinjectUtils.KernelInstance.Get<NotificationExecutersFactoryManager>();

            DBBackupStatusNotifyer dbBackupStatusNotifyer =
                DBProcessStatusNotifyerFactory.Create(typeof(DBBackupStatusNotifyer), dbQueryStatus) as DBBackupStatusNotifyer;


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

        private readonly NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private readonly IDBCommands _dbCommands;
        private readonly IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private readonly DBBackupStatusNotifyer _dbBackupStatusNotifyer;

        private readonly string _dbBackupBaseFolderPath;


        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;

        public CreateBackupStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            IDBCommands dbCommands,
                                            IDBBackupRestoreCommands dbBackupRestoreCommands,
                                            DBBackupStatusNotifyer dbBackupStatusNotifyer,
                                            string dbBackupBaseFolderPath)
        {
            notificationExecutersFactoryManager.ThrowIfNull(nameof(notificationExecutersFactoryManager));
            dbCommands.ThrowIfNull(nameof(dbCommands));
            dbBackupRestoreCommands.ThrowIfNull(nameof(dbBackupRestoreCommands));
            dbBackupStatusNotifyer.ThrowIfNull(nameof(dbBackupStatusNotifyer));
            dbBackupBaseFolderPath.ThrowIfNull(nameof(dbBackupBaseFolderPath));


            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _dbCommands = dbCommands;
            _dbBackupRestoreCommands = dbBackupRestoreCommands;
            _dbBackupStatusNotifyer = dbBackupStatusNotifyer;
            _dbBackupBaseFolderPath = dbBackupBaseFolderPath;

            _dbBackupStatusNotifyer.OnDBProcessStatus += DBBackupStatusNotifyer_OnDBProcessStatus;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }



        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));

            string timeStampStr = DateTime.Now.ToString("{0:yyyy-MM-dd-HH-mm-ss}", CultureInfo.InvariantCulture);

            string targetFileName = $"bu_{ _dbCommands.GetDataBaseName()}_{timeStampStr}.bak";
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

        private void DBBackupStatusNotifyer_OnDBProcessStatus(double precent)
        {
            if (_tempNotificationWrapperExecuter.CurrentNotificationStateItem!= null)
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepsProgressByValue(Convert.ToInt32(precent));
                _tempNotificationWrapperExecuter.CallHandleNotificationStateChanged();
            }
        }


    }
}
