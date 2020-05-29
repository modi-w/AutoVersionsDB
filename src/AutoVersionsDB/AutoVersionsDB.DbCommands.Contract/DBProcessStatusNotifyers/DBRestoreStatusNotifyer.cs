

namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public class DBRestoreStatusNotifyer : DBProcessStatusNotifyerBase
    {
        private IDBQueryStatus _dbQueryStatus;

        public DBRestoreStatusNotifyer(IDBQueryStatus dbQueryStatus, int interval)
            : base(interval)
        {
            _dbQueryStatus = dbQueryStatus;
        }

        public override double GetStatusPrecents()
        {
            return _dbQueryStatus.GetRestoreProcessStatus();
        }


    }
}
