﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
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

namespace AutoVersionsDB.Core.ProcessSteps
{

    public class CreateBackupStep : AutoVersionsDbStep
    {
        public override string StepName => "Create Backup";

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly DBProcessStatusNotifyerFactory _dbProcessStatusNotifyerFactory;


        public CreateBackupStep(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                                DBProcessStatusNotifyerFactory dbProcessStatusNotifyerFactory)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _dbProcessStatusNotifyerFactory = dbProcessStatusNotifyerFactory;
        }




        public override void Execute(AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));

            string timeStampStr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);

            string targetFileName;
            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processState.ProjectConfig.DBTypeCode, processState.ProjectConfig.ConnStr, processState.ProjectConfig.DBCommandsTimeout))
            {
                targetFileName = $"bu_{ dbCommands.GetDataBaseName()}_{timeStampStr}.bak";
            }

            string targetFileFullPath = Path.Combine(processState.ProjectConfig.DBBackupBaseFolder, targetFileName);
            FileSystemPathUtils.ResloveFilePath(targetFileFullPath);

            //notificationExecutersProvider.SetStepStartManually(100, "Backup process");

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(processState.ProjectConfig.DBTypeCode, processState.ProjectConfig.ConnStrToMasterDB))
            {
                DBProcessStatusNotifyerBase dbBackupStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBBackupStatusNotifyer), dbQueryStatus) as DBBackupStatusNotifyer;

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    InternalSteps.Add(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbBackupStatusNotifyer.Start(
                (precents) =>
                {
                    //notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

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
                        using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processState.ProjectConfig.DBTypeCode, processState.ProjectConfig.ConnStr, processState.ProjectConfig.DBCommandsTimeout))
                        {
                            using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(processState.ProjectConfig.DBTypeCode, processState.ProjectConfig.ConnStrToMasterDB, processState.ProjectConfig.DBCommandsTimeout))
                            {
                                dbBackupRestoreCommands.CreateDbBackup(targetFileFullPath, dbCommands.GetDataBaseName());

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
                        processExpetion = ex;
                    }

                });

                ExecuteInternalSteps(processState, false);



                dbBackupStatusNotifyer.Stop();

                processState.DBBackupFileFullPath = targetFileFullPath;
            }

        }



    }




}
