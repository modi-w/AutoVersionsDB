using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer;
using AutoVersionsDB.DbCommands.SqlServer.Utils;
using System.Collections.Generic;

namespace AutoVersionsDB.DbCommands.Integration
{
    public class DBCommands_FactoryProvider
    {
        public Dictionary<string, IDBCommands_Factory> DBCommands_FactoryDictionary { get; set; }

        public DBCommands_FactoryProvider()
        {
            populateFactoriesDictionary();
        }


        public IDBCommands_Factory GetDBCommandsFactoryByDBTypeCode(string dbTypeCode)
        {
            return DBCommands_FactoryDictionary[dbTypeCode];
        }

        public IDBConnectionManager CreateDBConnectionManager(string dbTypeCode, string connectionString, int timeout)
        {
            IDBConnectionManager dbConnectionManager = null;

            IDBCommands_Factory dbCommands_Factory;
            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommands_FactoryDictionary.TryGetValue(dbTypeCode, out dbCommands_Factory))
            {
                dbConnectionManager = dbCommands_Factory.CreateDBConnectionManager(connectionString, timeout);
            }

            return dbConnectionManager;
        }


        public IDBCommands CreateDBCommand(string dbTypeCode, string connectionString, int timeout)
        {
            IDBCommands dbCommands = null;

            IDBCommands_Factory dbCommands_Factory;
            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommands_FactoryDictionary.TryGetValue(dbTypeCode, out dbCommands_Factory))
            {
                dbCommands = dbCommands_Factory.CreateDBCommands(connectionString, timeout);
            }

            return dbCommands;
        }

        public IDBBackupRestoreCommands CreateDBBackupRestoreCommands(string dbTypeCode, string connectionString, int timeout)
        {
            IDBBackupRestoreCommands dbBackupRestoreCommands = null;

            IDBCommands_Factory dbCommands_Factory;
            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommands_FactoryDictionary.TryGetValue(dbTypeCode, out dbCommands_Factory))
            {
                dbBackupRestoreCommands = dbCommands_Factory.CreateBackupRestoreCommands(connectionString, timeout);
            }

            return dbBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string dbTypeCode, string connectionString)
        {
            IDBQueryStatus dbQueryStatus = null;

            IDBCommands_Factory dbCommands_Factory;
            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && DBCommands_FactoryDictionary.TryGetValue(dbTypeCode, out dbCommands_Factory))
            {
                dbQueryStatus = dbCommands_Factory.CreateDBQueryStatus(connectionString, 0);
            }

            return dbQueryStatus;
        }




        private void populateFactoriesDictionary()
        {
            DBCommands_FactoryDictionary = new Dictionary<string, IDBCommands_Factory>();

            EmbeddedResourcesManager embeddedResourcesManager = new EmbeddedResourcesManager();
            SqlServerDBCommands_Factory sqlServerDBCommands_Factory = new SqlServerDBCommands_Factory(embeddedResourcesManager);
            DBCommands_FactoryDictionary.Add(sqlServerDBCommands_Factory.DBTypeCode, sqlServerDBCommands_Factory);

        }



    }
}
