using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public abstract class AutoVersionsDbScriptsEngine : AutoVersionsDbEngine
    {
        private ScriptFilesComparersManager _scriptFilesComparersManager;

        public AutoVersionsDbScriptsEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            NotificationableActionStepBase rollbackStep,
                                            ScriptFilesComparersManager scriptFilesComparersManager)
            : base(notificationExecutersFactoryManager, rollbackStep)
        {
            _scriptFilesComparersManager = scriptFilesComparersManager;
        }

        protected override void OnPrepared(PrepareEngineEventArgs e)
        {
            ProjectConfigItem projectConfig = e.EngineConfig as ProjectConfigItem;

            _scriptFilesComparersManager.Load(projectConfig);

            base.OnPrepared(e);
        }
    } 
}
