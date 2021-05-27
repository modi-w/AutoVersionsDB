using System;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationProcessException : Exception
    {
        public string ErrorCode { get; private set; }
        public string InstructionsMessage { get; private set; }
        public NotificationErrorType NotificationErrorType { get; private set; }

        public NotificationProcessException()
        {
        }
        public NotificationProcessException(string message)
        : this(message, null)
        {
            NotificationErrorType = NotificationErrorType.Error;
        }

        public NotificationProcessException(string message, Exception innerException)
        : this(null, message, null, NotificationErrorType.Error, innerException)
        {
        }


        public NotificationProcessException(string errorCode, string message, string instructionsMessage, NotificationErrorType notificationErrorType)
         : this(errorCode, message, instructionsMessage, notificationErrorType, null)
        {
        }

        public NotificationProcessException(string errorCode, string message, string instructionsMessage, NotificationErrorType notificationErrorType, Exception ex)
            : base(message, ex)
        {
            ErrorCode = errorCode;
            InstructionsMessage = instructionsMessage;
            NotificationErrorType = notificationErrorType;
        }


    }
}
