using AutoVersionsDB.Core.ArtifactFile;
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
        private ArtifactExtractor _artifactExtractor;


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

            _artifactExtractor = new ArtifactExtractor(projectConfig);

            _scriptFilesComparersManager.Load(projectConfig);

            base.OnPreparing(e);
        }



        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_artifactExtractor != null)
                {
                    _artifactExtractor.Dispose();
                }
            }

            _disposed = true;
            // Call base class implementation.
            base.Dispose(disposing);
        }

       
    }
}
