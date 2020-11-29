
using System;

namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public class DBBackupStatusNotifyer : DBProcessStatusNotifyerBase, IDisposable
    {
        private readonly DBQueryStatus _dbQueryStatus;

        public DBBackupStatusNotifyer(DBQueryStatus dbQueryStatus, int interval)
            : base(interval)
        {
            _dbQueryStatus = dbQueryStatus;
        }

        public override double GetStatusPrecents()
        {
            return _dbQueryStatus.GetBackupProcessStatus();
        }



        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBBackupStatusNotifyer()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_dbQueryStatus != null)
                {
                    _dbQueryStatus.Dispose();
                }

            }
            // free native resources here if there are any
        }

        #endregion


    }
}
