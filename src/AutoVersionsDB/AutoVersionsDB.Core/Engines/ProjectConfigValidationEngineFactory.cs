using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Engines
{
    public class ProjectConfigValidationEngineFactory : NotificationableEngineFactoryBase<AutoVersionsDbEngine, ProjectConfigItem>
    {
        public override string EngineTypeName => "Project Config Validation";

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;

        public ProjectConfigValidationEngineFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public override AutoVersionsDbEngine Create(ProjectConfigItem projectConfig)
        {
            AutoVersionsDbEngine engine = new AutoVersionsDbEngine(_notificationExecutersFactoryManager);

            engine.EngineTypeName(EngineTypeName)
                .ProjectConfigValidation(projectConfig);

            return engine;
        }


    }
}
