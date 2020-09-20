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
    public static class AutoVersionsDbAPI
    {

        private static readonly object _processSyncLock = new object();

        private static readonly AutoVersionsDBCLI _autoVersionsDBCLI = NinjectUtils.KernelInstance.Get<AutoVersionsDBCLI>();

        private static ProjectConfigsAPI GetInstanceNewProjectConfigsAPI()
        {
            return NinjectUtils.KernelInstance.Get<ProjectConfigsAPI>();
        }

        private static DBVersionsAPI GetInstanceNewDBVersionsAPI()
        {
            return NinjectUtils.KernelInstance.Get<DBVersionsAPI>();
        }



        public static List<DBType> GetDbTypesList()
        {
            DBCommandsFactoryProvider dbCommandsFactoryProvider = NinjectUtils.KernelInstance.Get<DBCommandsFactoryProvider>();

            return dbCommandsFactoryProvider.GetDbTypesList();
        }



        public static int CLIRun(string[] args)
        {
            return _autoVersionsDBCLI.Run(args);
        }
        public static int CLIRun(string args)
        {
            return _autoVersionsDBCLI.Run(args);
        }



        #region Config

        public static List<ProjectConfigItem> GetProjectsList()
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().GetProjectsList();
            }
        }

        public static ProjectConfigItem GetProjectConfigByProjectCode(string projectCode)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().GetProjectConfigByProjectCode(projectCode);
            }
        }



        public static ProcessTrace SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().SaveNewProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessTrace UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().UpdateProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessTrace ChangeProjectCode(string prevProjectCode, string newProjectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().ChangeProjectCode(prevProjectCode, newProjectCode, onNotificationStateChanged);
            }
        }

        public static ProcessTrace RemoveProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewProjectConfigsAPI().RemoveProjectConfig(projectCode, onNotificationStateChanged);
            }
        }






        #endregion


        #region Validation

        public static ProcessTrace ValidateDBVersions(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().ValidateDBVersions(projectCode, onNotificationStateChanged);
            }
        }

        public static ProcessTrace ValidateProjectConfig(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().ValidateProjectConfig(projectCode, onNotificationStateChanged);
            }
        }


        public static bool ValdiateTargetStateAlreadyExecuted(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().ValdiateTargetStateAlreadyExecuted(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            }
        }

        #endregion


        #region Run Change Db State

        public static ProcessTrace SyncDB(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().SyncDB(projectCode, onNotificationStateChanged);
            }
        }

        public static ProcessTrace RecreateDBFromScratch(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().RecreateDBFromScratch(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        public static ProcessTrace SetDBToSpecificState(string projectCode, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().SetDBToSpecificState(projectCode, targetStateScriptFilename, isIgnoreHistoryWarning, onNotificationStateChanged);
            }
        }


        public static ProcessTrace SetDBStateByVirtualExecution(string projectCode, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().SetDBStateByVirtualExecution(projectCode, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        #endregion


        #region Deploy

        public static ProcessTrace Deploy(string projectCode, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().Deploy(projectCode, onNotificationStateChanged);
            }
        }

        #endregion



        #region Scripts

        public static ScriptFilesState CreateScriptFilesState(ProjectConfigItem projectConfig)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().CreateScriptFilesState(projectConfig);
            }
        }

        public static string CreateNewIncrementalScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().CreateNewIncrementalScriptFile(projectConfig, scriptName);
            }
        }

        public static string CreateNewRepeatableScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().CreateNewRepeatableScriptFile(projectConfig, scriptName);
            }
        }

        public static string CreateNewDevDummyDataScriptFile(ProjectConfigItem projectConfig, string scriptName)
        {
            lock (_processSyncLock)
            {
                return GetInstanceNewDBVersionsAPI().CreateNewDevDummyDataScriptFile(projectConfig, scriptName);
            }
        }


        #endregion





    }
}
