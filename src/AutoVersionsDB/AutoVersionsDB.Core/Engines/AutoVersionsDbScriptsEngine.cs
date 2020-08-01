using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public abstract class AutoVersionsDbScriptsEngine : AutoVersionsDbEngine
    {
        private readonly ScriptFilesComparersManager _scriptFilesComparersManager;

        public AutoVersionsDbScriptsEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                            NotificationableActionStepBase rollbackStep,
                                            ScriptFilesComparersManager scriptFilesComparersManager)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
            _scriptFilesComparersManager = scriptFilesComparersManager;
        }

        protected override void OnPreparing(PrepareEngineEventArgs e)
        {
            e.ThrowIfNull(nameof(e));

            ProjectConfigItem projectConfig = e.EngineConfig as ProjectConfigItem;

            _scriptFilesComparersManager.Load(projectConfig);

            base.OnPreparing(e);
        }
    } 
}
