using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
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



        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processState.ThrowIfNull(nameof(processState));


            //notificationExecutersProvider.SetStepStartManually(100, "Restore process");

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB))
            {
                DBProcessStatusNotifyerBase dbRestoreStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBRestoreStatusNotifyer), dbQueryStatus) as DBRestoreStatusNotifyer;

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    InternalSteps.Add(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbRestoreStatusNotifyer.Start((precents) =>
                {
                    // notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

                    foreach (ExternalProcessStatusStep step in InternalSteps)
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
                        using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout))
                        {
                            using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, projectConfig.DBCommandsTimeout))
                            {
                                dbBackupRestoreCommands.RestoreDbFromBackup(processState.DBBackupFileFullPath, dbCommands.GetDataBaseName());

                                foreach (ExternalProcessStatusStep step in InternalSteps)
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

                        processExpetion = new NotificationEngineException(this.StepName, ex.Message, errorInstructionsMessage, ex);
                    }
                });

                onExecuteStepsList.Invoke(InternalSteps, true);

                dbRestoreStatusNotifyer.Stop();


            }

        }

    }
}
