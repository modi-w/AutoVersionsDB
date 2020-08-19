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







        internal NotifictionStateChangeHandler(Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            _onNotificationStateChanged = onNotificationStateChanged;
        }

        internal void Reset(string processName)
        {
            RootNotificationStateItem = new NotificationStateItem(processName);
            ProcessTrace = new ProcessTrace();

            RiseNotificationStateChanged();
        }




        internal void StepStart(string stepName)
        {
            getCurrentNotificationStateItem().InternalNotificationStateItem = new NotificationStateItem(stepName);

            //if (!string.IsNullOrWhiteSpace(additionalStepInfo))
            //{
            //    notificationStateItem.StepName = $"{notificationStateItem.StepName} - {additionalStepInfo}";
            //}

            //     notificationStateItem.InternalNotificationStateItem = null;

            //if (notificationStateItem.NumOfSteps > 0)
            //{
            //    notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

            //    RiseNotificationStateChanged();
            //}
        }

        public void SetInternalSteps(int numOfSteps)
        {
            getCurrentNotificationStateItem().SetNumOfSteps(numOfSteps);

            RiseNotificationStateChanged();
        }


        internal void StepEnd()
        {
            NotificationStateItem parentNotificationStateItem = getParentNotificationStateItem();

            parentNotificationStateItem.StepNumber++;

            if (parentNotificationStateItem.NumOfSteps > 0)
            {
                if (parentNotificationStateItem.IsPrecentsAboveMin)
                {
                    parentNotificationStateItem.LastNotifyPrecents = parentNotificationStateItem.Precents;

                    RiseNotificationStateChanged();
                }
            }

            parentNotificationStateItem.InternalNotificationStateItem = null;


        }

        //internal void ForceStepProgress(NotificationStateItem notificationStateItem, int forceSecondaryProcessStepNumber)
        //{
        //    notificationStateItem.StepNumber = forceSecondaryProcessStepNumber;

        //    if (notificationStateItem.IsPrecentsAboveMin)
        //    {
        //        notificationStateItem.LastNotifyPrecents = notificationStateItem.Precents;

        //        RiseNotificationStateChanged();
        //    }
        //}


        internal void StepError(string errorCode, string errorMessage, string instructionsMessage)
        {
            NotificationStateItem currentNotificationStateItem = getCurrentNotificationStateItem();
            currentNotificationStateItem.ErrorCode = errorCode;
            currentNotificationStateItem.ErrorMesage = errorMessage;
            currentNotificationStateItem.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged();
        }



        internal void ClearAllInternalProcessState()
        {
            RootNotificationStateItem.InternalNotificationStateItem = null;
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



        private NotificationStateItem getCurrentNotificationStateItem()
        {
            NotificationStateItem parentNotificationStateItem = getParentNotificationStateItem();

            if (parentNotificationStateItem.InternalNotificationStateItem == null)
            {
                return parentNotificationStateItem;
            }
            else
            {
                return parentNotificationStateItem.InternalNotificationStateItem;
            }

        }
        private NotificationStateItem getParentNotificationStateItem()
        {

            NotificationStateItem parentNotificationStateItem = this.RootNotificationStateItem;
            NotificationStateItem prevParentNotificationStateItem = parentNotificationStateItem;

            while (parentNotificationStateItem!= null
                    && parentNotificationStateItem.InternalNotificationStateItem!= null)
            {
                prevParentNotificationStateItem = parentNotificationStateItem;
                parentNotificationStateItem = parentNotificationStateItem.InternalNotificationStateItem;
            }

            return prevParentNotificationStateItem;
        }
    }
}
