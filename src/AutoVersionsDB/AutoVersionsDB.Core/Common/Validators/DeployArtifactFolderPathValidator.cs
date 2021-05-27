using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DeployArtifactFolderPathValidator : ValidatorBase
    {
        private readonly string _deployArtifactFolderPath;

        public const string Name = "DeployArtifactFolderPath";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public DeployArtifactFolderPathValidator(string deployArtifactFolderPath)
        {
            _deployArtifactFolderPath = deployArtifactFolderPath;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_deployArtifactFolderPath))
            {
                return CoreTextResources.MandatoryFieldErrorMessage.Replace("[FieldName]", "Deploy Artifact Folder Path");
            }


            return "";
        }
    }
}
