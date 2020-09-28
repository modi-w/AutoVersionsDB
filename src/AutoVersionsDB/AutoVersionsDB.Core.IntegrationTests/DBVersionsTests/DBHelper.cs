using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public static class DBHelper
    {
        private static DBCommandsFactoryProvider _dbCommandsFactoryProvider = new DBCommandsFactoryProvider();

        public static void RestoreDB(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType)
        {
            using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(projectConfig.DBConnectionInfo).AsDisposable())
            {
                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBConnectionInfo).AsDisposable())
                {
                    string dbTestsBaseLocation = Path.Combine(FileSystemPathUtils.CommonApplicationData, "AutoVersionsDB.IntegrationTests", "TestsDBs");
                    if (!Directory.Exists(dbTestsBaseLocation))
                    {
                        Directory.CreateDirectory(dbTestsBaseLocation);
                    }

                    string filename = DBBackupFilesProvider.GetDBBackupFilePath(dbBackupFileType, projectConfig.DBType);
                    dbBackupRestoreCommands.Instance.RestoreDbFromBackup(filename, dbConnection.Instance.DataBaseName, dbTestsBaseLocation);
                }
            }
        }

        public static NumOfConnections GetNumOfOpenConnection(ProjectConfigItem projectConfig)
        {
            NumOfConnections numOfConnectionsItem = new NumOfConnections();

            string masterDBName;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                masterDBName = dbCommands.Instance.GetDataBaseName();
            }


            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                numOfConnectionsItem.DBName = dbCommands.Instance.GetDataBaseName();
            }

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBConnectionInfo).AsDisposable())
            {
                numOfConnectionsItem.NumOfConnectionsToDB = dbQueryStatus.Instance.GetNumOfOpenConnection(numOfConnectionsItem.DBName);
                numOfConnectionsItem.NumOfConnectionsToMasterDB = dbQueryStatus.Instance.GetNumOfOpenConnection(masterDBName);
            }

            return numOfConnectionsItem;
        }


        //Comment: Dev Dummy Data Scripts should not run on Delivery Environment
        public static void AssertDbInFinalState_DeliveryEnv(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                bool isTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.Instance.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.Instance.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(lookupTable1Data.Rows[0]["Lookup1Value"], Is.EqualTo("Value1"));
                Assert.That(lookupTable1Data.Rows[1]["Lookup1Value"], Is.EqualTo("Value2"));

                bool isLookupTable2Exist = dbCommands.Instance.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.Instance.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(3));
                Assert.That(lookupTable2Data.Rows[0]["Lookup2Value"], Is.EqualTo("Value3"));
                Assert.That(lookupTable2Data.Rows[1]["Lookup2Value"], Is.EqualTo("Value4"));
                Assert.That(lookupTable2Data.Rows[2]["Lookup2Value"], Is.EqualTo("Value5"));

                bool isInvoiceTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema3", "InvoiceTable1");
                Assert.That(isInvoiceTable1Exist, Is.True);

                DataTable invoiceTable1Data = dbCommands.Instance.GetTable("Schema3.InvoiceTable1");
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(0));

                bool isTransTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.Instance.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(0));


                bool isSpOnTable1Exist = dbCommands.Instance.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }


        public static void AssertDbInFinalState_OnlyIncremental(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                bool isTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.Instance.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.Instance.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(0));

                bool isLookupTable2Exist = dbCommands.Instance.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.Instance.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(0));

                bool isInvoiceTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema3", "InvoiceTable1");
                Assert.That(isInvoiceTable1Exist, Is.True);

                DataTable invoiceTable1Data = dbCommands.Instance.GetTable("Schema3.InvoiceTable1");
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(0));

                bool isTransTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.Instance.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(0));


                bool isSpOnTable1Exist = dbCommands.Instance.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }



        public static void AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(ProjectConfigItem projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);
            }

            if (!projectConfig.DevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    AssertsHelpers.AssertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertsHelpers.AssertRepeatableFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertsHelpers.AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                AssertsHelpers.AssertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertsHelpers.AssertRepeatableFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertsHelpers.AssertDevDummyDataFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }

        public static void AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(ProjectConfigItem projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);
            }

            if (!projectConfig.DevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    AssertsHelpers.AssertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertsHelpers.AssertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    AssertsHelpers.AssertDevDummyDataFilesWithDbExecuted_DeliveryEnv(projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                AssertsHelpers.AssertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertsHelpers.AssertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);

                AssertsHelpers.AssertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }




    }
}
