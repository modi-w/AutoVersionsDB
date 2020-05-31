﻿using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    [TestFixture]
    public class AutoVersionsDbAPI_TestsBase
    {
        protected string c_targetStateFile_MiddleState => $"incScript_2020-02-25.102_CreateLookupTable2.sql";
        protected string c_targetStateFile_FinalState => $"incScript_2020-03-02.101_CreateInvoiceTable1.sql";

        protected ScriptFileTypeBase _incrementalScriptFileType;
        protected ScriptFileTypeBase _repeatableScriptFileType;
        protected ScriptFileTypeBase _devDummyDataScriptFileType;

        protected StandardKernel _ninjectKernelContainer;
        protected AutoVersionsDbAPI _autoVersionsDbAPI;

        protected DBCommands_FactoryProvider _dbCommands_FactoryProvider;

        protected FileChecksumManager _fileChecksumManager;


        public AutoVersionsDbAPI_TestsBase()
        {
            _ninjectKernelContainer = new StandardKernel();
            _ninjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            NinjectUtils.SetKernelInstance(_ninjectKernelContainer);

            _incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
            _repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
            _devDummyDataScriptFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();

            _fileChecksumManager = new FileChecksumManager();

            _dbCommands_FactoryProvider = new DBCommands_FactoryProvider();
        }

        #region Setup

        [SetUp]
        public void Init()
        {
            _autoVersionsDbAPI = _ninjectKernelContainer.Get<AutoVersionsDbAPI>();

            string dbBackupFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder); 
            if (!Directory.Exists(dbBackupFolderPath))
            {
                Directory.CreateDirectory(dbBackupFolderPath);
            }

            string[] filesToDelete = Directory.GetFiles(dbBackupFolderPath);
            foreach (string filenameToDelete in filesToDelete)
            {
                File.Delete(filenameToDelete);
            }

        }

        #endregion



        #region Config Types

        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DevEnv_ValidScripts = new[]
        {
           new ProjectConfigItemForTest_DevEnv_SqlServer(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DevEnv_ChangedHistoryFiles_Incremental = new[]
        {
           new ProjectConfigItemForTest_DevEnv_SqlServer(IntegrationTestsSetting.DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DevEnv_ChangedHistoryFiles_Repeatable = new[]
        {
           new ProjectConfigItemForTest_DevEnv_SqlServer(IntegrationTestsSetting.DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DevEnv_MissingFile = new[]
        {
           new ProjectConfigItemForTest_DevEnv_SqlServer(IntegrationTestsSetting.DevScriptsBaseFolderPath_MissingFile),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DevEnv_ScriptError = new[]
        {
           new ProjectConfigItemForTest_DevEnv_SqlServer(IntegrationTestsSetting.DevScriptsBaseFolderPath_ScriptError),
        };


        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_ValidScripts = new[]
        {
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_Normal),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Incremental = new[]
        {
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Repeatable = new[]
        {
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_MissingFile = new[]
        {
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_MissingFileh),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_ScriptError = new[]
        {
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_ScriptError),
        };
        protected static readonly IEnumerable<ProjectConfigItemForTestBase> ProjectConfigItemArray_DeliveryEnv_WithDevDummyDataFiles = new[]
{
           new ProjectConfigItemForTest_DeliveryEnv_SqlServer(IntegrationTestsSetting.DeliveryArtifactFolderPath_WithDevDummyDataFiles),
        };

        #endregion


        protected void assertProccessErrors()
        {
            if (_autoVersionsDbAPI.HasError)
            {
                throw new Exception(_autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager.GetOnlyErrorsHistoryAsString());
            }
        }


        protected NumOfConnections getNumOfOpenConnection(ProjectConfigItem projectConfig)
        {
            NumOfConnections numOfConnectionsItem = new NumOfConnections();

            string masterDBName;
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, 0))
            {
                masterDBName = dbCommands.GetDataBaseName();
            }


            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                numOfConnectionsItem.DBName = dbCommands.GetDataBaseName();
            }

            using (IDBQueryStatus dbQueryStatus = _dbCommands_FactoryProvider.CreateDBQueryStatus(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB))
            {
                numOfConnectionsItem.NumOfConnectionsToDB = dbQueryStatus.GetNumOfOpenConnection(numOfConnectionsItem.DBName);
                numOfConnectionsItem.NumOfConnectionsToMasterDB = dbQueryStatus.GetNumOfOpenConnection(masterDBName);
            }

            return numOfConnectionsItem;
        }

        protected void assertNumOfOpenDbConnection(ProjectConfigItemForTestBase projectConfig, NumOfConnections numOfOpenConnections_Before)
        {
            NumOfConnections numOfOpenConnections_After = getNumOfOpenConnection(projectConfig);

            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToDB));
            Assert.That(numOfOpenConnections_Before.NumOfConnectionsToMasterDB, Is.GreaterThanOrEqualTo(numOfOpenConnections_After.NumOfConnectionsToMasterDB));
        }






        protected void restoreDB(ProjectConfigItem projectConfig, string filename)
        {
            using (IDBConnectionManager dbConnectionManager = _dbCommands_FactoryProvider.CreateDBConnectionManager(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {

                using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommands_FactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, 0))
                {
                    string dbTestsBaseLocation = Path.Combine(Utils.FileSystemPathUtils.CommonApplicationData, "AutoVersionsDB.BL.IntegrationTests", "TestsDBs");
                    if (!Directory.Exists(dbTestsBaseLocation))
                    {
                        Directory.CreateDirectory(dbTestsBaseLocation);

                    }

                    dbBackupRestoreCommands.RestoreDbFromBackup(filename, dbConnectionManager.DataBaseName, dbTestsBaseLocation);
                }
            }
        }





        #region Assert DB State



        protected void assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(ProjectConfigItem projectConfig, string originalRestoreDBFilePath)
        {
            string dbBackupFileFullPath;
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                DataTable executionHistoryTable = dbCommands.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName);

                DataRow lastRow = executionHistoryTable.Rows[executionHistoryTable.Rows.Count - 1];

                dbBackupFileFullPath = Convert.ToString(lastRow["DBBackupFileFullPath"]);
            }

            FileInfo fiOriginalDBFile = new FileInfo(originalRestoreDBFilePath);
            FileInfo finNewBackupDBFile = new FileInfo(dbBackupFileFullPath);


            Assert.That(fiOriginalDBFile.Length, Is.EqualTo(finNewBackupDBFile.Length));

        }

        protected void assertDbInEmptyStateExceptSystemTables(ProjectConfigItem projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                DataTable allSchemaTable = dbCommands.GetAllDBSchemaExceptDBVersionSchema();

                Assert.That(allSchemaTable.Rows.Count, Is.EqualTo(0));
            }


        }


        protected void assertDbInMiddleState(ProjectConfigItem projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                bool isTable1Exist = dbCommands.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(0));

                bool isLookupTable2Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(0));

                bool isSpOnTable1Exist = dbCommands.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }


        }

        protected void assertDbInFinalState_DevEnv(ProjectConfigItem projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                bool isTable1Exist = dbCommands.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(lookupTable1Data.Rows[0]["Lookup1Value"], Is.EqualTo("Value1"));
                Assert.That(lookupTable1Data.Rows[1]["Lookup1Value"], Is.EqualTo("Value2"));

                bool isLookupTable2Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(3));
                Assert.That(lookupTable2Data.Rows[0]["Lookup2Value"], Is.EqualTo("Value3"));
                Assert.That(lookupTable2Data.Rows[1]["Lookup2Value"], Is.EqualTo("Value4"));
                Assert.That(lookupTable2Data.Rows[2]["Lookup2Value"], Is.EqualTo("Value5"));

                bool isInvoiceTable1Exist = dbCommands.CheckIfTableExist("Schema3", "InvoiceTable1");
                Assert.That(isInvoiceTable1Exist, Is.True);

                DataTable invoiceTable1Data = dbCommands.GetTable("Schema3.InvoiceTable1");
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(invoiceTable1Data.Rows[0]["Comments"], Is.EqualTo("Comment 1"));
                Assert.That(invoiceTable1Data.Rows[1]["Comments"], Is.EqualTo("Comment 2"));

                bool isTransTable1Exist = dbCommands.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(transTable1Data.Rows[0]["TotalPrice"], Is.EqualTo(200));
                Assert.That(transTable1Data.Rows[1]["TotalPrice"], Is.EqualTo(1000));


                bool isSpOnTable1Exist = dbCommands.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }


        //Comment: Dev Dummy Data Scripts should not run on Delivery Environment
        protected void assertDbInFinalState_DeliveryEnv(ProjectConfigItem projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                bool isTable1Exist = dbCommands.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(lookupTable1Data.Rows[0]["Lookup1Value"], Is.EqualTo("Value1"));
                Assert.That(lookupTable1Data.Rows[1]["Lookup1Value"], Is.EqualTo("Value2"));

                bool isLookupTable2Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(3));
                Assert.That(lookupTable2Data.Rows[0]["Lookup2Value"], Is.EqualTo("Value3"));
                Assert.That(lookupTable2Data.Rows[1]["Lookup2Value"], Is.EqualTo("Value4"));
                Assert.That(lookupTable2Data.Rows[2]["Lookup2Value"], Is.EqualTo("Value5"));

                bool isInvoiceTable1Exist = dbCommands.CheckIfTableExist("Schema3", "InvoiceTable1");
                Assert.That(isInvoiceTable1Exist, Is.True);

                DataTable invoiceTable1Data = dbCommands.GetTable("Schema3.InvoiceTable1");
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(0));

                bool isTransTable1Exist = dbCommands.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(0));


                bool isSpOnTable1Exist = dbCommands.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }


        protected void assertDbInFinalState_OnlyIncremental(ProjectConfigItem projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                bool isTable1Exist = dbCommands.CheckIfTableExist("Schema1", "Table1");
                Assert.That(isTable1Exist, Is.True);

                DataTable table1Data = dbCommands.GetTable("Schema1.Table1");
                Assert.That(table1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(table1Data.Rows[0]["Col1"], Is.EqualTo("aa"));
                Assert.That(table1Data.Rows[1]["Col1"], Is.EqualTo("bb"));

                bool isLookupTable1Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable1");
                Assert.That(isLookupTable1Exist, Is.True);

                DataTable lookupTable1Data = dbCommands.GetTable("Schema2.LookupTable1");
                Assert.That(lookupTable1Data.Rows.Count, Is.EqualTo(0));

                bool isLookupTable2Exist = dbCommands.CheckIfTableExist("Schema2", "LookupTable2");
                Assert.That(isLookupTable2Exist, Is.True);

                DataTable lookupTable2Data = dbCommands.GetTable("Schema2.LookupTable2");
                Assert.That(lookupTable2Data.Rows.Count, Is.EqualTo(0));

                bool isInvoiceTable1Exist = dbCommands.CheckIfTableExist("Schema3", "InvoiceTable1");
                Assert.That(isInvoiceTable1Exist, Is.True);

                DataTable invoiceTable1Data = dbCommands.GetTable("Schema3.InvoiceTable1");
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(0));

                bool isTransTable1Exist = dbCommands.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(0));


                bool isSpOnTable1Exist = dbCommands.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }


        #endregion


        protected void assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(ProjectConfigItemForTestBase projectConfig)
        {
            ArtifactExtractor artifactExtractor = null;

            IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0);

            DataTable dbScriptsExecutionHistoryFilesTable = dbCommands.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName);

            if (!projectConfig.IsDevEnvironment)
            {
                artifactExtractor = new ArtifactExtractor(projectConfig);
            }


            string[] arrIncAllScriptFiles = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
            Dictionary<string, FileInfo> incSctipFilesDictionary = arrIncAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);

            List<DataRow> incExecutionHistoryFilesDBRows =
                dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _incrementalScriptFileType.FileTypeCode).ToList();

            foreach (DataRow executedScriptRow in incExecutionHistoryFilesDBRows)
            {
                string filename = Convert.ToString(executedScriptRow["Filename"]);

                Assert.That(incSctipFilesDictionary.ContainsKey(filename));

                FileInfo fiScriptFile = incSctipFilesDictionary[filename];

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);

                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }


            string[] arrRptAllScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
            Dictionary<string, FileInfo> rptSctipFilesDictionary = arrRptAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);


            List<DataRow> rptExecutionHistoryFilesDBRows =
                dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _repeatableScriptFileType.FileTypeCode).ToList();

            foreach (DataRow executedScriptRow in rptExecutionHistoryFilesDBRows)
            {
                string filename = Convert.ToString(executedScriptRow["Filename"]);

                Assert.That(rptSctipFilesDictionary.ContainsKey(filename));

                FileInfo fiScriptFile = rptSctipFilesDictionary[filename];

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);

                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }


            if (projectConfig.IsDevEnvironment)
            {
                string[] arrDddAllScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);
                Dictionary<string, FileInfo> dddSctipFilesDictionary = arrDddAllScriptFiles.Select(e => new FileInfo(e)).ToDictionary(e => e.Name);


                List<DataRow> dddExecutionHistoryFilesDBRows =
                    dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["ScriptFileType"]) == _devDummyDataScriptFileType.FileTypeCode).ToList();

                foreach (DataRow executedScriptRow in dddExecutionHistoryFilesDBRows)
                {
                    string filename = Convert.ToString(executedScriptRow["Filename"]);

                    Assert.That(dddSctipFilesDictionary.ContainsKey(filename));

                    FileInfo fiScriptFile = dddSctipFilesDictionary[filename];

                    string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);

                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
                }
            }



            if (artifactExtractor != null)
            {
                artifactExtractor.Dispose();
                artifactExtractor = null;
            }

            dbCommands.Dispose();
            dbCommands = null;

        }


        protected void assertThatDbExecutedFilesAreInMiddleState(ProjectConfigItemForTestBase projectConfig)
        {
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                DataTable dbScriptsExecutionHistoryFilesTable = dbCommands.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName);

                List<DataRow> executedDBFileList = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().OrderBy(row => Convert.ToInt32(row["ID"])).ToList();
                Assert.That(executedDBFileList.Count, Is.EqualTo(3));

                Assert.That(executedDBFileList[0]["Filename"], Is.EqualTo("incScript_2020-02-25.100_initState.sql"));
                Assert.That(executedDBFileList[1]["Filename"], Is.EqualTo("incScript_2020-02-25.101_CreateLookupTable1.sql"));
                Assert.That(executedDBFileList[2]["Filename"], Is.EqualTo("incScript_2020-02-25.102_CreateLookupTable2.sql"));
            }

        }


        protected void assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(ProjectConfigItemForTestBase projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable;
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName);
            }

            if (!projectConfig.IsDevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    assertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    assertRepeatableFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    assertDevDummyDataFilesWithDbExecuted_DeliveryEnv(projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                assertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                assertRepeatableFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                assertDevDummyDataFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }

        protected void assertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(ProjectConfigItemForTestBase projectConfig)
        {
            DataTable dbScriptsExecutionHistoryFilesTable;
            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.GetTable(DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName);
            }

            if (!projectConfig.IsDevEnvironment)
            {
                using (ArtifactExtractor artifactExtractor = new ArtifactExtractor(projectConfig))
                {
                    assertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    assertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);

                    assertDevDummyDataFilesWithDbExecuted_DeliveryEnv(projectConfig, dbScriptsExecutionHistoryFilesTable);
                }
            }
            else
            {
                assertIncrementalFilesWithDbExecuted(projectConfig, dbScriptsExecutionHistoryFilesTable);

                assertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);

                assertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(projectConfig, dbScriptsExecutionHistoryFilesTable);
            }
        }


        private void assertIncrementalFilesWithDbExecuted(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllIncrementalScriptFiles = Directory.GetFiles(projectConfig.IncrementalScriptsFolderPath, $"{_incrementalScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllIncrementalScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }

        private void assertRepeatableFilesWithDbExecuted(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }
        private void assertRepeatableFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllRepeatableScriptFiles = Directory.GetFiles(projectConfig.RepeatableScriptsFolderPath, $"{_repeatableScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);



            foreach (string scriptFile in arrAllRepeatableScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);

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

        private void assertDevDummyDataFilesWithDbExecuted(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();
                Assert.That(executedScriptRows.Count, Is.EqualTo(1));

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);
                DataRow executedScriptRow = executedScriptRows.First();
                Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
            }
        }
        private void assertDevDummyDataFilesWithDbExecuted_DeliveryEnv(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
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
        private void assertDevDummyDataFilesWithDbExecuted_RunAgainAfterRepetableFilesChanged(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
        {
            string[] arrAllDevDummyDataScriptFiles = Directory.GetFiles(projectConfig.DevDummyDataScriptsFolderPath, $"{_devDummyDataScriptFileType.Prefix}*.sql", SearchOption.AllDirectories);

            foreach (string scriptFile in arrAllDevDummyDataScriptFiles)
            {
                FileInfo fiScriptFile = new FileInfo(scriptFile);

                List<DataRow> executedScriptRows = dbScriptsExecutionHistoryFilesTable.Rows.Cast<DataRow>().Where(row => Convert.ToString(row["Filename"]) == fiScriptFile.Name).ToList();

                string computedFileHash = _fileChecksumManager.GetMd5HashByFilePath(fiScriptFile.FullName);

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
