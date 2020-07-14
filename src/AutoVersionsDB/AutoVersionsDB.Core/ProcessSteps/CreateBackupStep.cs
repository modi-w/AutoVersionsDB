using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
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

    public class CreateBackupStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => "Create Backup";

        private readonly NotificationExecutersFactoryManager _notificationExecutersFactoryManager;
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;



        private IDBCommands _dbCommands;
        private IDBQueryStatus _dbQueryStatus;
        private IDBBackupRestoreCommands _dbBackupRestoreCommands;
        private DBBackupStatusNotifyer _dbBackupStatusNotifyer;

        private string _dbBackupBaseFolderPath;


        private NotificationWrapperExecuter _tempNotificationWrapperExecuter;

        public CreateBackupStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
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

            _dbBackupBaseFolderPath = projectConfig.DBBackupBaseFolder;

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);
            _dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout);

            _dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB);
            _dbBackupStatusNotifyer = DBProcessStatusNotifyerFactory.Create(typeof(DBBackupStatusNotifyer), _dbQueryStatus) as DBBackupStatusNotifyer;

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
            if (_tempNotificationWrapperExecuter.CurrentNotificationStateItem != null)
            {
                _tempNotificationWrapperExecuter.CurrentNotificationStateItem.StepsProgressByValue(Convert.ToInt32(precent));
                _tempNotificationWrapperExecuter.CallHandleNotificationStateChanged();
            }
        }



        #region IDisposable

        private bool _disposed = false;

        ~CreateBackupStep() => Dispose(false);

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

                if (_dbBackupStatusNotifyer != null)
                {
                    _dbBackupStatusNotifyer.OnDBProcessStatus -= DBBackupStatusNotifyer_OnDBProcessStatus;
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
