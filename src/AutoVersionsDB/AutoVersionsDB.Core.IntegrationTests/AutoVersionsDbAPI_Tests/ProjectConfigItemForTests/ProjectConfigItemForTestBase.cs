using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using System;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public abstract class ProjectConfigItemForTestBase : ProjectConfigItem
    {

        public ProjectConfigItemForTestBase()
        {
            //this.ProjectGuid = Guid.NewGuid().ToString();
            this.Id = "IntegrationTestProject";
            this.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);
            this.DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeployArtifact_FolderPath);
            this.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal);
            this.BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackupBaseFolder);
        }

        public override string ToString()
        {
            string envStr;

            if (this.DevEnvironment)
            {
                envStr = "Dev";
            }
            else
            {
                envStr = "Delivery";
            }

            return $"{envStr} Env -  {this.DBType}";
        }
    }
}
