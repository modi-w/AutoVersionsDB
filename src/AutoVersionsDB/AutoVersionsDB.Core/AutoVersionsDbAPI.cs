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


        private static ProjectConfigsAPI _projectConfigsAPI = NinjectUtils.KernelInstance.Get<ProjectConfigsAPI>();
        private static DBVersionsAPI _dbVersionsAPI = NinjectUtils.KernelInstance.Get<DBVersionsAPI>();





        #region Config

        public static List<DBType> GetDBTypes()
        {
            return _projectConfigsAPI.GetDBTypes();
        }


        public static List<ProjectConfigItem> GetProjectsList()
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.GetProjectsList();
            }
        }

        public static ProjectConfigItem GetProjectConfigById(string id)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.GetProjectConfigById(id);
            }
        }



        public static ProcessResults SaveNewProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.SaveNewProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessResults UpdateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.UpdateProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessResults ChangeProjectId(string prevId, string newId, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.ChangeProjectId(prevId, newId, onNotificationStateChanged);
            }
        }

        public static ProcessResults RemoveProjectConfig(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.RemoveProjectConfig(id, onNotificationStateChanged);
            }
        }






        #endregion


        #region Validation

        public static ProcessResults ValidateProjectConfig(ProjectConfigItem projectConfig, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _projectConfigsAPI.ValidateProjectConfig(projectConfig, onNotificationStateChanged);
            }
        }

        public static ProcessResults ValidateDBVersions(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.ValidateDBVersions(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults ValdiateTargetStateAlreadyExecuted(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.ValdiateTargetStateAlreadyExecuted(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }

        #endregion


        #region Run Change Db State

        public static ProcessResults SyncDB(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SyncDB(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults RecreateDBFromScratch(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.RecreateDBFromScratch(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBToSpecificState(string id, string targetStateScriptFilename, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SetDBToSpecificState(id, targetStateScriptFilename, isIgnoreHistoryWarning, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBStateByVirtualExecution(string id, string targetStateScriptFilename, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SetDBStateByVirtualExecution(id, targetStateScriptFilename, onNotificationStateChanged);
            }
        }


        #endregion


        #region Deploy

        public static ProcessResults Deploy(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.Deploy(id, onNotificationStateChanged);
            }
        }

        #endregion



        #region Scripts

        public static ProcessResults GetScriptFilesState(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.GetScriptFilesState(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewIncrementalScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.CreateNewIncrementalScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewRepeatableScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.CreateNewRepeatableScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }

        public static ProcessResults CreateNewDevDummyDataScriptFile(string id, string scriptName, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.CreateNewDevDummyDataScriptFile(id, scriptName, onNotificationStateChanged);
            }
        }


        #endregion





    }
}
