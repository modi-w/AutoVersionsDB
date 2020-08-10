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
        private ArtifactExtractor _artifactExtractor;


        public AutoVersionsDbScriptsEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                            NotificationableActionStepBase rollbackStep)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
        }

        protected override void OnInitiated(InitiateEngineEventArgs e)
        {
            e.ThrowIfNull(nameof(e));

            ProjectConfigItem projectConfig = e.EngineConfig as ProjectConfigItem;

            _artifactExtractor = new ArtifactExtractor(projectConfig);

            base.OnInitiated(e);
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
