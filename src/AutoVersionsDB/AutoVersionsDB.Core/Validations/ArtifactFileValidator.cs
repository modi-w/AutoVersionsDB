using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.Engines;
using System.IO;

namespace AutoVersionsDB.Core.Validations
{
    public class ArtifactFileValidator : ValidatorBase
    {
        public override string ValidatorName => "ArtifactFile";

        public override string ErrorInstructionsMessage => "Artifact File not exist";

        private bool _isDevEnvironment;
        private string _deliveryArtifactFolderPath;

        public ArtifactFileValidator(bool isDevEnvironment, string deliveryArtifactFolderPath)
        {
            _isDevEnvironment = isDevEnvironment;
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (!_isDevEnvironment)
            {
                if (!Directory.Exists(_deliveryArtifactFolderPath))
                {
                    string errorMsg = "Delivery Foder not exist";
                    return errorMsg;
                }

                string[] artifactFiles = Directory.GetFiles(_deliveryArtifactFolderPath, $"*{ArtifactExtractor.ArtifactFilenameExtension}");

                if (artifactFiles.Length == 0)
                {
                    string errorMsg = "Delivery Artifact File not exist";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
