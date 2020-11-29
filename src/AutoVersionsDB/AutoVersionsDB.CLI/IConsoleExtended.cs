using System;
using System.CommandLine;

namespace AutoVersionsDB.CLI
{
    public interface IConsoleExtended : IConsole
    {
        int BufferHeight { get; set; }
        int BufferWidth { get; set; }
        int CursorTop { get; set; }
        int CursorLeft { get; set; }

        bool CursorVisible { get; set; }

        ConsoleColor ForegroundColor { get; set; }
        void SetCursorPosition(int left, int top);

    }
}
