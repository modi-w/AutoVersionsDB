using AutoVersionsDB.DB;
using AutoVersionsDB.DB.DBProcessStatusNotifyers;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{

    public class CreateBackupStep : DBVersionsStep
    {
        public override string StepName => "Create Backup";

        private readonly DBCommandsFactory _dbCommandsFactory;


        public CreateBackupStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactory = dbCommandsFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            string timeStampStr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);

            string targetFileName;
            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
            {
                targetFileName = $"bu_{ dbCommands.DataBaseName}_{timeStampStr}.bak";
            }

            string targetFileFullPath = Path.Combine(processContext.ProjectConfig.BackupFolderPath, targetFileName);
            FileSystemPathUtils.ResloveFilePath(targetFileFullPath);

            //notificationExecutersProvider.SetStepStartManually(100, "Backup process");

            using (var dbBackupStatusNotifyer = _dbCommandsFactory.CreateDBProcessStatusNotifyer(typeof(DBBackupStatusNotifyer), processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                //DBProcessStatusNotifyerBase dbBackupStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(, dbQueryStatus.Instance) as DBBackupStatusNotifyer;

                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    internalSteps.Add(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbBackupStatusNotifyer.Instance.Start(
                (precents) =>
                {
                    //notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

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
                        using (var dbCommands = _dbCommandsFactory.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
                        {
                            using (var dbBackupRestoreCommands = _dbCommandsFactory.CreateDBBackupRestoreCommands(processContext.ProjectConfig.DBConnectionInfo))
                            {
                                dbBackupRestoreCommands.CreateDBBackup(targetFileFullPath, dbCommands.DataBaseName);

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
                        processExpetion = ex;
                    }

                });

                ExecuteInternalSteps(internalSteps, false);



                dbBackupStatusNotifyer.Instance.Stop();

                processContext.DBBackupFileFullPath = targetFileFullPath;
            }

        }



    }




}
