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
            this.DevScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(AppGlobals.AppSetting.DevScriptsBaseFolderPath_Normal);
            this.DeployArtifactFolderPath = FileSystemHelpers.ParsePathVaribles(AppGlobals.AppSetting.DeployArtifact_FolderPath);
            this.DeliveryArtifactFolderPath = FileSystemHelpers.ParsePathVaribles(AppGlobals.AppSetting.DevScriptsBaseFolderPath_Normal);
            this.DBBackupBaseFolder = FileSystemHelpers.ParsePathVaribles(AppGlobals.AppSetting.DBBackupBaseFolder);
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
