using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using AutoVersionsDB.DbCommands.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.DbCommands.Integration
{
    public class DBCommandsFactory
    {
        private Dictionary<string, IDBTypeObjectsFactory> _DBTypeObjectsFactoryDictionary;

        public DBCommandsFactory()
        {
            PopulateFactoriesDictionary();
        }


        public List<DBType> GetDBTypes()
        {

            return _DBTypeObjectsFactoryDictionary
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
            return _DBTypeObjectsFactoryDictionary.ContainsKey(dbTypeCode);
        }


        public IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            IDBConnection dbConnection = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _DBTypeObjectsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBTypeObjectsFactory dbTypeObjectsFactory))
            {
                dbConnection = dbTypeObjectsFactory.CreateDBConnection(dbConnectionInfo);
            }

            return dbConnection;
        }
        public IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            IDBConnection dbConnection = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _DBTypeObjectsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBTypeObjectsFactory dbTypeObjectsFactory))
            {
                dbConnection = dbTypeObjectsFactory.CreateAdminDBConnection(dbConnectionInfo);
            }

            return dbConnection;
        }


        public DBCommands CreateDBCommand(DBConnectionInfo dbConnectionInfo)
        {
            DBCommands dbCommands = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _DBTypeObjectsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBTypeObjectsFactory dbTypeObjectsFactory))
            {
                IDBConnection dbConnection = dbTypeObjectsFactory.CreateDBConnection(dbConnectionInfo);
                IDBScriptsProvider dbScriptsProvider = dbTypeObjectsFactory.CreateDBScriptsProvider();

                dbCommands = new DBCommands(dbConnection, dbScriptsProvider);
            }

            return dbCommands;
        }

        public DBBackupRestoreCommands CreateDBBackupRestoreCommands(DBConnectionInfo dbConnectionInfo)
        {
            DBBackupRestoreCommands dbBackupRestoreCommands = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _DBTypeObjectsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBTypeObjectsFactory dbTypeObjectsFactory))
            {
                IDBConnection adminDBConnection = dbTypeObjectsFactory.CreateAdminDBConnection(dbConnectionInfo);
                IDBScriptsProvider dbScriptsProvider = dbTypeObjectsFactory.CreateDBScriptsProvider();

                dbBackupRestoreCommands = new DBBackupRestoreCommands(adminDBConnection, dbScriptsProvider);
            }

            return dbBackupRestoreCommands;
        }

        public DBQueryStatus CreateDBQueryStatus(DBConnectionInfo dbConnectionInfo)
        {
            DBQueryStatus dbQueryStatus = null;

            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.DBType)
                && _DBTypeObjectsFactoryDictionary.TryGetValue(dbConnectionInfo.DBType, out IDBTypeObjectsFactory dbTypeObjectsFactory))
            {
                IDBConnection adminDBConnection = dbTypeObjectsFactory.CreateAdminDBConnection(dbConnectionInfo);
                IDBScriptsProvider dbScriptsProvider = dbTypeObjectsFactory.CreateDBScriptsProvider();

                dbQueryStatus = new DBQueryStatus(adminDBConnection, dbScriptsProvider);
            }

            return dbQueryStatus;
        }


        public virtual DBProcessStatusNotifyerBase CreateDBProcessStatusNotifyer(Type notifyerType, DBConnectionInfo dbConnectionInfo)
        {
            DBQueryStatus dbQueryStatus = CreateDBQueryStatus(dbConnectionInfo);
            DBProcessStatusNotifyerBase dbNotifyer = Activator.CreateInstance(notifyerType, dbQueryStatus, DBCommandsConsts.DbLongProcessGetStatusIntervalInMs) as DBProcessStatusNotifyerBase;

            return dbNotifyer;
        }




        private void PopulateFactoriesDictionary()
        {
            _DBTypeObjectsFactoryDictionary = new Dictionary<string, IDBTypeObjectsFactory>();

            SqlServerDBTypeObjectsFactory sqlServerDBCommands_Factory = new SqlServerDBTypeObjectsFactory();
            _DBTypeObjectsFactoryDictionary.Add(sqlServerDBCommands_Factory.DBType.Code, sqlServerDBCommands_Factory);

        }



    }
}
