using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer;
using AutoVersionsDB.DbCommands.SqlServer.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.DbCommands.Integration
{
    public class DBCommandsFactoryProvider
    {
        private Dictionary<string, IDBCommandsFactory> _dbCommandsFactoryDictionary;

        public DBCommandsFactoryProvider()
        {
            PopulateFactoriesDictionary();
        }


        public List<DBType> GetDbTypesList()
        {

            return _dbCommandsFactoryDictionary
                .Values
                .Select(e => e.DBType)
                .ToList();
        }

        //public IDBCommandsFactory GetDBCommandsFactoryByDBTypeCode(string dbTypeCode)
        //{
        //    return _dbCommandsFactoryDictionary[dbTypeCode];
        //}

        public bool ContainDbType(string dbTypeCode)
        {
            return _dbCommandsFactoryDictionary.ContainsKey(dbTypeCode);
        }


        public IDBConnectionManager CreateDBConnectionManager(string dbTypeCode, string connectionString, int timeout)
        {
            IDBConnectionManager dbConnectionManager = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && _dbCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbConnectionManager = dbCommands_Factory.CreateDBConnectionManager(connectionString, timeout);
            }

            return dbConnectionManager;
        }


        public IDBCommands CreateDBCommand(string dbTypeCode, string connectionString, int timeout)
        {
            IDBCommands dbCommands = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && _dbCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbCommands = dbCommands_Factory.CreateDBCommands(connectionString, timeout);
            }

            return dbCommands;
        }

        public IDBBackupRestoreCommands CreateDBBackupRestoreCommands(string dbTypeCode, string connectionString, int timeout)
        {
            IDBBackupRestoreCommands dbBackupRestoreCommands = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && _dbCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbBackupRestoreCommands = dbCommands_Factory.CreateBackupRestoreCommands(connectionString, timeout);
            }

            return dbBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string dbTypeCode, string connectionString)
        {
            IDBQueryStatus dbQueryStatus = null;

            if (!string.IsNullOrWhiteSpace(dbTypeCode)
                && _dbCommandsFactoryDictionary.TryGetValue(dbTypeCode, out IDBCommandsFactory dbCommands_Factory))
            {
                dbQueryStatus = dbCommands_Factory.CreateDBQueryStatus(connectionString, 0);
            }

            return dbQueryStatus;
        }




        private void PopulateFactoriesDictionary()
        {
            _dbCommandsFactoryDictionary = new Dictionary<string, IDBCommandsFactory>();

            SqlServerDBCommandsFactory sqlServerDBCommands_Factory = new SqlServerDBCommandsFactory();
            _dbCommandsFactoryDictionary.Add(sqlServerDBCommands_Factory.DBType.Code, sqlServerDBCommands_Factory);

        }



    }
}
