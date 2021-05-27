using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class ArtifactFileValidationsFactory : ValidationsFactory
    {
        public const string Name = "Artifact File";
        public override string ValidationName => Name;


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.DevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validationsGroup.Add(artifactFileValidator);

            return validationsGroup;
        }
    }
}
