using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationProcessException : Exception
    {
        public string ErrorCode { get; private set; }
        public string InstructionsMessage { get; private set; }


        public NotificationProcessException()
        {
        }
        public NotificationProcessException(string message) : base(message)
        {
        }
        public NotificationProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public NotificationProcessException(string errorCode, string message, string instructionsMessage)
         : base(message)
        {
            ErrorCode = errorCode;
            InstructionsMessage = instructionsMessage;
        }

        public NotificationProcessException(string errorCode, string message, string instructionsMessage, Exception ex)
            :base(message, ex)
        {
            ErrorCode = errorCode;
            InstructionsMessage = instructionsMessage;
        }


    }
}
