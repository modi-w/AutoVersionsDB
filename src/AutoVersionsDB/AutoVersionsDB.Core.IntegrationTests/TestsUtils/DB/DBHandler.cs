using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB
{
    public class DBHandler
    {
        private readonly DBCommandsFactory dbCommandsFactoryProvider;
        private readonly DBBackupFilesProvider _dbBackupFilesProvider;


        public DBHandler(DBCommandsFactory dbCommandsFactory,
                            DBBackupFilesProvider dbBackupFilesProvider)
        {
            dbCommandsFactoryProvider = dbCommandsFactory;
            _dbBackupFilesProvider = dbBackupFilesProvider;
        }

        public void RestoreDB(DBConnectionInfo dbConnectionInfo, DBBackupFileType dbBackupFileType)
        {
            using (var dbConnection = dbCommandsFactoryProvider.CreateDBConnection(dbConnectionInfo))
            {
                using (var dbBackupRestoreCommands = dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(dbConnectionInfo))
                {
                    string dbTestsBaseLocation = Path.Combine(FileSystemPathUtils.CommonApplicationData, "AutoVersionsDB.IntegrationTests", "TestsDBs");
                    if (!Directory.Exists(dbTestsBaseLocation))
                    {
                        Directory.CreateDirectory(dbTestsBaseLocation);
                    }

                    string filename = _dbBackupFilesProvider.GetDBBackupFilePath(dbBackupFileType, dbConnectionInfo.DBType);
                    dbBackupRestoreCommands.RestoreDBFromBackup(filename, dbConnection.DataBaseName, dbTestsBaseLocation);
                }
            }
        }


        public void CreateDBBackup(DBConnectionInfo dbConnectionInfo, string targetFilePath)
        {
            using (var dbConnection = dbCommandsFactoryProvider.CreateDBConnection(dbConnectionInfo))
            {
                using (var dbBackupRestoreCommands = dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(dbConnectionInfo))
                {
                    dbBackupRestoreCommands.CreateDBBackup(targetFilePath, dbConnectionInfo.DBName);
                }
            }
        }

        public void DropDB(DBConnectionInfo dbConnectionInfo)
        {
            using (var dbConnection = dbCommandsFactoryProvider.CreateDBConnection(dbConnectionInfo))
            {
                using (var dbBackupRestoreCommands = dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(dbConnectionInfo))
                {
                    dbBackupRestoreCommands.DropDB(dbConnectionInfo.DBName);
                }
            }
        }


        public NumOfDBConnections GetNumOfOpenConnection(DBConnectionInfo dbConnectionInfo)
        {
            NumOfDBConnections numOfConnectionsItem = new NumOfDBConnections();

            string masterDBName;
            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                masterDBName = dbCommands.DataBaseName;
            }


            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                numOfConnectionsItem.DBName = dbCommands.DataBaseName;
            }

            using (var dbQueryStatus = dbCommandsFactoryProvider.CreateDBQueryStatus(dbConnectionInfo))
            {
                numOfConnectionsItem.NumOfConnectionsToDB = dbQueryStatus.GetNumOfOpenConnection(numOfConnectionsItem.DBName);
                numOfConnectionsItem.NumOfConnectionsToAdminDB = dbQueryStatus.GetNumOfOpenConnection(masterDBName);
            }

            return numOfConnectionsItem;
        }



        public bool CheckIfTableExist(DBConnectionInfo dbConnectionInfo, string schemaName, string tableName)
        {
            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                return dbCommands.CheckIfTableExist(schemaName, tableName);
            }
        }

        public DataTable GetTable(DBConnectionInfo dbConnectionInfo, string tableName)
        {
            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                return dbCommands.GetTable(tableName);
            }
        }


        public bool CheckIfStoredProcedureExist(DBConnectionInfo dbConnectionInfo, string schemaName, string spName)
        {
            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                return dbCommands.CheckIfStoredProcedureExist(schemaName, spName);
            }
        }

        public DataTable GetAllDBSchemaExceptDBVersionSchema(DBConnectionInfo dbConnectionInfo)
        {
            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(dbConnectionInfo))
            {
                return dbCommands.GetAllDBSchemaExceptDBVersionSchema();
            }
        }


    }
}
