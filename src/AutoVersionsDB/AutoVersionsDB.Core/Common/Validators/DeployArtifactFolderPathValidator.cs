using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class DeployArtifactFolderPathValidator : ValidatorBase
    {
        private readonly string _deployArtifactFolderPath;

        public override string ValidatorName => "DeployArtifactFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DeployArtifactFolderPathValidator(string deployArtifactFolderPath)
        {
            _deployArtifactFolderPath = deployArtifactFolderPath;
        }

        public override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_deployArtifactFolderPath))
            {
                string errorMsg = "Deploy Artifact Folder Path is Mandatory";
                return errorMsg;
            }


            return "";
        }
    }
}
