using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    //public delegate void OnNotificationStateItemChangedEventHandler(NotificationStateItem notificationStateItem);

    public class NotifictionStatesHistory
    {
        public List<NotificationStateItem> NotificationStatesProcessHistory { get; private set; }

        public NotificationStateItem RootNotificationStateItem { get; private set; }


        private Action<NotificationStateItem> _onNotificationStateChanged;

       // public event OnNotificationStateItemChangedEventHandler OnNotificationStateItemChanged;



        public bool HasError
        {
            get
            {
                bool outVal;

                lock (NotificationStatesProcessHistory)
                {
                    outVal = NotificationStatesProcessHistory.Any(e => e.HasError);
                }

                return outVal;
            }
        }


        public string ErrorCode
        {
            get
            {
                string outStr = "";

                lock (NotificationStatesProcessHistory)
                {
                    var lastStateWithErrorCode = NotificationStatesProcessHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelErrorCode));
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

                lock (NotificationStatesProcessHistory)
                {
                    var lastStateWithInstructionsMessage = NotificationStatesProcessHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
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

                lock (NotificationStatesProcessHistory)
                {
                    NotificationStateItem lastStateWithInstructionsMessage = NotificationStatesProcessHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));

                    outStr = lastStateWithInstructionsMessage.LowLevelStepName;
                }

                return outStr;
            }
        }


        public NotifictionStatesHistory(Action<NotificationStateItem> onNotificationStateChanged)
        {
            _onNotificationStateChanged = onNotificationStateChanged;
        }

        public void Reset(NotificationStateItem rootNotificationStateItem)
        {
            RootNotificationStateItem = rootNotificationStateItem;
            NotificationStatesProcessHistory = new List<NotificationStateItem>();
        }



        public void HandleNotificationStateChanged()
        {
            NotificationStateItem snapshotNotificationState = RootNotificationStateItem.Clone();
            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            lock (NotificationStatesProcessHistory)
            {
                NotificationStatesProcessHistory.Add(snapshotNotificationState);
            }

            Task.Run(() =>
            {
                _onNotificationStateChanged?.Invoke(snapshotNotificationState);
            });
        }









        public string GetAllHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();

            lock (NotificationStatesProcessHistory)
            {
                foreach (var notificationState in NotificationStatesProcessHistory)
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }

            return sbStrResults.ToString();
        }

        public string GetOnlyErrorsHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();


            lock (NotificationStatesProcessHistory)
            {
                foreach (var notificationState in NotificationStatesProcessHistory.Where(e => e.HasError))
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }


            return sbStrResults.ToString();
        }


    }
}
