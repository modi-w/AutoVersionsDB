//using AutoVersionsDB.DbCommands.Contract;
//using AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers;
//using System;


//namespace AutoVersionsDB.DbCommands.Integration
//{
//    /// <summary>
//    /// We need that the DBCommand will be with diffrent instance because this should run in prallel to another db process
//    /// </summary>
//    public class DBProcessStatusNotifyerFactory
//    {
//        public virtual DBProcessStatusNotifyerBase Create(Type notifyerType, DBQueryStatus dbQueryStatus)
//        {
//            DBProcessStatusNotifyerBase dbNotifyer = Activator.CreateInstance(notifyerType, dbQueryStatus, DBCommandsConsts.DbLongProcessGetStatusIntervalInMs) as DBProcessStatusNotifyerBase;

//            return dbNotifyer;
//        }

//    }
//}
