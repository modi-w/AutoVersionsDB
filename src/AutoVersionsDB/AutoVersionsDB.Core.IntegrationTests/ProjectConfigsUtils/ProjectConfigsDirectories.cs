using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils
{
    public class ProjectConfigsDirectories
    {
        public static string DefaultBackupsFolder => FileSystemPathUtils.ParsePathVaribles($@"[CommonApplicationData]\AutoVersionsDB\Backups\{IntegrationTestsConsts.DummyProjectConfig.Id}");

        public void ClearAutoCreatedFolders()
        {
            ResolveDeleteFolder(ProjectConfigsDirectories.DefaultBackupsFolder);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.IncrementalScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.RepeatableScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.DevDummyDataScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath);
        }

        public void ResolveDeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }


        public void AssertDirectoryExist(string testName, string path)
        {
            Assert.That(Directory.Exists(path), $"{testName} -> The BackupFolderPath folder ('{path}') is not exist");
        }


    }
}
