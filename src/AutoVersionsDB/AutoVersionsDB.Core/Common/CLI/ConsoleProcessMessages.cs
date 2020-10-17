using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.IO;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoVersionsDB.Core.Common.CLI
{
    public class ConsoleProcessMessages : IConsoleProcessMessages
    {
        private IConsoleExtended _console;
        private ConsoleSpinner _spinner;



        private int numberOfLineForLastMessage = 1;

        //public ConsoleProcessMessages() { }
        public ConsoleProcessMessages(IConsoleExtended console,
                                        ConsoleSpinner spinner)
        {
            _console = console;
            _spinner = spinner;
        }

        public void StartSpiiner()
        {
            _spinner.Start();
        }
        public void StopSpinner()
        {
            _spinner.Dispose();
        }


        public void SetErrorMessage(string message)
        {
            Environment.ExitCode = CLIConsts.ExistErrorCode;

            IStandardStreamWriter errorWriter = _console.Error;
            _console.ForegroundColor = ConsoleColor.Red;
            errorWriter.WriteLine(message);

            _console.ForegroundColor = ConsoleColor.White;
        }

        public void SetErrorInstruction(string message)
        {
            IStandardStreamWriter errorWriter = _console.Error;
            _console.ForegroundColor = ConsoleColor.DarkRed;
            errorWriter.WriteLine(message);

            _console.ForegroundColor = ConsoleColor.White;
        }

        public void SetInfoMessage(string message)
        {
            _console.ForegroundColor = ConsoleColor.Gray;

            _console.Out.WriteLine(message);

            _console.ForegroundColor = ConsoleColor.White;
        }

        public void SetInlineInfoMessage(string message, ConsoleColor color)
        {
            _console.ForegroundColor = color;

            _console.Out.Write(message);

            _console.ForegroundColor = ConsoleColor.White;
        }

        public void StartProcessMessage(string processName)
        {
            StartProcessMessage(processName, "");
        }
        public void StartProcessMessage(string processName, string paramsStr)
        {
            _console.ForegroundColor = ConsoleColor.Gray;

            if (!string.IsNullOrWhiteSpace(paramsStr))
            {
                _console.Out.WriteLine($"> Run '{processName}' for '{paramsStr}'");
            }
            else
            {
                _console.Out.WriteLine($"> Run '{processName}' (no params)");
            }

        }


        

        public void ProcessComplete(ProcessResults processReults)
        {
            ClearConsoleLine(0);

            lock (CLIConsts.ConsolWriteSync)
            {

                if (processReults.Trace.HasError)
                {
                    SetErrorInstruction("The process complete with errors:");
                    SetErrorInstruction("--------------------------------");
                    SetErrorMessage(processReults.Trace.GetOnlyErrorsHistoryAsString());

                    SetErrorInstruction(processReults.Trace.InstructionsMessage);

                }
                else
                {
                    _console.ForegroundColor = ConsoleColor.Green;
                    _console.Out.WriteLine("The process complete successfully");
                    _console.ForegroundColor = ConsoleColor.White;
                }

                _console.ForegroundColor = ConsoleColor.White;
            }

        }

    
        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            lock (CLIConsts.ConsolWriteSync)
            {
                ClearConsoleLine(3);

                int cursorTopStart = _console.CursorTop;

                _console.ForegroundColor = ConsoleColor.DarkGray;

                _console.SetCursorPosition(3, _console.CursorTop);
                _console.Out.Write(notificationStateItem.ToString());

                int cursorTopEnd = _console.CursorTop;

                numberOfLineForLastMessage = cursorTopEnd - cursorTopStart + 1;

                _console.ForegroundColor = ConsoleColor.White;
            }
        }



        private void ClearConsoleLine(int cursorLeft)
        {
            _console.SetCursorPosition(cursorLeft, _console.CursorTop - (numberOfLineForLastMessage - 1));

            for (int i = 0; i < numberOfLineForLastMessage; i++)
            {
                _console.Out.Write(new string(' ', _console.BufferWidth));
                if (i == 0 && cursorLeft > 0)
                {
                    _console.SetCursorPosition(0, _console.CursorTop);
                }
                else
                {
                    _console.SetCursorPosition(cursorLeft, _console.CursorTop + 1);
                }
            }

            _console.SetCursorPosition(cursorLeft, _console.CursorTop - numberOfLineForLastMessage);
        }




        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsoleProcessMessages()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_spinner != null)
                {
                    _spinner.Dispose();
                }
            }

            //Comment: delete files is unmanage resource - so it is not in the disposing condition

        }

        #endregion

    }
}
