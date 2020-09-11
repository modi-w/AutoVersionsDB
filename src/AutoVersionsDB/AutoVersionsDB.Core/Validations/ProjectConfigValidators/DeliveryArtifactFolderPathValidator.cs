using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DeliveryArtifactFolderPathValidator : ValidatorBase
    {
        private readonly string _deliveryArtifactFolderPath;

        internal override string ValidatorName => "DeliveryArtifactFolderPath";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal DeliveryArtifactFolderPathValidator(string deliveryArtifactFolderPath)
        {
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        internal override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_deliveryArtifactFolderPath))
            {
                string errorMsg = "Delivery Artifact Folder Path is empty";
                return errorMsg;
            }
            else
            {
                if (!Directory.Exists(_deliveryArtifactFolderPath))
                {
                    string errorMsg = "Delivery Artifact Folder is not exist";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
