using AutoVersionsDB.Helpers;
using System.IO;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class CreateMissingFoldersStep : ProjectConfigStep
    {
        public override string StepName => "Create Missing Folders";


        public CreateMissingFoldersStep()
        {
        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ResolveFolderExist(processContext.ProjectConfig.BackupFolderPath);

            if (processContext.ProjectConfig.DevEnvironment)
            {
                ResolveFolderExist(processContext.ProjectConfig.DevScriptsBaseFolderPath);
                ResolveFolderExist(processContext.ProjectConfig.IncrementalScriptsFolderPath);
                ResolveFolderExist(processContext.ProjectConfig.RepeatableScriptsFolderPath);
                ResolveFolderExist(processContext.ProjectConfig.DevDummyDataScriptsFolderPath);

                ResolveFolderExist(processContext.ProjectConfig.DeployArtifactFolderPath);
            }
            else
            {
                ResolveFolderExist(processContext.ProjectConfig.DeliveryArtifactFolderPath);
            }

        }

        private void ResolveFolderExist(string folderPath)
        {
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
        }

    }
}
