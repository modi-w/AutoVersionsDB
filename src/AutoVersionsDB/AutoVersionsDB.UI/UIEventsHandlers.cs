using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public delegate void OnExceptionEventHandler(object sender, string exceptionMessage);
  
    public delegate bool OnConfirmEventHandler(object sender, string confirmMessage);

    public delegate void OnShowStatesLogEventHandler(object sender, StatesLogViewModel statesLogViewModel);



    public delegate TextInputResults OnTextInputEventHandler(object sender, string instructionMessageText);

    public class TextInputResults
    {
        public string ResultText { get; set; }
        public bool IsApply { get; set; }
    }
}
