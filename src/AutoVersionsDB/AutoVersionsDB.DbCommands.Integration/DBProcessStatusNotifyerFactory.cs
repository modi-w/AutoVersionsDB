using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
using System;


namespace AutoVersionsDB.DbCommands.Integration
{
    /// <summary>
    /// We need that the DBCommand will be with diffrent instance because this should run in prallel to another db process
    /// </summary>
    public class DBProcessStatusNotifyerFactory
    {

        public DBProcessStatusNotifyerFactory()
        {
        }

        public DBProcessStatusNotifyerBase Create(Type notifyerType, IDBQueryStatus dbQueryStatus)
        {
            DBProcessStatusNotifyerBase dbNotifyer = Activator.CreateInstance(notifyerType, dbQueryStatus, DBCommandsConsts.C_DBLongProcessGetStatusIntervalInMs) as DBProcessStatusNotifyerBase;

            return dbNotifyer;
        }

    }
}
