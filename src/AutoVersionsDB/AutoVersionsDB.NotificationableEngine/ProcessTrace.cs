using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTrace
    {

        public List<NotificationStateItem> StatesHistory { get; }


        public bool HasError
        {
            get
            {
                bool outVal;

                lock (StatesHistory)
                {
                    outVal = StatesHistory.Any(e => e.HasError);
                }

                return outVal;
            }
        }


        public string ErrorCode
        {
            get
            {
                string outStr = "";

                lock (StatesHistory)
                {
                    var lastStateWithErrorCode = StatesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelErrorCode));
                    if (lastStateWithErrorCode != null)
                    {
                        outStr = lastStateWithErrorCode.LowLevelErrorCode;
                    }
                }

                return outStr;
            }
        }


        public string InstructionsMessage
        {
            get
            {
                string outStr = "";

                lock (StatesHistory)
                {
                    var lastStateWithInstructionsMessage = StatesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
                    if (lastStateWithInstructionsMessage != null)
                    {
                        outStr = lastStateWithInstructionsMessage.LowLevelInstructionsMessage;
                    }
                }

                return outStr;
            }
        }

        public string InstructionsMessageStepName
        {
            get
            {
                string outStr = "";

                lock (StatesHistory)
                {
                    NotificationStateItem lastStateWithInstructionsMessage = StatesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));

                    outStr = lastStateWithInstructionsMessage.LowLevelStepName;
                }

                return outStr;
            }
        }


        internal ProcessTrace() {

            StatesHistory = new List<NotificationStateItem>();
        }


        internal void Appand(NotificationStateItem notificationStateItem)
        {
            lock (StatesHistory)
            {
                StatesHistory.Add(notificationStateItem);
            }
        }


        public string GetAllHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();

            lock (StatesHistory)
            {
                foreach (var notificationState in StatesHistory.ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }

            return sbStrResults.ToString();
        }

        public string GetOnlyErrorsHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();


            lock (StatesHistory)
            {
                foreach (var notificationState in StatesHistory.Where(e => e.HasError).ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }


            return sbStrResults.ToString();
        }


    }
}
