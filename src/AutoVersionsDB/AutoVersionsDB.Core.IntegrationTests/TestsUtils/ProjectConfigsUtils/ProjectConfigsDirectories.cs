using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils
{
    public class ProjectConfigsDirectories
    {
        public static string DefaultBackupsFolder => FileSystemPathUtils.ParsePathVaribles($@"[CommonApplicationData]\AutoVersionsDB\Backups\{IntegrationTestsConsts.DummyProjectConfigValid.Id}");

        public void ClearAutoCreatedFolders()
        {
            ResolveDeleteFolder(DefaultBackupsFolder);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.DevScriptsBaseFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.IncrementalScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.RepeatableScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.DevDummyDataScriptsFolderPath);
            ResolveDeleteFolder(IntegrationTestsConsts.DummyProjectConfigValid.DeployArtifactFolderPath);
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
