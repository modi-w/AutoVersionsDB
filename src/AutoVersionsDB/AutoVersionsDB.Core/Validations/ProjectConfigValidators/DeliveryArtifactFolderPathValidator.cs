using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DeliveryArtifactFolderPathValidator : ValidatorBase
    {
        private readonly ProjectConfigItem _projectConfigItem;

        public override string ValidatorName => "DeliveryArtifactFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DeliveryArtifactFolderPathValidator(ProjectConfigItem projectConfig)
        {
            _projectConfigItem = projectConfig;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
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
