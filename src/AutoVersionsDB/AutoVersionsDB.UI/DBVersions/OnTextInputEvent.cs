using System;

namespace AutoVersionsDB.UI.DBVersions
{
    public class TextInputResults
    {
        public string ResultText { get; set; }
        public bool IsApply { get; set; }
    }

    public class OnTextInputEventsEventArgs : EventArgs
    {
        public string InstructionMessageText { get; }

        public TextInputResults Results { get; set; }

        public OnTextInputEventsEventArgs(string instructionMessageText)
        {
            InstructionMessageText = instructionMessageText;
        }

    }

}
