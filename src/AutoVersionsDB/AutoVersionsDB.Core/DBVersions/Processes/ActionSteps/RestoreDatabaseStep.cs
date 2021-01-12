using AutoVersionsDB.DB;
using AutoVersionsDB.DB.DBProcessStatusNotifyers;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class RestoreDatabaseStep : DBVersionsStep
    {
        public const string StepNameStr = "Rollback (Restore) Database";

        public override string StepName => StepNameStr;


        private readonly DBCommandsFactory dbCommandsFactoryProvider;


        public RestoreDatabaseStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            dbCommandsFactoryProvider = dbCommandsFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            //notificationExecutersProvider.SetStepStartManually(100, "Restore process");

            using (var dbRestoreStatusNotifyer = dbCommandsFactoryProvider.CreateDBProcessStatusNotifyer(typeof(DBRestoreStatusNotifyer), processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    internalSteps.Add(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbRestoreStatusNotifyer.Instance.Start((precents) =>
                {
                    // notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

                    foreach (ExternalProcessStatusStep step in internalSteps)
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
                        using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
                        {
                            using (var dbBackupRestoreCommands = dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(processContext.ProjectConfig.DBConnectionInfo))
                            {
                                dbBackupRestoreCommands.RestoreDBFromBackup(processContext.DBBackupFileFullPath, dbCommands.DataBaseName);

                                foreach (ExternalProcessStatusStep step in internalSteps)
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

                        processExpetion = new NotificationProcessException(StepName, ex.Message, errorInstructionsMessage, NotificationErrorType.Error, ex);
                    }
                });

                ExecuteInternalSteps(internalSteps, true);

                dbRestoreStatusNotifyer.Instance.Stop();


            }

        }

    }
}
