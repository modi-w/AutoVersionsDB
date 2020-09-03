using AutoVersionsDB.Common;
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
            this.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeployArtifact_FolderPath);
            this.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.DBBackupBaseFolder = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder);
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
