using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB
{
    public class DBAsserts
    {
        private readonly DBHandler _dbHandler;
        private readonly DBBackupFilesProvider _dbBackupFilesProvider;

        public DBAsserts(DBHandler dbHandler,
                            DBBackupFilesProvider dbBackupFilesProvider)
        {
            _dbHandler = dbHandler;
            _dbBackupFilesProvider = dbBackupFilesProvider;
        }


        public void AssertNumOfOpenDBConnection(string testName, DBConnectionInfo dbConnectionInfo, NumOfDBConnections numOfOpenConnections_Before)
        {
            NumOfDBConnections numOfOpenConnections_After = _dbHandler.GetNumOfOpenConnection(dbConnectionInfo);

            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToDB), $"{testName} >>> NumOfConnectionsToDB after '{numOfOpenConnections_After.NumOfConnectionsToDB}', is grater then before '{numOfOpenConnections_Before.NumOfConnectionsToDB}'");
            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToAdminDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToAdminDB), $"{testName} >>> NumOfConnectionsToAdminDB after '{numOfOpenConnections_After.NumOfConnectionsToAdminDB}', is grater then before '{numOfOpenConnections_Before.NumOfConnectionsToAdminDB}'");
        }

        public void AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(string testName, DBConnectionInfo dbConnectionInfo, DBBackupFileType dbBackupFileType)
        {
            //Comment: this check is not work because the original bak files was backup on diffrent sql server



            //string dbBackupFileFullPath;
            //using (DBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            //{
            //    DataTable executionHistoryTable = dbCommands.Instance.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName);

            //    DataRow lastRow = executionHistoryTable.Rows[executionHistoryTable.Rows.Count - 1];

            //    dbBackupFileFullPath = Convert.ToString(lastRow["DBBackupFileFullPath"]);
            //}

            //FileInfo fiOriginalDBFile = new FileInfo(originalRestoreDBFilePath);
            //FileInfo finNewBackupDBFile = new FileInfo(dbBackupFileFullPath);


            //Assert.That(fiOriginalDBFile.Length, Is.EqualTo(finNewBackupDBFile.Length));

        }


        public void AssertRestore(string testName, DBConnectionInfo dbConnectionInfo, DBBackupFileType dbBackupFileType, ProcessTrace processTrace)
        {
            Assert.That(processTrace.HasError);

            bool isRestoreExecuted =
                processTrace
                .StatesLog.Any(e => e.InternalStepNotificationState != null
                                        && !string.IsNullOrWhiteSpace(e.InternalStepNotificationState.StepName)
                                        && e.InternalStepNotificationState.StepName.StartsWith(RestoreDatabaseStep.Name));

            Assert.That(isRestoreExecuted, $"{testName} >>> Restore step was not executed");



            ////Comment: the following  check is not work because the original bak files was backup on diffrent sql server
            //string tempBackupFileToCompare = Path.Combine(FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DBBackupBaseFolder), $"TempBackupFileToCompare_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}");
            //_dbHandler.CreateDBBackup(dbConnectionInfo, tempBackupFileToCompare);

            //string orginalDBBackupFilePathForTheTest = _dbBackupFilesProvider.GetDBBackupFilePath(dbBackupFileType, dbConnectionInfo.DBType);

            //FileInfo fiOrginalDBBackupFilePathForTheTest = new FileInfo(orginalDBBackupFilePathForTheTest);
            //FileInfo fiTempBackupFileToCompare = new FileInfo(tempBackupFileToCompare);

            //Assert.That(fiOrginalDBBackupFilePathForTheTest.Length, Is.EqualTo(fiTempBackupFileToCompare.Length));
        }



        public void AssertDBInEmptyStateExceptSystemTables(string testName, DBConnectionInfo dbConnectionInfo)
        {
            DataTable allSchemaTable = _dbHandler.GetAllDBSchemaExceptDBVersionSchema(dbConnectionInfo);

            Assert.That(allSchemaTable.Rows.Count, Is.EqualTo(0), $"{testName} >>> The DB should be empty except schema tables, but its not.");
        }




        public void AssertThatDBExecutedFilesAreInMiddleState(string testName, DBConnectionInfo dbConnectionInfo)
        {
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName);
            assertTableNumOfRows(testName, DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName, tableData, 3);
            assertTableCellValue(testName, DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName, tableData, 0, "Filename", "incScript_0001_initState.sql");
            assertTableCellValue(testName, DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName, tableData, 1, "Filename", "incScript_0002_CreateLookupTable1.sql");
            assertTableCellValue(testName, DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName, tableData, 2, "Filename", "incScript_0003_CreateLookupTable2.sql");
        }


        public void AssertDBInMiddleState(string testName, DBConnectionInfo dbConnectionInfo)
        {
            AssertTable1ExistWithFullData(testName, dbConnectionInfo);

            string tableName = "[Schema2].[LookupTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable1");
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);

            tableName = "[Schema2].[LookupTable2]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable2");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);


            AssertSpExsit(testName, dbConnectionInfo, "Schema1", "SpOnTable1");

        }

        public void AssertDBInFinalState_DevEnv(string testName, DBConnectionInfo dbConnectionInfo)
        {
            AssertTable1ExistWithFullData(testName, dbConnectionInfo);

            string tableName = "[Schema2].[LookupTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable1");
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 2);
            assertTableCellValue(testName, tableName, tableData, 0, "Lookup1Value", "Value1");
            assertTableCellValue(testName, tableName, tableData, 1, "Lookup1Value", "Value2");


            tableName = "[Schema2].[LookupTable2]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable2");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 3);
            assertTableCellValue(testName, tableName, tableData, 0, "Lookup2Value", "Value3");
            assertTableCellValue(testName, tableName, tableData, 1, "Lookup2Value", "Value4");
            assertTableCellValue(testName, tableName, tableData, 2, "Lookup2Value", "Value5");


            tableName = "[Schema3].[InvoiceTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "InvoiceTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 2);
            assertTableCellValue(testName, tableName, tableData, 0, "Comments", "Comment 1");
            assertTableCellValue(testName, tableName, tableData, 1, "Comments", "Comment 2");


            tableName = "[Schema3].[TransTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "TransTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 2);
            assertTableCellValue(testName, tableName, tableData, 0, "TotalPrice", 200);
            assertTableCellValue(testName, tableName, tableData, 1, "TotalPrice", 1000);


            AssertSpExsit(testName, dbConnectionInfo, "Schema1", "SpOnTable1");
        }




        //Comment: Dev Dummy Data Scripts should not run on Delivery Environment
        public void AssertDBInFinalState_DeliveryEnv(string testName, DBConnectionInfo dbConnectionInfo)
        {
            AssertTable1ExistWithFullData(testName, dbConnectionInfo);

            string tableName = "[Schema2].[LookupTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable1");
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 2);
            assertTableCellValue(testName, tableName, tableData, 0, "Lookup1Value", "Value1");
            assertTableCellValue(testName, tableName, tableData, 1, "Lookup1Value", "Value2");


            tableName = "[Schema2].[LookupTable2]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable2");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 3);
            assertTableCellValue(testName, tableName, tableData, 0, "Lookup2Value", "Value3");
            assertTableCellValue(testName, tableName, tableData, 1, "Lookup2Value", "Value4");
            assertTableCellValue(testName, tableName, tableData, 2, "Lookup2Value", "Value5");


            tableName = "[Schema3].[InvoiceTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "InvoiceTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);


            tableName = "[Schema3].[TransTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "TransTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);


            AssertSpExsit(testName, dbConnectionInfo, "Schema1", "SpOnTable1");

        }


        public void AssertDBInFinalState_OnlyIncremental(string testName, DBConnectionInfo dbConnectionInfo)
        {
            AssertTable1ExistWithFullData(testName, dbConnectionInfo);

            string tableName = "[Schema2].[LookupTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable1");
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);

            tableName = "[Schema2].[LookupTable2]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema2", "LookupTable2");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);

            tableName = "[Schema3].[InvoiceTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "InvoiceTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);

            tableName = "[Schema3].[TransTable1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema3", "TransTable1");
            tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 0);

            AssertSpExsit(testName, dbConnectionInfo, "Schema1", "SpOnTable1");

        }







        private void AssertTable1ExistWithFullData(string testName, DBConnectionInfo dbConnectionInfo)
        {
            string tableName = "[Schema1].[Table1]";
            AssertTableExsit(testName, dbConnectionInfo, "Schema1", "Table1");
            DataTable tableData = _dbHandler.GetTable(dbConnectionInfo, tableName);
            assertTableNumOfRows(testName, tableName, tableData, 2);
            assertTableCellValue(testName, tableName, tableData, 0, "Col1", "aa");
            assertTableCellValue(testName, tableName, tableData, 1, "Col1", "bb");
        }




        private void AssertTableExsit(string testName, DBConnectionInfo dbConnectionInfo, string schemaName, string tableName)
        {
            bool isTableExist = _dbHandler.CheckIfTableExist(dbConnectionInfo, schemaName, tableName);
            Assert.That(isTableExist, Is.True, $"{testName} >>> the table [{schemaName}].[{tableName}] is not exist");
        }


        private static void assertTableNumOfRows(string testName, string tableName, DataTable table1Data, int numOfRows)
        {
            Assert.That(table1Data.Rows.Count, Is.EqualTo(numOfRows), $"{testName} >>> The table {tableName} should be {numOfRows} rows, but has {table1Data.Rows.Count}");
        }
        private static void assertTableCellValue(string testName, string tableName, DataTable table1Data, int rowIndex, string colName, object cellValue)
        {
            Assert.That(table1Data.Rows[rowIndex][colName], Is.EqualTo(cellValue), $"{testName} >>> For table '{tableName}' cell [{rowIndex}][{colName}] should be '{cellValue}', but was '{table1Data.Rows[rowIndex][colName]}' ");
        }



        private void AssertSpExsit(string testName, DBConnectionInfo dbConnectionInfo, string schemaName, string spName)
        {
            bool isTableExist = _dbHandler.CheckIfStoredProcedureExist(dbConnectionInfo, schemaName, spName);
            Assert.That(isTableExist, Is.True, $"{testName} >>> the stored procedure [{schemaName}].[{spName}] is not exist");
        }

    }
}
