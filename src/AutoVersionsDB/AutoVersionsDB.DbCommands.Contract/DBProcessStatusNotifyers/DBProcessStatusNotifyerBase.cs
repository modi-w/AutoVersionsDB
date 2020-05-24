using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public delegate void OnDBProcessStatusEventHandler(double precent);

    public abstract class DBProcessStatusNotifyerBase
    {
        public int IntervalInMs { get; private set; }

        public bool IsActive { get; private set; }

        public event OnDBProcessStatusEventHandler OnDBProcessStatus;

        public DBProcessStatusNotifyerBase(int intervalInMs)
        {

            IntervalInMs = intervalInMs;
        }


        public abstract double GetStatusPrecents();


        public void Start()
        {
            IsActive = true;

               Task.Factory.StartNew(() =>
                 {
                     try
                     {
                         run();
                     }
                     catch (ThreadAbortException threadAbortEx)
                     {
                         //Do Nothing - usually happand when the system closed before the sleep interval is over - like in Unit Tests
                     }
                     catch (Exception ex)
                     {
                         throw new Exception("DBProcessStatusNotifyerBase. on run Thread", ex);
                     }
                 });
         
        }

        public void Stop()
        {
            IsActive = false;
        }



        private void run()
        {
            Thread.Sleep(IntervalInMs);

            while (IsActive)
            {
                Thread.Sleep(IntervalInMs);

                double currentPrecentStatus = GetStatusPrecents();

                OnDBProcessStatus?.Invoke(currentPrecentStatus);
            }
        }
    }
}
