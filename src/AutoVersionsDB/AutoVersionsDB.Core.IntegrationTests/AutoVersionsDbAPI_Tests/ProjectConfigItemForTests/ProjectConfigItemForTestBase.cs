using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using System;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public abstract class ProjectConfigItemForTestBase : ProjectConfigItem
    {

        public ProjectConfigItemForTestBase()
        {
            this.ProjectGuid = Guid.NewGuid().ToString();
            this.ProjectName = "IntegrationTestProject";
            this.DevScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.DeployArtifactFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DeployArtifact_FolderPath);
            this.DeliveryArtifactFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.DBBackupBaseFolder = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder);
        }

        public override string ToString()
        {
            string envStr;

            if (this.IsDevEnvironment)
            {
                envStr = "Dev";
            }
            else
            {
                envStr = "Delivery";
            }

            return $"{envStr} Env -  {this.DBTypeCode}";
        }
    }
}
