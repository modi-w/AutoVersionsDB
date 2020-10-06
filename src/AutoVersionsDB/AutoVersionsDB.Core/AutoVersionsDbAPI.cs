using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.Core.ConfigProjects.Processes;
using AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions;

namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDBAPI
    {

        private static readonly object _processSyncLock = new object();

        private static AutoVersionsDBCLI GetNewInstanceForAutoVersionsDBCLI()
        {
            return NinjectUtils.KernelInstance.Get<AutoVersionsDBCLI>();
        }

        private static ProjectConfigsAPI GetNewInstanceForProjectConfigsAPI()
        {
            return NinjectUtils.KernelInstance.Get<ProjectConfigsAPI>();
        }

        private static DBVersionsAPI GetNewInstanceForDBVersionsAPI()
        {
            return NinjectUtils.KernelInstance.Get<DBVersionsAPI>();
        }






        public static int CLIRun(string[] args)
        {
            return GetNewInstanceForAutoVersionsDBCLI().Run(args);
        }
        public static int CLIRun(string args)
        {
            return GetNewInstanceForAutoVersionsDBCLI().Run(args);
        }



        #region Config

        public static List<DBType> GetDBTypesList()
        {
            return GetNewInstanceForProjectConfigsAPI().GetDBTypesList();
        }


        public static List<ProjectConfigItem> GetProjectsList()
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().GetProjectsList();
            }
        }

        public static ProjectConfigItem GetProjectConfigById(string id)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().GetProjectConfigById(id);
            }
        }



        public static ProcessResults SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().SaveNewProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessResults UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().UpdateProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessResults ChangeProjectId(string prevId, string newId, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().ChangeProjectId(prevId, newId, onNotificationStateChanged);
            }
        }

        public static ProcessResults RemoveProjectConfig(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForProjectConfigsAPI().RemoveProjectConfig(id, onNotificationStateChanged);
            }
        }






        #endregion


        #region Validation

        public static ProcessResults ValidateDBVersions(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().ValidateDBVersions(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults ValidateProjectConfig(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().ValidateProjectConfig(id, onNotificationStateChanged);
            }
        }


        public static ProcessResults ValdiateTargetStateAlreadyExecuted(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().ValdiateTargetStateAlreadyExecuted(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }

        #endregion


        #region Run Change Db State

        public static ProcessResults SyncDB(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().SyncDB(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults RecreateDBFromScratch(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().RecreateDBFromScratch(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBToSpecificState(string id, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().SetDBToSpecificState(id, targetStateScriptFilename, isIgnoreHistoryWarning, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBStateByVirtualExecution(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().SetDBStateByVirtualExecution(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        #endregion


        #region Deploy

        public static ProcessResults Deploy(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().Deploy(id, onNotificationStateChanged);
            }
        }

        #endregion



        #region Scripts

        public static ProcessResults GetScriptFilesState(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().GetScriptFilesState(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewIncrementalScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().CreateNewIncrementalScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewRepeatableScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().CreateNewRepeatableScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewDevDummyDataScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetNewInstanceForDBVersionsAPI().CreateNewDevDummyDataScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }


        #endregion





    }
}
