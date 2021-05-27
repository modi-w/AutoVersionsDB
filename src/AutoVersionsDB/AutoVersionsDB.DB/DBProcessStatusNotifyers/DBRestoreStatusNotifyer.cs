using System;

namespace AutoVersionsDB.DB.DBProcessStatusNotifyers
{
    public class DBRestoreStatusNotifyer : DBProcessStatusNotifyerBase, IDisposable
    {
        private readonly DBQueryStatus _dbQueryStatus;

        public DBRestoreStatusNotifyer(DBQueryStatus dbQueryStatus, int interval)
            : base(interval)
        {
            _dbQueryStatus = dbQueryStatus;
        }

        public override double GetStatusPrecents()
        {
            return _dbQueryStatus.GetRestoreProcessStatus();
        }




        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBRestoreStatusNotifyer()
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
