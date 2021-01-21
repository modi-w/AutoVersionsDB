using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class ArtifactFileValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _deliveryArtifactFolderPath;

        public const string Name = "ArtifactFile";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ArtifactFileNotExistErrorMessage;
        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public ArtifactFileValidator(bool isDevEnvironment,
                                        string deliveryArtifactFolderPath)
        {
            _isDevEnvironment = isDevEnvironment;
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        public override string Validate()
        {
            if (!_isDevEnvironment)
            {
                if (!Directory.Exists(_deliveryArtifactFolderPath))
                {
                    return CoreTextResources.ArtifactFolderExistErrorMessage;
                }

                string[] artifactFiles = Directory.GetFiles(_deliveryArtifactFolderPath, $"*{ArtifactExtractor.ArtifactFilenameExtension}");

                if (artifactFiles.Length == 0)
                {
                    return CoreTextResources.ArtifactFileNotExistErrorMessage;
                }
            }

            return "";
        }
    }
}
