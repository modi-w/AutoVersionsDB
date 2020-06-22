using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class ArtifactFileValidationEngineFactory : NotificationableEngineFactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Artifact File Validation";

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public ArtifactFileValidationEngineFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
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
