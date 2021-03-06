﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoVersionsDB.DB.DBProcessStatusNotifyers
{
    public abstract class DBProcessStatusNotifyerBase
    {
        public int IntervalInMs { get; private set; }

        public bool IsActive { get; private set; }

        protected DBProcessStatusNotifyerBase(int intervalInMs)
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

            //Comment: we wait here because the run process is run on another thread
            Thread.Sleep(IntervalInMs * 2);
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
