using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoVersionsDB.ConsoleApp
{
    //https://stackoverflow.com/questions/888533/how-can-i-update-the-current-line-in-a-c-sharp-windows-console-app

    public  class ConsoleSpinner :IDisposable
    {
        private  int _counter;

        public int IntervalInMs { get; private set; }
        public bool IsActive { get; private set; }


        public ConsoleSpinner()
        {
            IntervalInMs = 100;

            Start();
        }

        public void Start()
        {
            IsActive = true;

            Task.Run(() =>
            {
                try
                {
                    Run();
                }
                catch (ThreadAbortException threadAbortEx)
                {
                    string exStr = threadAbortEx.ToString();
                    //Do Nothing - usually happand when the system closed before the sleep interval is over - like in Unit Tests
                }
                catch (Exception ex)
                {
                    throw new Exception("ConsoleSpinner. on run Thread", ex);
                }
            });

        }

        public void Stop()
        {
            IsActive = false;

            //Comment: we wait here because the run process is run on another thread
            Thread.Sleep(IntervalInMs * 10);

            lock (ConsoleHandler.ConsolWriteSync)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(" ");
            }
        }


        private void Run()
        {
            Thread.Sleep(IntervalInMs);

            while (IsActive)
            {
                Thread.Sleep(IntervalInMs);

                Turn();
            }
        }

        private  void Turn()
        {
            _counter++;

            lock (ConsoleHandler.ConsolWriteSync)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.SetCursorPosition(0, Console.CursorTop);
                switch (_counter % 4)
                {
                    case 0: Console.Write("/"); _counter = 0; break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsoleSpinner()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            //Comment: delete files is unmanage resource - so it is not in the disposing condition
            Stop();

        }

        #endregion

    }
}
