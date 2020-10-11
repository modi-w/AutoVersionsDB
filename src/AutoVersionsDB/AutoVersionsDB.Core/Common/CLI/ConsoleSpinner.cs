using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.Common.CLI
{
    //https://stackoverflow.com/questions/888533/how-can-i-update-the-current-line-in-a-c-sharp-windows-console-app

    public class ConsoleSpinner
    {
        private readonly IConsoleExtended _console;

        private int _counter;

        public int IntervalInMs { get; private set; }
        public bool IsActive { get; private set; }


        public ConsoleSpinner(IConsoleExtended console)
        {
            _console = console;

            IntervalInMs = 100;
        }

        public void Start()
        {
            IsActive = true;

            lock (ConsoleProcessMessages.ConsolWriteSync)
            {
                _console.CursorVisible = false;
            }

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

            lock (ConsoleProcessMessages.ConsolWriteSync)
            {
                _console.SetCursorPosition(0, _console.CursorTop);
                _console.Out.Write(" ");

                _console.CursorVisible = true;
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

        private void Turn()
        {
            _counter++;

            lock (ConsoleProcessMessages.ConsolWriteSync)
            {
                _console.ForegroundColor = ConsoleColor.DarkGray;

                _console.SetCursorPosition(0, _console.CursorTop);
                switch (_counter % 4)
                {
                    case 0: _console.Out.Write("/"); _counter = 0; break;
                    case 1: _console.Out.Write("-"); break;
                    case 2: _console.Out.Write("\\"); break;
                    case 3: _console.Out.Write("|"); break;
                }

                _console.ForegroundColor = ConsoleColor.White;
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
