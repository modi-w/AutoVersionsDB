using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class RestoreDatabaseStep : AutoVersionsDbStep
    {
        public const string StepNameStr = "Rollback (Restore) Database";

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



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            //notificationExecutersProvider.SetStepStartManually(100, "Restore process");

            var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStrToMasterDB);

            try
            {
                DBProcessStatusNotifyerBase dbRestoreStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus) as DBRestoreStatusNotifyer;

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    AddInternalStep(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbRestoreStatusNotifyer.Start((precents) =>
                {
                    // notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

                    foreach (ExternalProcessStatusStep step in ReadOnlyInternalSteps)
                    {
                        if (!step.IsCompleted)
                        {
                            step.SetProcessState((int)Math.Floor(precents), processExpetion);
                        }
                    }
                });

                Task.Run(() =>
                {
                    IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout);
                    IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStrToMasterDB, processContext.ProjectConfig.DBCommandsTimeout);
                    try
                    {
                        dbBackupRestoreCommands.RestoreDbFromBackup(processContext.DBBackupFileFullPath, dbCommands.GetDataBaseName());

                        foreach (ExternalProcessStatusStep step in ReadOnlyInternalSteps)
                        {
                            if (!step.IsCompleted)
                            {
                                step.SetProcessState(100, processExpetion);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorInstructionsMessage = "The process fail when trying to 'Restore the Database', try to change the Timeout parameter and restore the database manually.";

                        processExpetion = new NotificationProcessException(this.StepName, ex.Message, errorInstructionsMessage, ex);
                    }
                    finally
                    {
                        _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbCommands);
                        _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbBackupRestoreCommands);
                    }
                });

                ExecuteInternalSteps(true);

                dbRestoreStatusNotifyer.Stop();
            }
            finally
            {
                _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbQueryStatus);
            }

        }

    }
}
