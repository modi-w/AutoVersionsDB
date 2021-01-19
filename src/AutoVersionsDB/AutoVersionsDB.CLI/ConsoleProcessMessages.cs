using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.CommandLine.IO;

namespace AutoVersionsDB.CLI
{
    public class ConsoleProcessMessages : IConsoleProcessMessages
    {
        private readonly IConsoleExtended _console;
        private readonly ConsoleSpinner _spinner;



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

        public void SetErrorInstruction(string message, NotificationErrorType notificationErrorType)
        {

            IStandardStreamWriter errorWriter = _console.Error;

            switch (notificationErrorType)
            {
                case NotificationErrorType.Error:

                    Environment.ExitCode = CLIConsts.ExistErrorCode;

                    _console.ForegroundColor = ConsoleColor.DarkRed;
                    break;

                case NotificationErrorType.Attention:

                    _console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;

                case NotificationErrorType.None:
                default:
                    throw new Exception($"Invalid NotificationErrorType '{notificationErrorType}'");
            }

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
        public void StartProcessMessage(string processName, string args)
        {
            _console.ForegroundColor = ConsoleColor.Gray;

            if (!string.IsNullOrWhiteSpace(args))
            {
                _console.Out.WriteLine(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", processName).Replace("[args]", args));
            }
            else
            {
                _console.Out.WriteLine(CLITextResources.StartProcessMessageNoArgs.Replace("[processName]", processName));
            }

        }




        public void ProcessComplete(ProcessResults processReults)
        {
            processReults.ThrowIfNull(nameof(processReults));

            ClearConsoleLine(0);

            lock (CLIConsts.ConsolWriteSync)
            {

                if (processReults.Trace.HasError)
                {
                    if (processReults.Trace.NotificationErrorType == NotificationErrorType.Attention)
                    {
                        SetErrorInstruction(processReults.Trace.InstructionsMessage, processReults.Trace.NotificationErrorType);
                    }
                    else
                    {
                        SetErrorInstruction(CLITextResources.ProcessCompleteWithErrors, NotificationErrorType.Error);
                        SetErrorInstruction("--------------------------------", NotificationErrorType.Error);
                        SetErrorMessage(processReults.Trace.GetOnlyErrorsStatesLogAsString());
                        SetErrorInstruction(processReults.Trace.InstructionsMessage, processReults.Trace.NotificationErrorType);
                    }


                }
                else
                {
                    _console.ForegroundColor = ConsoleColor.Green;
                    _console.Out.WriteLine(CLITextResources.ProcessCompleteSuccessfully);
                    _console.ForegroundColor = ConsoleColor.White;
                }

                _console.ForegroundColor = ConsoleColor.White;
            }

        }


        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            notificationStateItem.ThrowIfNull(nameof(notificationStateItem));

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
