using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DeliveryArtifactFolderPathValidator : ValidatorBase
    {
        private readonly string _deliveryArtifactFolderPath;

        public const string Name = "DeliveryArtifactFolderPath";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public DeliveryArtifactFolderPathValidator(string deliveryArtifactFolderPath)
        {
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_deliveryArtifactFolderPath))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "Delivery Artifact Folder Pat");
            }
            else
            {
                if (!Directory.Exists(_deliveryArtifactFolderPath))
                {
                    return CoreTextResources.DeliveryArtifactFolderNotExistErrorMessage;
                }
            }

            return "";
        }
    }
}
