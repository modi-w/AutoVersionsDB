using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DeliveryArtifactFolderPathValidator : ValidatorBase
    {
        public override string ValidatorName => "DeliveryArtifactFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        private ProjectConfigItem _projectConfigItem;

        public DeliveryArtifactFolderPathValidator(ProjectConfigItem projectConfig)
        {
            _projectConfigItem = projectConfig;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (string.IsNullOrWhiteSpace(_projectConfigItem.DeliveryArtifactFolderPath))
            {
                string errorMsg = "Delivery Artifact Folder Path is empty";
                return errorMsg;
            }
            else
            {
                if (!Directory.Exists(_projectConfigItem.DeliveryArtifactFolderPath))
                {
                    string errorMsg = "Delivery Artifact Folder is not exist";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
