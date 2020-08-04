using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotifictionStateChangeHandler
    {


        private Action<ProcessStateResults, NotificationStateItem> _onNotificationStateChanged;


        public ProcessStateResults NotifictionStatesHistory { get; private set; }

        public NotificationStateItem RootNotificationStateItem { get; private set; }


        internal NotifictionStateChangeHandler(Action<ProcessStateResults, NotificationStateItem> onNotificationStateChanged)
        {
            _onNotificationStateChanged = onNotificationStateChanged;
        }

        internal void Reset(NotificationStateItem rootNotificationStateItem)
        {
            RootNotificationStateItem = rootNotificationStateItem;
            NotifictionStatesHistory = new ProcessStateResults();
            
            riseNotificationStateChanged();
        }




        internal void StepStart(NotificationStateItem notificationStateItem, string stepName, string additionalStepInfo, bool hasInternalStep)
        {
            notificationStateItem.StepName = stepName;

            if (!string.IsNullOrWhiteSpace(additionalStepInfo))
            {
                notificationStateItem.StepName = $"{notificationStateItem.StepName} - {additionalStepInfo}";
            }

            notificationStateItem.InternalNotificationStateItem = null;

            if (!hasInternalStep)
            {
                notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

                riseNotificationStateChanged();
            }
        }

        internal void StepEnd(NotificationStateItem notificationStateItem, bool hasInternalStep)
        {
            notificationStateItem.StepNumber++;

            if (!hasInternalStep)
            {
                if (notificationStateItem.IsPrecentsAboveMin)
                {
                    notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

                    riseNotificationStateChanged();
                }
            }
        }

        internal void ForceStepProgress(NotificationStateItem notificationStateItem, int forceSecondaryProcessStepNumber)
        {
            notificationStateItem.StepNumber = forceSecondaryProcessStepNumber;

            if (notificationStateItem.IsPrecentsAboveMin)
            {
                notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

                riseNotificationStateChanged();
            }
        }


        internal void StepError(NotificationStateItem notificationStateItem, string errorCode, string errorMessage, string instructionsMessage)
        {
            notificationStateItem.ErrorCode = errorCode;
            notificationStateItem.ErrorMesage = errorMessage;
            notificationStateItem.InstructionsMessage = instructionsMessage;

            riseNotificationStateChanged();
        }



        private void riseNotificationStateChanged()
        {
            NotificationStateItem snapshotNotificationState = RootNotificationStateItem.Clone();
            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            lock (NotifictionStatesHistory)
            {
                NotifictionStatesHistory.Appand(snapshotNotificationState);
            }

            Task.Run(() =>
            {
                _onNotificationStateChanged?.Invoke(NotifictionStatesHistory, snapshotNotificationState);
            });
        }
    }
}
