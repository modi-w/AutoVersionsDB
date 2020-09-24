using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using Moq;
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

        protected Mock<ProjectConfigsStorage> _mockProjectConfigsStorage;

        protected DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        protected FileChecksum _fileChecksum;


        public AutoVersionsDbAPI_TestsBase()
        {
            _ninjectKernelContainer = new StandardKernel();
            _ninjectKernelContainer.Load(Assembly.GetExecutingAssembly());

            _mockProjectConfigsStorage = new Mock<ProjectConfigsStorage>();
            _mockProjectConfigsStorage.Setup(m => m.IsIdExsit(It.IsAny<string>())).Returns(true);
            _ninjectKernelContainer.Bind<ProjectConfigsStorage>().ToConstant(_mockProjectConfigsStorage.Object);

            NinjectUtils.SetKernelInstance(_ninjectKernelContainer);


            _incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
            _repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
            _devDummyDataScriptFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();

            _fileChecksum = new FileChecksum();

            _dbCommandsFactoryProvider = new DBCommandsFactoryProvider();
        }

        #region Setup

        [SetUp]
        public void Init()
        {
            string dbBackupFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder);
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


        protected void assertProccessErrors(ProcessTrace processResults)
        {
            if (processResults.HasError)
            {
                throw new Exception(processResults.GetOnlyErrorsHistoryAsString());
            }
        }


        protected NumOfConnections getNumOfOpenConnection(ProjectConfigItem projectConfig)
        {
            NumOfConnections numOfConnectionsItem = new NumOfConnections();

            string masterDBName;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionStringToMasterDB, 0).AsDisposable())
            {
                masterDBName =  dbCommands.Instance.GetDataBaseName();
            }


            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                numOfConnectionsItem.DBName =  dbCommands.Instance.GetDataBaseName();
            }

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(projectConfig.DBType, projectConfig.ConnectionStringToMasterDB).AsDisposable())
            {
                numOfConnectionsItem.NumOfConnectionsToDB = dbQueryStatus.Instance.GetNumOfOpenConnection(numOfConnectionsItem.DBName);
                numOfConnectionsItem.NumOfConnectionsToMasterDB = dbQueryStatus.Instance.GetNumOfOpenConnection(masterDBName);
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
            using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {

                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBType, projectConfig.ConnectionStringToMasterDB, 0).AsDisposable())
                {
                    string dbTestsBaseLocation = Path.Combine(FileSystemPathUtils.CommonApplicationData, "AutoVersionsDB.BL.IntegrationTests", "TestsDBs");
                    if (!Directory.Exists(dbTestsBaseLocation))
                    {
                        Directory.CreateDirectory(dbTestsBaseLocation);

                    }

                    dbBackupRestoreCommands.Instance.RestoreDbFromBackup(filename, dbConnection.Instance.DataBaseName, dbTestsBaseLocation);
                }
            }
        }


        protected static void RemoveArtifactTempFolder(ProjectConfigItemForTestBase projectConfig)
        {
            if (Directory.Exists(projectConfig.DeliveryExtractedFilesArtifactFolder))
            {
                Directory.Delete(projectConfig.DeliveryExtractedFilesArtifactFolder, true);
            }
        }






        #region Assert DB State



        protected void assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(ProjectConfigItem projectConfig, string originalRestoreDBFilePath)
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

        protected void assertDbInEmptyStateExceptSystemTables(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                DataTable allSchemaTable = dbCommands.Instance.GetAllDBSchemaExceptDBVersionSchema();

                Assert.That(allSchemaTable.Rows.Count, Is.EqualTo(0));
            }


        }


        protected void assertDbInMiddleState(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
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

                bool isSpOnTable1Exist = dbCommands.Instance.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }


        }

        protected void assertDbInFinalState_DevEnv(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
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
                Assert.That(invoiceTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(invoiceTable1Data.Rows[0]["Comments"], Is.EqualTo("Comment 1"));
                Assert.That(invoiceTable1Data.Rows[1]["Comments"], Is.EqualTo("Comment 2"));

                bool isTransTable1Exist = dbCommands.Instance.CheckIfTableExist("Schema3", "TransTable1");
                Assert.That(isTransTable1Exist, Is.True);

                DataTable transTable1Data = dbCommands.Instance.GetTable("Schema3.TransTable1");
                Assert.That(transTable1Data.Rows.Count, Is.EqualTo(2));
                Assert.That(transTable1Data.Rows[0]["TotalPrice"], Is.EqualTo(200));
                Assert.That(transTable1Data.Rows[1]["TotalPrice"], Is.EqualTo(1000));


                bool isSpOnTable1Exist = dbCommands.Instance.CheckIfStoredProcedureExist("Schema1", "SpOnTable1");
                Assert.That(isSpOnTable1Exist, Is.True);
            }

        }


        //Comment: Dev Dummy Data Scripts should not run on Delivery Environment
        protected void assertDbInFinalState_DeliveryEnv(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
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


        protected void assertDbInFinalState_OnlyIncremental(ProjectConfigItem projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
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


        #endregion


        protected void assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(ProjectConfigItemForTestBase projectConfig)
        {

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                ArtifactExtractor artifactExtractor = null;

                DataTable dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);

                if (!projectConfig.DevEnvironment)
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

                    string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

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

                    string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

                    Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
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

                        Assert.That(dddSctipFilesDictionary.ContainsKey(filename));

                        FileInfo fiScriptFile = dddSctipFilesDictionary[filename];

                        string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);

                        Assert.That(executedScriptRow["ComputedFileHash"], Is.EqualTo(computedFileHash));
                    }
                }



                if (artifactExtractor != null)
                {
                    artifactExtractor.Dispose();
                    artifactExtractor = null;
                }
            }




        }


        protected void assertThatDbExecutedFilesAreInMiddleState(ProjectConfigItemForTestBase projectConfig)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                DataTable dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);

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
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);
            }

            if (!projectConfig.DevEnvironment)
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
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBType, projectConfig.ConnectionString, 0).AsDisposable())
            {
                dbScriptsExecutionHistoryFilesTable = dbCommands.Instance.GetTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);
            }

            if (!projectConfig.DevEnvironment)
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

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
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

                string computedFileHash = _fileChecksum.GetHashByFilePath(fiScriptFile.FullName);
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

        private void assertDevDummyDataFilesWithDbExecuted(ProjectConfigItemForTestBase projectConfig, DataTable dbScriptsExecutionHistoryFilesTable)
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
