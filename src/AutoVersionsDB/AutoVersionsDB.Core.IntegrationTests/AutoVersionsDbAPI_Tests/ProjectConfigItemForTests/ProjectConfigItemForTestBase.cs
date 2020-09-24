﻿using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using System;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests
{
    public abstract class ProjectConfigItemForTestBase : ProjectConfigItem
    {

        public ProjectConfigItemForTestBase()
        {
            //this.ProjectGuid = Guid.NewGuid().ToString();
            this.Code = "IntegrationTestProject";
            this.DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DeployArtifact_FolderPath);
            this.DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);
            this.BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder);
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
