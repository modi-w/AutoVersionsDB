using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTrace
    {

        private readonly List<NotificationStateItem> _statesHistory;

        public List<NotificationStateItem> StatesHistory
        {
            get
            {
                return _statesHistory.ToList();
            }
        }


        public bool HasError
        {
            get
            {
                bool outVal;

                lock (_statesHistory)
                {
                    outVal = _statesHistory.Any(e => e.HasError);
                }

                return outVal;
            }
        }


        public string ErrorCode
        {
            get
            {
                string outStr = "";

                lock (_statesHistory)
                {
                    var lastStateWithErrorCode = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelErrorCode));
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

                lock (_statesHistory)
                {
                    var lastStateWithInstructionsMessage = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
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

                lock (_statesHistory)
                {
                    NotificationStateItem lastStateWithInstructionsMessage = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));

                    outStr = lastStateWithInstructionsMessage.LowLevelStepName;
                }

                return outStr;
            }
        }


        internal ProcessTrace()
        {

            _statesHistory = new List<NotificationStateItem>();
        }


        internal void Appand(NotificationStateItem notificationStateItem)
        {
            lock (_statesHistory)
            {
                _statesHistory.Add(notificationStateItem);
            }
        }


        public string GetAllHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();

            lock (_statesHistory)
            {
                foreach (var notificationState in _statesHistory.ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }

            return sbStrResults.ToString();
        }

        public string GetOnlyErrorsHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();


            lock (_statesHistory)
            {
                foreach (var notificationState in _statesHistory.Where(e => e.HasError).ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }


            return sbStrResults.ToString();
        }


    }
}
