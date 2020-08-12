using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoVersionsDB.DbCommands.Contract.DBProcessStatusNotifyers
{
    public abstract class DBProcessStatusNotifyerBase
    {
        public int IntervalInMs { get; private set; }

        public bool IsActive { get; private set; }

        public DBProcessStatusNotifyerBase(int intervalInMs)
        {

            IntervalInMs = intervalInMs;
        }


        public abstract double GetStatusPrecents();


        public void Start(Action<double> onProgress)
        {
            IsActive = true;

               Task.Run(() =>
                 {
                     try
                     {
                         Run(onProgress);
                     }
                     catch (ThreadAbortException threadAbortEx)
                     {
                         string exStr = threadAbortEx.ToString();
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



        private void Run(Action<double> onProgress)
        {
            Thread.Sleep(IntervalInMs);

            while (IsActive)
            {
                Thread.Sleep(IntervalInMs);

                double currentPrecentStatus = GetStatusPrecents();

                onProgress?.Invoke(currentPrecentStatus);
            }
        }
    }
}
