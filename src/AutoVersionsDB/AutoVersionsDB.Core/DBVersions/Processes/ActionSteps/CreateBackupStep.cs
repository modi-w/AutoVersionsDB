using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
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

        private readonly DBCommandsFactory _dbCommandsFactoryProvider;
        private readonly DBProcessStatusNotifyerFactory _dbProcessStatusNotifyerFactory;


        public CreateBackupStep(DBCommandsFactory dbCommandsFactory,
                                DBProcessStatusNotifyerFactory dbProcessStatusNotifyerFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            _dbCommandsFactoryProvider = dbCommandsFactory;
            _dbProcessStatusNotifyerFactory = dbProcessStatusNotifyerFactory;
        }




        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            string timeStampStr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);

            string targetFileName;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                targetFileName = $"bu_{ dbCommands.Instance.GetDataBaseName()}_{timeStampStr}.bak";
            }

            string targetFileFullPath = Path.Combine(processContext.ProjectConfig.BackupFolderPath, targetFileName);
            FileSystemPathUtils.ResloveFilePath(targetFileFullPath);

            //notificationExecutersProvider.SetStepStartManually(100, "Backup process");

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                DBProcessStatusNotifyerBase dbBackupStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBBackupStatusNotifyer), dbQueryStatus.Instance) as DBBackupStatusNotifyer;

                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    internalSteps.Add(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbBackupStatusNotifyer.Start(
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
                        using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
                        {
                            using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
                            {
                                dbBackupRestoreCommands.Instance.CreateDBBackup(targetFileFullPath, dbCommands.Instance.GetDataBaseName());

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



                dbBackupStatusNotifyer.Stop();

                processContext.DBBackupFileFullPath = targetFileFullPath;
            }

        }



    }




}
