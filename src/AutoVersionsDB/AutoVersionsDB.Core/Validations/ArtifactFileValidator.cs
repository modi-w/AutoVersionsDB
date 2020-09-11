using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System.IO;

namespace AutoVersionsDB.Core.Validations
{
    public class ArtifactFileValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _deliveryArtifactFolderPath;

        public override string ValidatorName => "ArtifactFile";

        public override string ErrorInstructionsMessage => "Artifact File not exist";


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
