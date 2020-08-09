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

    public class RestoreDatabaseStep : AutoVersionsDbStep
    {
        public const string StepNameStr = "Rollback (Restore) Database";
        public override bool HasInternalStep => false;

        public override string StepName => StepNameStr;


        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly DBProcessStatusNotifyerFactory _dbProcessStatusNotifyerFactory;


        public RestoreDatabaseStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                    DBProcessStatusNotifyerFactory dbProcessStatusNotifyerFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _dbProcessStatusNotifyerFactory = dbProcessStatusNotifyerFactory;
        }


   
        public override int GetNumOfInternalSteps(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            return 1;
        }



        public override void Execute(ProjectConfig projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));


            using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(100))
            {
                notificationWrapperExecuter.SetStepStartManually("Restore process");

                using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB))
                {
                    DBProcessStatusNotifyerBase dbRestoreStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus) as DBRestoreStatusNotifyer;
               
                    dbRestoreStatusNotifyer.Start((precents) =>
                    {

                        if (notificationWrapperExecuter.CurrentNotificationStateItem != null)
                        {
                            notificationWrapperExecuter.ForceStepProgress(Convert.ToInt32(precents));
                        }
                    });

                    try
                    {
                        using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                        {
                            using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout))
                            {
                                dbBackupRestoreCommands.RestoreDbFromBackup(processState.DBBackupFileFullPath, dbCommands.GetDataBaseName());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorInstructionsMessage = "The process fail when trying to 'Restore the Database', try to change the Timeout parameter and restore the database manually.";

                        throw new NotificationEngineException(this.StepName, ex.Message, errorInstructionsMessage, ex);
                    }

                    dbRestoreStatusNotifyer.Stop();


                }

            }
        }

    }
}
