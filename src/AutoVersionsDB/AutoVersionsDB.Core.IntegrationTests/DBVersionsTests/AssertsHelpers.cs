using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public static class AssertsHelpers
    {
        private static string c_targetStateFile_MiddleState => $"incScript_2020-02-25.102_CreateLookupTable2.sql";
        private static string c_targetStateFile_FinalState => $"incScript_2020-03-02.101_CreateInvoiceTable1.sql";

        private static ScriptFileTypeBase _incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
        private static ScriptFileTypeBase _repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
        private static ScriptFileTypeBase _devDummyDataScriptFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();

        private static FileChecksum _fileChecksum = new FileChecksum();




        public static void AssertProccessErrors(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                throw new Exception(processResults.GetOnlyErrorsHistoryAsString());
            }
        }

        public static void AssertNumOfOpenDbConnection(ProjectConfigItem projectConfig, NumOfConnections numOfOpenConnections_Before)
        {
            NumOfConnections numOfOpenConnections_After = DBHelper.GetNumOfOpenConnection(projectConfig);

            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToDB));
            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToMasterDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToMasterDB));
        }

        public static void AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType)
        {
            //Comment: this check is not work because the original bak files was backup on diffrent sql server



            //string dbBackupFileFullPath;
            //using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            //{
            //    DataTable executionHistoryTable = dbCommands.Instance.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName);

            //    DataRow lastRow = executionHistoryTable.Rows[executionHistoryTable.Rows.Count - 1];

            //    dbBackupFileFullPath = Convert.ToString(lastRow["DBBackupFileFullPath"]);
            //}

            //FileInfo fiOriginalDBFile = new FileInfo(originalRestoreDBFilePath);
            //FileInfo finNewBackupDBFile = new FileInfo(dbBackupFileFullPath);


            //Assert.That(fiOriginalDBFile.Length, Is.EqualTo(finNewBackupDBFile.Length));

        }




        public static void AssertIncrementalFilesWithDbExecuted(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllIncrementalScriptFiles = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllIncrementalScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }

        public static void AssertRepeatableFilesWithDbExecuted(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }
        public static void AssertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);



            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

                if (fiScriptFile.Name == "rptScript_DataForLookupTable1.sql")
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(2));

                    DataRow firstInstance = executedScriptRows[0];
                    Assert.That(firstInstance["ComputedFileHash"].ToString() != computedFileHash);

                    DataRow secondInstance = executedScriptRows[1];
                    Assert.That(secondInstance["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
                else
                {
                    Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                    DataRow executedScriptRow = executedScriptRows.First();
                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
            }
        }

        public static void AssertDevDummyDataFilesWithDbExecuted(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }
        public static void AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
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
        public static void AssertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(ProjectConfigItem projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
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
                    Assert.That(secondInstance["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
                else
                {

                    Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                    DataRow executedScriptRow = executedScriptRows.First();
                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
            }
        }



    }
}
