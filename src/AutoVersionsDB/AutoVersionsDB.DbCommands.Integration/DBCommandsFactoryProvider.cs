using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer;
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


        public List<DBType> GetDBTypes()
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


        public IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            IDBConnection dbConnection = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _dbCommandsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBCommandsFactory dbCommands_Factory))
            {
                dbConnection = dbCommands_Factory.CreateDBConnection(dbConnectionInfo);
            }

            return dbConnection;
        }
        public IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            IDBConnection dbConnection = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _dbCommandsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBCommandsFactory dbCommands_Factory))
            {
                dbConnection = dbCommands_Factory.CreateAdminDBConnection(dbConnectionInfo);
            }

            return dbConnection;
        }


        public IDBCommands CreateDBCommand(DBConnectionInfo dbConnectionInfo)
        {
            IDBCommands dbCommands = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _dbCommandsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBCommandsFactory dbCommands_Factory))
            {
                dbCommands = dbCommands_Factory.CreateDBCommands(dbConnectionInfo);
            }

            return dbCommands;
        }

        public IDBBackupRestoreCommands CreateDBBackupRestoreCommands(DBConnectionInfo dbConnectionInfo)
        {
            IDBBackupRestoreCommands dbBackupRestoreCommands = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _dbCommandsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBCommandsFactory dbCommands_Factory))
            {
                dbBackupRestoreCommands = dbCommands_Factory.CreateBackupRestoreCommands(dbConnectionInfo);
            }

            return dbBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(DBConnectionInfo dbConnectionInfo)
        {
            IDBQueryStatus dbQueryStatus = null;
       
            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _dbCommandsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBCommandsFactory dbCommands_Factory))
            {
                dbQueryStatus = dbCommands_Factory.CreateDBQueryStatus(dbConnectionInfo);
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
