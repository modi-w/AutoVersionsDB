using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class ArtifactFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "ArtifactFile";

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.DevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validationsGroup.Add(artifactFileValidator);

            return validationsGroup;
        }
    }
}
