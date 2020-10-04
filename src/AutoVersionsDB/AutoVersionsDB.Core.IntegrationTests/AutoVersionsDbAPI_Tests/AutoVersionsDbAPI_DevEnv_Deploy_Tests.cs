//using AutoVersionsDB.Core.DBVersions.ArtifactFile;
//using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
//using AutoVersionsDB.NotificationableEngine;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.IO;
//using System.IO.Compression;
//using System.Linq;

//namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
//{
//    public class AutoVersionsDbAPI_DevEnv_Deploy_Tests : AutoVersionsDbAPI_TestsBase
//    {

//        [Test]
//        public void Deploy([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
//        {
//            //Arrange
//            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);


//            //Act
//            ProcessResults processResults = AutoVersionsDbAPI.Deploy(projectConfig.Id, null);


//            //Assert
//            assertProccessErrors(processResults.Trace);
//            assertThat_NewFileInTheDeployPath_And_ItsContentBeEqualToTheDevScriptsFolder(projectConfig);

//        }

//        private void assertThat_NewFileInTheDeployPath_And_ItsContentBeEqualToTheDevScriptsFolder(ProjectConfigItemForTestBase projectConfig)
//        {
//            string[] allArtifactFiles = Directory.GetFiles(projectConfig.DeployArtifactFolderPath, $"*{ArtifactExtractor.ArtifactFilenameExtension}", SearchOption.TopDirectoryOnly);

//            Assert.That(allArtifactFiles.Length, Is.EqualTo(1));

//            string artifactFile = allArtifactFiles[0];

//            string tempFolder = Path.Combine(projectConfig.DeployArtifactFolderPath, "TempFolder");

//            if (Directory.Exists(tempFolder))
//            {
//                Directory.Delete(tempFolder, true);
//            }

//            Directory.CreateDirectory(tempFolder);

//            try
//            {
//                ZipFile.ExtractToDirectory(artifactFile, tempFolder);

//                string[] incrementalScriptFilesFromDevFolder = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

//                string incrementalTempFolder = Path.Combine(tempFolder, _incrementalScriptFileType.RelativeFolderName);
//                string[] incrementalScriptFilesExtractFolder = Directory.GetFiles(incrementalTempFolder, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

//                compareTwoFoldersFiles(incrementalScriptFilesFromDevFolder, incrementalScriptFilesExtractFolder);

//                string[] repeatableScriptFilesFromDevFolder = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

//                string repeatableTempFolder = Path.Combine(tempFolder, _repeatableScriptFileType.RelativeFolderName);
//                string[] repeatableScriptFilesExtractFolder = Directory.GetFiles(repeatableTempFolder, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

//                compareTwoFoldersFiles(repeatableScriptFilesFromDevFolder, repeatableScriptFilesExtractFolder);

//                string devDummyDataTempFolder = Path.Combine(tempFolder, _devDummyDataScriptFileType.RelativeFolderName);
//                Assert.That(Directory.Exists(devDummyDataTempFolder), Is.EqualTo(false));
//            }
//            finally
//            {
//                Directory.Delete(tempFolder, true);
//            }
//        }

//        private void compareTwoFoldersFiles(string[] scriptFilesFromDevFolder, string[] scriptFilesExtractFolder)
//        {
//            Dictionary<string, FileInfo> devFolderFileInfosDictionary = scriptFilesFromDevFolder.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

//            Dictionary<string, FileInfo> extractFolderFileInfosDictionary = scriptFilesExtractFolder.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

//            Assert.That(devFolderFileInfosDictionary.Count == extractFolderFileInfosDictionary.Count);

//            foreach (FileInfo devFolderFileInfo in devFolderFileInfosDictionary.Values)
//            {
//                Assert.That(extractFolderFileInfosDictionary.ContainsKey(devFolderFileInfo.Name));

//                FileInfo extractFileInfo = extractFolderFileInfosDictionary[devFolderFileInfo.Name];

//                string devFolderFileHash = _fileChecksum.GetHashByFilePath(devFolderFileInfo.FullName);
//                string extractFolderFileHash = _fileChecksum.GetHashByFilePath(extractFileInfo.FullName);

//                Assert.That(devFolderFileHash, Is.EqualTo(devFolderFileHash));
//            }
//        }
//    }
//}
