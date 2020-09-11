using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ArtifactFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "ArtifactFile";

        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            ArtifactFileValidator artifactFileValidator = new ArtifactFileValidator(projectConfig.IsDevEnvironment, projectConfig.DeliveryArtifactFolderPath);
            validationsGroup.Add(artifactFileValidator);

            return validationsGroup;
        }
    }
}
