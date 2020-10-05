using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.Common.CLI
{
    public interface IConsoleExtended : IConsole
    {
        int BufferHeight { get; set; }
        int BufferWidth { get; set; }
        int CursorTop { get; set; }
        int CursorLeft { get; set; }
        ConsoleColor ForegroundColor { get; set; }
        void SetCursorPosition(int left, int top);

    }
}
