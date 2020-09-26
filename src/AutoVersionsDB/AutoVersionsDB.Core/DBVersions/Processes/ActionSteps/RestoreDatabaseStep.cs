using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class RestoreDatabaseStep : DBVersionsStep
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



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            //notificationExecutersProvider.SetStepStartManually(100, "Restore process");

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                DBProcessStatusNotifyerBase dbRestoreStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus.Instance) as DBRestoreStatusNotifyer;

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

                    try
                    {
                        using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
                        {
                            using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
                            {
                                dbBackupRestoreCommands.Instance.RestoreDbFromBackup(processContext.DBBackupFileFullPath, dbCommands.Instance.GetDataBaseName());

                                foreach (ExternalProcessStatusStep step in ReadOnlyInternalSteps)
                                {
                                    if (!step.IsCompleted)
                                    {
                                        step.SetProcessState(100, processExpetion);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorInstructionsMessage = "The process fail when trying to 'Restore the Database', try to change the Timeout parameter and restore the database manually.";

                        processExpetion = new NotificationProcessException(StepName, ex.Message, errorInstructionsMessage, ex);
                    }
                });

                ExecuteInternalSteps(true);

                dbRestoreStatusNotifyer.Stop();


            }

        }

    }
}
