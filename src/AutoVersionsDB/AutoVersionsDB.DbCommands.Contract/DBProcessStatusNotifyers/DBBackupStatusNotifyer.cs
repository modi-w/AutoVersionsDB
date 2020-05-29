
namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public class DBBackupStatusNotifyer : DBProcessStatusNotifyerBase
    {
        private IDBQueryStatus _dbQueryStatus;

        public DBBackupStatusNotifyer(IDBQueryStatus dbQueryStatus, int interval)
            :base(interval)
        {
            _dbQueryStatus = dbQueryStatus;
        }

        public override double GetStatusPrecents()
        {
            return _dbQueryStatus.GetBackupProcessStatus();
        }




    }
}
