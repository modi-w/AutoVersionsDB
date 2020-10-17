using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DeliveryArtifactFolderPathValidator : ValidatorBase
    {
        private readonly string _deliveryArtifactFolderPath;

        public override string ValidatorName => "DeliveryArtifactFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DeliveryArtifactFolderPathValidator(string deliveryArtifactFolderPath)
        {
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_deliveryArtifactFolderPath))
            {
                string errorMsg = "Delivery Artifact Folder Path is mandatory";
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
