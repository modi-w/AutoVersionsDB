using System;

namespace AutoVersionsDB.UI
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }
        public MessageEventArgs(string message)
        {
            Message = message;
        }

    }
    public class ConfirmEventArgs : EventArgs
    {
        public string Message { get; set; }
        public bool IsConfirm { get; set; }

        public ConfirmEventArgs(string message)
        {
            Message = message;
        }

    }

    public static class UIGeneralEvents
    {
        public static event EventHandler<MessageEventArgs> OnException;
        public static event EventHandler<ConfirmEventArgs> OnConfirm;

        internal static void FireOnException(object sender, Exception ex)
        {
            if (OnException == null)
            {
                throw new Exception($"Bind method to 'OnException' event is mandatory");
            }

            OnException(sender, new MessageEventArgs(ex.ToString()));
        }

        internal static bool FireOnConfirm(object sender, string confirmMessage)
        {
            if (OnConfirm == null)
            {
                throw new Exception($"Bind method to 'OnConfirm' event is mandatory");
            }

            ConfirmEventArgs eventArgs = new ConfirmEventArgs(confirmMessage);

            OnConfirm(sender, eventArgs);

            return eventArgs.IsConfirm;
        }

    }
}
