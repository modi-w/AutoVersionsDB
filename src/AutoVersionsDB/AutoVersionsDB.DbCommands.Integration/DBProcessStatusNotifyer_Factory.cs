using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using System;


namespace AutoVersionsDB.DbCommands.Integration
{
    /// <summary>
    /// We need that the DBCommand will be with diffrent instance because this should run in prallel to another db process
    /// </summary>
    public class DBProcessStatusNotifyer_Factory
    {
        private DBCommands_FactoryProvider _dbCommands_FactoryProvider;


        public DBProcessStatusNotifyer_Factory(DBCommands_FactoryProvider dbCommands_FactoryProvider)
        {
            _dbCommands_FactoryProvider = dbCommands_FactoryProvider;
        }

        public DBProcessStatusNotifyerBase Create(Type notifyerType, IDBQueryStatus dbQueryStatus)
        {
            DBProcessStatusNotifyerBase dbNotifyer = Activator.CreateInstance(notifyerType, dbQueryStatus, DBCommandsConsts.C_DBLongProcessGetStatusIntervalInMs) as DBProcessStatusNotifyerBase;

            return dbNotifyer;
        }

    }
}
