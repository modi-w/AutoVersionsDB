using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationExecutersProvider
    {
       // internal NotificationStateItem RootNotificationStateItem { get; private set; }

        internal NotifictionStateChangeHandler NotifictionStateChangeHandler { get; private set; }
        public ProcessTrace ProcessTrace
        {
            get
            {
                return NotifictionStateChangeHandler.ProcessTrace;
            }
        }



        internal NotificationExecutersProvider(NotifictionStateChangeHandler notifictionStateChangeHandler)
        {
            NotifictionStateChangeHandler = notifictionStateChangeHandler;
        }


        internal NotificationWrapperExecuter Reset(IEnumerable<NotificationableActionStepBase> internalSteps,
                                                    bool isContinueOnError)
        {
            var rootNotificationWrapperExecuter = CreateNotificationWrapperExecuter("", internalSteps, isContinueOnError);

            NotifictionStateChangeHandler.Reset(rootNotificationWrapperExecuter.CurrentNotificationStateItem);

            return rootNotificationWrapperExecuter;
        }

        public NotificationWrapperExecuter CreateNotificationWrapperExecuter(string parentStepName,
                                                                            IEnumerable<NotificationableActionStepBase> internalSteps,
                                                                            bool isContinueOnError)
        {
            NotificationStateItem currentParentNotificationStateItem = NotifictionStateChangeHandler.CurrentNotificationStateItem;

            NotificationWrapperExecuter newNotificationWrapperExecuter =
                new NotificationWrapperExecuter(this, currentParentNotificationStateItem, parentStepName, internalSteps, isContinueOnError);

            return newNotificationWrapperExecuter;
        }


        public void SetStepStartManually(int nufOfSteps, string stepName)
        {
            NotificationStateItem currentParentNotificationStateItem = NotifictionStateChangeHandler.CurrentNotificationStateItem;
            currentParentNotificationStateItem.InternalNotificationStateItem = new NotificationStateItem(nufOfSteps);

            this.NotifictionStateChangeHandler.StepStart(currentParentNotificationStateItem, stepName, false);
        }

      

        public void ForceStepProgress(int stepNumber)
        {
            NotificationStateItem currentParentNotificationStateItem = NotifictionStateChangeHandler.CurrentNotificationStateItem;

            this.NotifictionStateChangeHandler.ForceStepProgress(currentParentNotificationStateItem, stepNumber);
        }

        //public void ForceStepEnd(int stepNumber)
        //{
        //    NotificationStateItem currentParentNotificationStateItem = getCurrentParentNotificationStateItem();

        //    this.NotifictionStateChangeHandler.ForceStepProgress(currentParentNotificationStateItem, stepNumber);
        //}


        internal void ClearAllInternalProcessState()
        {
            NotifictionStateChangeHandler.RootNotificationStateItem.InternalNotificationStateItem = null;
        }



    }
}
