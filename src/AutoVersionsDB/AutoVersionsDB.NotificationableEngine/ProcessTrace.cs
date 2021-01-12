using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTrace
    {
        private readonly IList<StepNotificationState> _statesLog;
        public IList<StepNotificationState> StatesLog => _statesLog.ToList();

        public bool HasError
        {
            get
            {
                bool outVal;

                lock (_statesLog)
                {
                    outVal = _statesLog.Any(e => e.HasError);
                }

                return outVal;
            }
        }


        //public string ErrorCode
        //{
        //    get
        //    {
        //        string outStr = "";

        //        lock (_statesHistory)
        //        {
        //            var lastStateWithErrorCode = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelErrorCode));
        //            if (lastStateWithErrorCode != null)
        //            {
        //                outStr = lastStateWithErrorCode.LowLevelErrorCode;
        //            }
        //        }

        //        return outStr;
        //    }
        //}

        public bool ContainErrorCode(string errorCode)
        {
            return _statesLog.Any(e => e.LowLevelErrorCode == errorCode);
        }


        public string InstructionsMessage
        {
            get
            {
                string outStr = "";

                lock (_statesLog)
                {
                    var lastStateWithInstructionsMessage = _statesLog.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
                    if (lastStateWithInstructionsMessage != null)
                    {
                        outStr = lastStateWithInstructionsMessage.LowLevelInstructionsMessage;
                    }
                }

                return outStr;
            }
        }

        public NotificationErrorType NotificationErrorType
        {
            get
            {
                NotificationErrorType outNotificationErrorType =  NotificationErrorType.None;

                lock (_statesLog)
                {
                    var lastStateWithInstructionsMessage = _statesLog.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
                    if (lastStateWithInstructionsMessage != null)
                    {
                        outNotificationErrorType = lastStateWithInstructionsMessage.LowLevelNotificationErrorType;
                    }
                }

                return outNotificationErrorType;
            }
        }



        public string InstructionsMessageStepName
        {
            get
            {
                string outStr = "";

                lock (_statesLog)
                {
                    StepNotificationState lastStateWithInstructionsMessage = _statesLog.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));

                    outStr = lastStateWithInstructionsMessage.LowLevelStepName;
                }

                return outStr;
            }
        }



        internal ProcessTrace()
        {
            _statesLog = new List<StepNotificationState>();
        }


        internal void Appand(StepNotificationState notificationStateItem)
        {
            lock (_statesLog)
            {
                _statesLog.Add(notificationStateItem);
            }
        }






        public string GetAllStatesLogAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();

            lock (_statesLog)
            {
                foreach (var notificationState in _statesLog.ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }

            return sbStrResults.ToString();
        }

        public string GetOnlyErrorsStatesLogAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();


            lock (_statesLog)
            {
                // var notificationState = _statesHistory.Where(e => e.HasError).FirstOrDefault();

                //if (notificationState != null)
                //{
                //    sbStrResults.AppendLine(notificationState.ToString(false, false));
                //}

                foreach (var notificationState in _statesLog.Where(e => e.HasError).GroupBy(e => e.LowLevelErrorCode).Select(e => e.First()))
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }


            return sbStrResults.ToString();
        }


    }
}
