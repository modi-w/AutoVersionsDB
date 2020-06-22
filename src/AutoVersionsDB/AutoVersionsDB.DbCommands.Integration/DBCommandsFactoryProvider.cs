using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer;
using AutoVersionsDB.DbCommands.SqlServer.Utils;
using System.Collections.Generic;

namespace AutoVersionsDB.DbCommands.Integration
{
    public class DBCommandsFactoryProvider
    {
        public Dictionary<string, IDBCommandsFactory> DBCommandsFactoryDictionary { get; private set; }

        public DBCommandsFactoryProvider()
        {
            populateFactoriesDictionary();
        }


        public IDBCommandsFactory GetDBCommandsFactoryByDBTypeCode(string dbTypeCode)
        {
            return DBCommandsFactoryDictionary[dbTypeCode];
        }

        public IDBConnectionManager CreateDBConnectionManager(string dbTypeCode, string connectionString, int timeout)
        {
            IDBConnectionManager dbConnectionManager = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbConnectionManager = dbCommands_Factory.CreateDBConnectionManager(connectionString, timeout);
            }

            return dbConnectionManager;
        }


        public IDBCommands CreateDBCommand(string dbTypeCode, string connectionString, int timeout)
        {
            IDBCommands dbCommands = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbCommands = dbCommands_Factory.CreateDBCommands(connectionString, timeout);
            }

            return dbCommands;
        }

        public IDBBackupRestoreCommands CreateDBBackupRestoreCommands(string dbTypeCode, string connectionString, int timeout)
        {
            IDBBackupRestoreCommands dbBackupRestoreCommands = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbBackupRestoreCommands = dbCommands_Factory.CreateBackupRestoreCommands(connectionString, timeout);
            }

            return dbBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string dbTypeCode, string connectionString)
        {
            IDBQueryStatus dbQueryStatus = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbQueryStatus = dbCommands_Factory.CreateDBQueryStatus(connectionString, 0);
            }

            return dbQueryStatus;
        }




        private void populateFactoriesDictionary()
        {
            DBCommandsFactoryDictionary = new Dictionary<string, IDBCommandsFactory>();

            EmbeddedResourcesManager embeddedResourcesManager = new EmbeddedResourcesManager();
            SqlServerDBCommandsFactory sqlServerDBCommands_Factory = new SqlServerDBCommandsFactory(embeddedResourcesManager);
            DBCommandsFactoryDictionary.Add(sqlServerDBCommands_Factory.DBTypeCode, sqlServerDBCommands_Factory);

        }



    }
}
