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
#pragma warning disable CA1822 // Mark members as static
        public DBProcessStatusNotifyerBase Create(Type notifyerType, DBQueryStatus dbQueryStatus)
#pragma warning restore CA1822 // Mark members as static
        {
            DBProcessStatusNotifyerBase dbNotifyer = Activator.CreateInstance(notifyerType, dbQueryStatus, DBCommandsConsts.DbLongProcessGetStatusIntervalInMs) as DBProcessStatusNotifyerBase;

            return dbNotifyer;
        }

    }
}
