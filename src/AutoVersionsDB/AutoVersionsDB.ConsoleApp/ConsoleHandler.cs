using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoVersionsDB.ConsoleApp
{
    public class ConsoleHandler : IDisposable, IConsoleHandler
    {
        public static object ConsolWriteSync = new object();


        private int numberOfLineForLastMessage = 1;

        private ConsoleSpinner _spinner;

        public void StartSpiiner()
        {
            _spinner = new ConsoleSpinner();
        }
        public void StopSpinner()
        {
            _spinner.Dispose();
        }


        public void SetErrorMessage(string message)
        {
            TextWriter errorWriter = Console.Error;
            Console.ForegroundColor = ConsoleColor.Red;
            errorWriter.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SetErrorInstruction(string message)
        {
            TextWriter errorWriter = Console.Error;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            errorWriter.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteLineInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(message);
        }


        public void ProcessComplete(ProcessTrace processReults)
        {
            ClearConsoleLine(0);

            lock (ConsoleHandler.ConsolWriteSync)
            {

                if (processReults.HasError)
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("The process complete with errors:");
                    //Console.WriteLine("--------------------------------");
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    //Console.WriteLine(processReults.GetOnlyErrorsHistoryAsString());

                    //if (!string.IsNullOrWhiteSpace(processReults.InstructionsMessage))
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //    Console.WriteLine(processReults.InstructionsMessage);
                    //}

                    //Console.ForegroundColor = ConsoleColor.White;


                    SetErrorInstruction("The process complete with errors:");
                    SetErrorInstruction("--------------------------------");
                    SetErrorMessage(processReults.GetOnlyErrorsHistoryAsString());
                    
                    SetErrorInstruction(processReults.InstructionsMessage);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The process complete successfully");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }


        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            lock (ConsoleHandler.ConsolWriteSync)
            {
                ClearConsoleLine(3);

                int cursorTopStart = Console.CursorTop;

                Console.SetCursorPosition(3, Console.CursorTop);
                Console.Write(notificationStateItem.ToString());

                int cursorTopEnd = Console.CursorTop;

                numberOfLineForLastMessage = cursorTopEnd - cursorTopStart + 1;
            }
        }



        private void ClearConsoleLine(int cursorLeft)
        {
            Console.SetCursorPosition(cursorLeft, Console.CursorTop - (numberOfLineForLastMessage - 1));

            for (int i = 0; i < numberOfLineForLastMessage; i++)
            {
                Console.Write(new String(' ', Console.BufferWidth));
                if (i == 0 && cursorLeft > 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
                else
                {
                    Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                }
            }

            Console.SetCursorPosition(cursorLeft, Console.CursorTop - (numberOfLineForLastMessage));
        }




        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ConsoleHandler()
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
