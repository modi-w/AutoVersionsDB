
namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public class DBBackupStatusNotifyer : DBProcessStatusNotifyerBase
    {
        private readonly DBQueryStatus _dbQueryStatus;

        public DBBackupStatusNotifyer(DBQueryStatus dbQueryStatus, int interval)
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
