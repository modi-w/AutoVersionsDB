using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ScriptFiles
{
    public class ScriptFilesAsserts
    {
        private ScriptFileTypeBase _incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
        private ScriptFileTypeBase _repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
        private ScriptFileTypeBase _devDummyDataScriptFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();

        private readonly FileChecksum _fileChecksum;
        private readonly DBHandler _dbHandler;


        public ScriptFilesAsserts(FileChecksum fileChecksum,
                                    DBHandler dbHandler)
        {
            _fileChecksum = fileChecksum;
            _dbHandler = dbHandler;
        }


        public void AssertScriptFileExsit(string testName, string scriptFilename)
        {
            Assert.That(File.Exists(scriptFilename), $"{testName} -> The file: '{scriptFilename}', is missing");
        }


        public void AssertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(string testName, ProjectConfigItem projectConfig)
        {
            ArtifactExtractor artifactExtractor = null;

            if (!projectConfig.DevEnvironment)
            {
                artifactExtractor = new ArtifactExtractor(projectConfig);
            }

            DataTable dbScriptsExecutionHistoryFilesTable = _dbHandler.GetTable(projectConfig.DBConnectionInfo, DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);


            string[] arrIncAllScriptFiles = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
            Dictionary<string, FileInfo> incSctipFilesDictionary = arrIncAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

            List<DataRow> incExecutionHistoryFilesDBRows =
                dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _incrementalScriptFileType.FileTypeCode).ToList();

            foreach (DataRow executedScriptRow in incExecutionHistoryFilesDBRows)
            {
                string filename = Convert.ToString(executedScriptRow["Filename"]);

                AssertFileFromDBExistInDictionaryFolderFiles(testName, incSctipFilesDictionary, filename);

                FileInfo fiScriptFile = incSctipFilesDictionary[filename];

                AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
            }


            string[] arrRptAllScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
            Dictionary<string, FileInfo> rptSctipFilesDictionary = arrRptAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);


            List<DataRow> rptExecutionHistoryFilesDBRows =
                dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _repeatableScriptFileType.FileTypeCode).ToList();

            foreach (DataRow executedScriptRow in rptExecutionHistoryFilesDBRows)
            {
                string filename = Convert.ToString(executedScriptRow["Filename"]);

                AssertFileFromDBExistInDictionaryFolderFiles(testName, rptSctipFilesDictionary, filename);

                FileInfo fiScriptFile = rptSctipFilesDictionary[filename];

                AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
            }


            if (projectConfig.DevEnvironment)
            {
                string[] arrDddAllScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
                Dictionary<string, FileInfo> dddSctipFilesDictionary = arrDddAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);


                List<DataRow> dddExecutionHistoryFilesDBRows =
                    dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _devDummyDataScriptFileType.FileTypeCode).ToList();

                foreach (DataRow executedScriptRow in dddExecutionHistoryFilesDBRows)
                {
                    string filename = Convert.ToString(executedScriptRow["Filename"]);

                    AssertFileFromDBExistInDictionaryFolderFiles(testName, dddSctipFilesDictionary, filename);

                    FileInfo fiScriptFile = dddSctipFilesDictionary[filename];

                    AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
                }
            }




            if (artifactExtractor != null)
            {
                artifactExtractor.Dispose();
                artifactExtractor = null;
            }



        }

        public void AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(string testName, ProjectConfigItem projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable = _dbHandler.GetTable(projectConfig.DBConnectionInfo, DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);

            if (!projectConfig.DevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    AssertMatchIncrementalFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertMatchRepeatableFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                AssertMatchIncrementalFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertMatchRepeatableFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertMatchDevDummyDataFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }

        public void AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(string testName, ProjectConfigItem projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable = _dbHandler.GetTable(projectConfig.DBConnectionInfo, DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);

            if (!projectConfig.DevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    AssertMatchIncrementalFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertMatchRepeatableFilesWithDbExecuted_ForRunAgainAfterRepetableFilesChanged(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                AssertMatchIncrementalFilesWithDbExecuted(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertMatchRepeatableFilesWithDbExecuted_ForRunAgainAfterRepetableFilesChanged(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(testName, projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }


        public void AssertMatchIncrementalFilesWithDbExecuted(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllIncrementalScriptFiles = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllIncrementalScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                DataRow executedScriptRow = executedScriptRows.First();

                AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
            }
        }


        public void AssertMatchRepeatableFilesWithDbExecuted(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                DataRow executedScriptRow = executedScriptRows.First();

                AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
            }
        }

        public void AssertMatchRepeatableFilesWithDbExecuted_ForRunAgainAfterRepetableFilesChanged(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();


                if (fiScriptFile.Name == "rptScript_DataForLookupTable1.sql")
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(2), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 2 times.");

                    string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                    DataRow firstInstance = executedScriptRows[0];
                    Assert.That(firstInstance["ComputedFileHash"].ToString() != computedFileHash);

                    DataRow secondInstance = executedScriptRows[1];
                    AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, secondInstance);
                }
                else
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(1), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                    DataRow executedScriptRow = executedScriptRows.First();
                    AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
                }
            }
        }

        public void AssertMatchDevDummyDataFilesWithDbExecuted(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                DataRow executedScriptRow = executedScriptRows.First();
                AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
            }
        }
        public void AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            if (Directory.Exists(projectConfig.DevDummyDataScriptsFolderPath))
            {
                string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

                foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
                {
                    FileInfo fiScriptFile = new FileInfo(scriptFile);

                    List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                    Assert.That(executedScriptRows.Count, Is.EqualTo(0));
                }
            }

        }
        public void AssertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

                if (fiScriptFile.Name == "dddScript_DataForTransTable1.sql")
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(2));

                    DataRow firstInstance = executedScriptRows[0];
                    Assert.That(firstInstance["ComputedFileHash"].ToString() != computedFileHash);

                    DataRow secondInstance = executedScriptRows[1];
                    AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, secondInstance);
                }
                else
                {

                    Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                    DataRow executedScriptRow = executedScriptRows.First();
                    AssertScriptFileAndDBRowHasSameHash(testName, fiScriptFile, executedScriptRow);
                }
            }
        }







        private void AssertScriptFileAndDBRowHasSameHash(string testName, FileInfo fiScriptFile, DataRow executedScriptRow)
        {
            string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
            Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
        }


        private static void AssertFileFromDBExistInDictionaryFolderFiles(string testName, Dictionary<string, FileInfo> incSctipFilesDictionary, string filename)
        {
            Assert.That(incSctipFilesDictionary.ContainsKey(filename), $"{testName} -> The file '{filename}' exist in the DB but not exsit in the scripts folder.");
        }




        public void AssertThat_NewFileInTheDeployPath_And_ItsContentBeEqualToTheDevScriptsFolder(string testName, ProjectConfigItem projectConfig)
        {
            string[] allArtifactFiles = Directory.GetFiles(projectConfig.DeployArtifactFolderPath, $"*{ArtifactExtractor.ArtifactFilenameExtension}", SearchOption.TopDirectoryOnly);

            Assert.That(allArtifactFiles.Length, Is.EqualTo(1), $"{testName} -> Found '{allArtifactFiles.Length}' Artifact Files, Should be only 1");

            string artifactFile = allArtifactFiles[0];

            string tempFolder = Path.Combine(projectConfig.DeployArtifactFolderPath, "TempFolder");

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            Directory.CreateDirectory(tempFolder);

            try
            {
                ZipFile.ExtractToDirectory(artifactFile, tempFolder);

                string[] incrementalScriptFilesFromDevFolder = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

                string incrementalTempFolder = Path.Combine(tempFolder, _incrementalScriptFileType.RelativeFolderName);
                string[] incrementalScriptFilesExtractFolder = Directory.GetFiles(incrementalTempFolder, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

                compareTwoFoldersFiles(testName, incrementalScriptFilesFromDevFolder, incrementalScriptFilesExtractFolder);

                string[] repeatableScriptFilesFromDevFolder = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

                string repeatableTempFolder = Path.Combine(tempFolder, _repeatableScriptFileType.RelativeFolderName);
                string[] repeatableScriptFilesExtractFolder = Directory.GetFiles(repeatableTempFolder, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

                compareTwoFoldersFiles(testName, repeatableScriptFilesFromDevFolder, repeatableScriptFilesExtractFolder);

                string devDummyDataTempFolder = Path.Combine(tempFolder, _devDummyDataScriptFileType.RelativeFolderName);
                Assert.That(!Directory.Exists(devDummyDataTempFolder), $"{testName} -> The folder: '{devDummyDataTempFolder}' should be deleted");
            }
            finally
            {
                Directory.Delete(tempFolder, true);
            }
        }

        private void compareTwoFoldersFiles(string testName, string[] scriptFilesFromDevFolder, string[] scriptFilesExtractFolder)
        {
            Dictionary<string, FileInfo> devFolderFileInfosDictionary = scriptFilesFromDevFolder.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

            Dictionary<string, FileInfo> extractFolderFileInfosDictionary = scriptFilesExtractFolder.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

            Assert.That(devFolderFileInfosDictionary.Count == extractFolderFileInfosDictionary.Count, $"{testName} -> Should be same number of files in the 'DevScriptsFiles' folder and in the zip folders");

            foreach (FileInfo devFolderFileInfo in devFolderFileInfosDictionary.Values)
            {
                Assert.That(extractFolderFileInfosDictionary.ContainsKey(devFolderFileInfo.Name), $"{testName} -> The file '{devFolderFileInfo.Name}' not exist in the related folder in the zip file");

                FileInfo extractFileInfo = extractFolderFileInfosDictionary[devFolderFileInfo.Name];

                string devFolderFileHash = _fileChecksum.GetHashByFilePath(devFolderFileInfo.FullName);
                string extractFolderFileHash = _fileChecksum.GetHashByFilePath(extractFileInfo.FullName);

                Assert.That(devFolderFileHash, Is.EqualTo(devFolderFileHash), $"{testName} -> The file from Dev folder and the file from the zip, should be the same Hash");
            }
        }



    }
}
