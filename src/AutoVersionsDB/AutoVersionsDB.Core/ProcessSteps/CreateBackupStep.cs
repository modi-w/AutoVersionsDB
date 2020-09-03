using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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




        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            string timeStampStr = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);

            string targetFileName;
            IDBCommands dbCommandsExternal = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout);
            try
            {
                targetFileName = $"bu_{dbCommandsExternal.GetDataBaseName()}_{timeStampStr}.bak";
            }
            finally
            {
                _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbCommandsExternal);
            }

            string targetFileFullPath = Path.Combine(processContext.ProjectConfig.DBBackupBaseFolder, targetFileName);
            FileSystemPathUtils.ResloveFilePath(targetFileFullPath);

            //notificationExecutersProvider.SetStepStartManually(100, "Backup process");

            var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStrToMasterDB);
           
            try
            {
                DBProcessStatusNotifyerBase dbBackupStatusNotifyer = _dbProcessStatusNotifyerFactory.Create(typeof(DBBackupStatusNotifyer), dbQueryStatus) as DBBackupStatusNotifyer;

                for (int internalStepNumber = 1; internalStepNumber <= 100; internalStepNumber++)
                {
                    ExternalProcessStatusStep externalProcessStatusStep = new ExternalProcessStatusStep(internalStepNumber);
                    AddInternalStep(externalProcessStatusStep);
                }

                Exception processExpetion = null;


                dbBackupStatusNotifyer.Start(
                (precents) =>
                {
                    //notificationExecutersProvider.ForceStepProgress(Convert.ToInt32(precents));

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
                        dbBackupRestoreCommands.CreateDbBackup(targetFileFullPath, dbCommands.GetDataBaseName());

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
                        processExpetion = ex;
                    }
                    finally
                    {
                        _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbCommands);
                        _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbBackupRestoreCommands);
                    }

                });

                ExecuteInternalSteps(false);

                dbBackupStatusNotifyer.Stop();

                processContext.DBBackupFileFullPath = targetFileFullPath;
            }
            finally
            {
                _dbCommandsFactoryProvider.ReleaseService(processContext.ProjectConfig.DBTypeCode, dbQueryStatus);
            }

        }



    }




}
