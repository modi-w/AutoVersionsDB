using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.NotificationableEngine;
using Ninject;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core
{
    public static class AutoVersionsDBAPI
    {

        private static readonly object _processSyncLock = new object();


        private static readonly ProjectConfigsAPI _projectConfigsAPI = DIConfig.Kernel.Get<ProjectConfigsAPI>();
        private static readonly DBVersionsAPI _dbVersionsAPI = DIConfig.Kernel.Get<DBVersionsAPI>();





        #region Config

        public static IList<DBType> DBTypes => _projectConfigsAPI.DBTypes;


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


        #region Run Change DB State

        public static ProcessResults SyncDB(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SyncDB(id, onNotificationStateChanged);
            }
        }

        public static ProcessResults RecreateDBFromScratch(string id, TargetScripts targetScripts, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.RecreateDBFromScratch(id, targetScripts, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBToSpecificState(string id, TargetScripts targetScripts, bool isIgnoreHistoryWarning, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SetDBToSpecificState(id, targetScripts, isIgnoreHistoryWarning, onNotificationStateChanged);
            }
        }


        public static ProcessResults SetDBStateByVirtualExecution(string id, TargetScripts targetScripts, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.SetDBStateByVirtualExecution(id, targetScripts, onNotificationStateChanged);
            }
        }

        public static ProcessResults VirtualDDD(string id, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            lock (_processSyncLock)
            {
                return _dbVersionsAPI.VirtualDDD(id, onNotificationStateChanged);
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
