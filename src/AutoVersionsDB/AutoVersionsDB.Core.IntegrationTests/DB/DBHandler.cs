using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DB
{
    public class DBHandler
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly DBBackupFilesProvider _dbBackupFilesProvider;


        public DBHandler(DBCommandsFactoryProvider dbCommandsFactoryProvider,
                            DBBackupFilesProvider dbBackupFilesProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _dbBackupFilesProvider = dbBackupFilesProvider;
        }

        public void RestoreDB(DBConnectionInfo dbConnectionInfo, DBBackupFileType dbBackupFileType)
        {
            using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(dbConnectionInfo).AsDisposable())
            {
                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(dbConnectionInfo).AsDisposable())
                {
                    string dbTestsBaseLocation = Path.Combine(FileSystemPathUtils.CommonApplicationData, "AutoVersionsDB.IntegrationTests", "TestsDBs");
                    if (!Directory.Exists(dbTestsBaseLocation))
                    {
                        Directory.CreateDirectory(dbTestsBaseLocation);
                    }

                    string filename = _dbBackupFilesProvider.GetDBBackupFilePath(dbBackupFileType, dbConnectionInfo.DBType);
                    dbBackupRestoreCommands.Instance.RestoreDbFromBackup(filename, dbConnection.Instance.DataBaseName, dbTestsBaseLocation);
                }
            }
        }


        public void CreateDBBackup(DBConnectionInfo dbConnectionInfo, string targetFilePath)
        {
            using (var dbConnection = _dbCommandsFactoryProvider.CreateDBConnection(dbConnectionInfo).AsDisposable())
            {
                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(dbConnectionInfo).AsDisposable())
                {
                    dbBackupRestoreCommands.Instance.CreateDbBackup(targetFilePath, dbConnectionInfo.DBName);
                }
            }
        }



        public NumOfDBConnections GetNumOfOpenConnection(DBConnectionInfo dbConnectionInfo)
        {
            NumOfDBConnections numOfConnectionsItem = new NumOfDBConnections();

            string masterDBName;
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                masterDBName = dbCommands.Instance.GetDataBaseName();
            }


            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                numOfConnectionsItem.DBName = dbCommands.Instance.GetDataBaseName();
            }

            using (var dbQueryStatus = _dbCommandsFactoryProvider.CreateDBQueryStatus(dbConnectionInfo).AsDisposable())
            {
                numOfConnectionsItem.NumOfConnectionsToDB = dbQueryStatus.Instance.GetNumOfOpenConnection(numOfConnectionsItem.DBName);
                numOfConnectionsItem.NumOfConnectionsToAdminDB = dbQueryStatus.Instance.GetNumOfOpenConnection(masterDBName);
            }

            return numOfConnectionsItem;
        }




        public bool CheckIfTableExist(DBConnectionInfo dbConnectionInfo, string schemaName, string tableName)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                return dbCommands.Instance.CheckIfTableExist(schemaName, tableName);
            }
        }

        public DataTable GetTable(DBConnectionInfo dbConnectionInfo, string tableName)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                return dbCommands.Instance.GetTable(tableName);
            }
        }


        public bool CheckIfStoredProcedureExist(DBConnectionInfo dbConnectionInfo, string schemaName, string spName)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                return dbCommands.Instance.CheckIfStoredProcedureExist(schemaName, spName);
            }
        }

        public DataTable GetAllDBSchemaExceptDBVersionSchema(DBConnectionInfo dbConnectionInfo)
        {
            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo).AsDisposable())
            {
                return dbCommands.Instance.GetAllDBSchemaExceptDBVersionSchema();
            }
        }

        
    }
}
