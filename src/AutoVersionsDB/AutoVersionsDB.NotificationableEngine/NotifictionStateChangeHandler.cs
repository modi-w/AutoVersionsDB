using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotifictionStateChangeHandler
    {

        private readonly Action<ProcessTrace, NotificationStateItem> _onNotificationStateChanged;


        public ProcessTrace ProcessTrace { get; private set; }

        public NotificationStateItem RootNotificationStateItem { get; private set; }

        public NotificationStateItem CurrentNotificationStateItem
        {
            get
            {
                NotificationStateItem currentParentNotificationStateItem = null;
                NotificationStateItem nextNotificationStateItem = this.RootNotificationStateItem;

                while (nextNotificationStateItem != null)
                {
                    currentParentNotificationStateItem = nextNotificationStateItem;
                    nextNotificationStateItem = nextNotificationStateItem.InternalNotificationStateItem;
                }

                return currentParentNotificationStateItem;
            }

        }





        internal NotifictionStateChangeHandler(Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            _onNotificationStateChanged = onNotificationStateChanged;
        }

        internal void Reset(NotificationStateItem rootNotificationStateItem)
        {
            RootNotificationStateItem = rootNotificationStateItem;
            ProcessTrace = new ProcessTrace();
            
            RiseNotificationStateChanged();
        }




        internal void StepStart(NotificationStateItem notificationStateItem, string stepName, bool hasInternalStep)
        {
            notificationStateItem.StepName = stepName;

            //if (!string.IsNullOrWhiteSpace(additionalStepInfo))
            //{
            //    notificationStateItem.StepName = $"{notificationStateItem.StepName} - {additionalStepInfo}";
            //}

            notificationStateItem.InternalNotificationStateItem = null;

            if (!hasInternalStep)
            {
                notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

                RiseNotificationStateChanged();
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

                    RiseNotificationStateChanged();
                }
            }
        }

        internal void ForceStepProgress(NotificationStateItem notificationStateItem, int forceSecondaryProcessStepNumber)
        {
            notificationStateItem.StepNumber = forceSecondaryProcessStepNumber;

            if (notificationStateItem.IsPrecentsAboveMin)
            {
                notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

                RiseNotificationStateChanged();
            }
        }


        internal void StepError(NotificationStateItem notificationStateItem, string errorCode, string errorMessage, string instructionsMessage)
        {
            notificationStateItem.ErrorCode = errorCode;
            notificationStateItem.ErrorMesage = errorMessage;
            notificationStateItem.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged();
        }






        private void RiseNotificationStateChanged()
        {
            NotificationStateItem snapshotNotificationState = RootNotificationStateItem.Clone();
            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            lock (ProcessTrace)
            {
                ProcessTrace.Appand(snapshotNotificationState);
            }

            Task.Run(() =>
            {
                _onNotificationStateChanged?.Invoke(ProcessTrace, snapshotNotificationState);
            });
        }
    }
}
