using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.Validations
{
    public class ArtifactFileValidator : ValidatorBase
    {
        private readonly bool _isDevEnvironment;
        private readonly string _deliveryArtifactFolderPath;

        internal override string ValidatorName => "ArtifactFile";

        internal override string ErrorInstructionsMessage => "Artifact File not exist";


        internal ArtifactFileValidator(bool isDevEnvironment, 
                                        string deliveryArtifactFolderPath)
        {
            _isDevEnvironment = isDevEnvironment;
            _deliveryArtifactFolderPath = deliveryArtifactFolderPath;
        }

        internal override string Validate()
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
