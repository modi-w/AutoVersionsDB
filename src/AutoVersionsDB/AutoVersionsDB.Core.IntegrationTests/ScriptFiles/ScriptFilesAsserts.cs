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
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ScriptFiles
{
    public class ScriptFilesAsserts
    {
        private const string c_targetStateFile_MiddleState = "incScript_2020-02-25.102_CreateLookupTable2.sql";
        private const string c_targetStateFile_FinalState = "incScript_2020-03-02.101_CreateInvoiceTable1.sql";

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

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
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

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
            }
        }

        public void AssertMatchRepeatableFilesWithDbExecuted_ForRunAgainAfterRepetableFilesChanged(string testName, ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

                if (fiScriptFile.Name == "rptScript_DataForLookupTable1.sql")
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(2), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 2 times.");

                    DataRow firstInstance = executedScriptRows[0];
                    Assert.That(firstInstance["ComputedFileHash"].ToString() != computedFileHash);

                    DataRow secondInstance = executedScriptRows[1];
                    Assert.That(secondInstance["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
                else
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(1), $"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                    DataRow executedScriptRow = executedScriptRows.First();
                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
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
                Assert.That(executedScriptRows.Count, Is.EqualTo(1),$"{testName} -> The file '{fiScriptFile.Name}' exsit in the db '{executedScriptRows.Count}' times, should be 1 time.");

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
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
                    Assert.That(secondInstance["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
                }
                else
                {

                    Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                    DataRow executedScriptRow = executedScriptRows.First();
                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash), $"{testName} -> The file '{fiScriptFile.Name}' has diffrent hash from the file in the db.");
                }
            }
        }

    }
}
