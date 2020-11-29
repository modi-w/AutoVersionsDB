using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.UI
{
    public delegate void OnExceptionEventHandler(object sender, string exceptionMessage);

    public delegate bool OnConfirmEventHandler(object sender, string confirmMessage);

    public static class UIGeneralEvents
    {
        public static event OnExceptionEventHandler OnException;
        public static event OnConfirmEventHandler OnConfirm;

        internal static void FireOnException(object sender, Exception ex)
        {
            if (OnException == null)
            {
                throw new Exception($"Bind method to 'OnException' event is mandatory");
            }

            OnException(sender, ex.ToString());
        }

        internal static bool FireOnConfirm(object sender, string confirmMessage)
        {
            if (OnConfirm == null)
            {
                throw new Exception($"Bind method to 'OnConfirm' event is mandatory");
            }

            return OnConfirm(sender, confirmMessage);
        }

    }
}
