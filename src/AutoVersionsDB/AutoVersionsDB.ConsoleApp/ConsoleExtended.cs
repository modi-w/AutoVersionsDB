using AutoVersionsDB.CLI;
using System;
using System.CommandLine.IO;

namespace AutoVersionsDB.ConsoleApp
{
    public class ConsoleExtended : IConsoleExtended
    {
        private readonly SystemConsole _systemConsole;

        public int BufferHeight
        {
            get => Console.BufferHeight;
            set => Console.BufferHeight = value;
        }
        public int BufferWidth
        {
            get => Console.BufferWidth;
            set => Console.BufferWidth = value;
        }
        public int CursorTop
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }
        public int CursorLeft
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }
        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }


        public IStandardStreamWriter Out => _systemConsole.Out;

        public bool IsOutputRedirected => _systemConsole.IsOutputRedirected;

        public IStandardStreamWriter Error => _systemConsole.Error;

        public bool IsErrorRedirected => _systemConsole.IsErrorRedirected;

        public bool IsInputRedirected => _systemConsole.IsInputRedirected;



        public ConsoleExtended(SystemConsole systemConsole)
        {
            _systemConsole = systemConsole;
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }



    }
}
