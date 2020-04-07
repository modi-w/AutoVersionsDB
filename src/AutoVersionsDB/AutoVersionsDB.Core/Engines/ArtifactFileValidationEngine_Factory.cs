using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.ValidationsStep;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class ArtifactFileValidationEngine_Factory : NotificationableEngine_FactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Artifact File Validation";

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public ArtifactFileValidationEngine_Factory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .ArtifactFileValidation(projectConfig);


            return engine;
        }


    }
}
