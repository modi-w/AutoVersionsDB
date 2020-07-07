using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ArtifactFileValidationStep : ValidationsStep
    {
        protected override bool ShouldContinueWhenFindError => false;

        public ArtifactFileValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            SingleValidationStep singleValidationStep)
            : base(notificationExecutersFactoryManager, singleValidationStep)
        {
        }

        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.IsDevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            Validators.Add(artifactFileValidator);

        }
    }
}
