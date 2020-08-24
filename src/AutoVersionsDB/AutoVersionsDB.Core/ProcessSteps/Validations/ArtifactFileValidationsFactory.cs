using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ArtifactFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "ArtifactFile";

        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.IsDevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validationsGroup.Add(artifactFileValidator);

            return validationsGroup;
        }
    }
}
