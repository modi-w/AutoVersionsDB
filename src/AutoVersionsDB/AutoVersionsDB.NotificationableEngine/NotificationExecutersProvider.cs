using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationExecutersProvider
    {
        public NotificationStateItem RootNotificationStateItem { get; private set; }

        public NotifictionStatesHistory NotifictionStatesHistory { get; private set; }
        //public bool HasError
        //{
        //    get
        //    {
        //        return NotifictionStatesHistoryManager.HasError;
        //    }
        //}

        //public string ErrorCode
        //{
        //    get
        //    {
        //        return NotifictionStatesHistoryManager.ErrorCode;
        //    }
        //}


        //public string InstructionsMessage
        //{
        //    get
        //    {
        //        return NotifictionStatesHistoryManager.InstructionsMessage;
        //    }
        //}

        //public string InstructionsMessageStepName
        //{
        //    get
        //    {
        //        return NotifictionStatesHistoryManager.InstructionsMessageStepName;
        //    }
        //}





        public NotificationExecutersProvider(NotifictionStatesHistory notifictionStatesHistoryManager)
        {
            NotifictionStatesHistory = notifictionStatesHistoryManager;
        }

        public NotificationWrapperExecuter Reset(int numOfSteps)
        {
            var rootNotificationWrapperExecuter = CreateNotificationWrapperExecuter(numOfSteps);
            RootNotificationStateItem = rootNotificationWrapperExecuter.CurrentNotificationStateItem;

            NotifictionStatesHistory.Reset(RootNotificationStateItem);


            NotifictionStatesHistory.HandleNotificationStateChanged();

            return rootNotificationWrapperExecuter;
        }

        public NotificationWrapperExecuter CreateNotificationWrapperExecuter(int numOfSteps)
        {
            NotificationStateItem currentParentNotificationStateItem = null;
            NotificationStateItem nextNotificationStateItem = RootNotificationStateItem;

            while (nextNotificationStateItem != null)
            {
                currentParentNotificationStateItem = nextNotificationStateItem;
                nextNotificationStateItem = nextNotificationStateItem.InternalNotificationStateItem;
            }

            NotificationWrapperExecuter newNotificationWrapperExecuter =
                new NotificationWrapperExecuter(this, currentParentNotificationStateItem, numOfSteps);

            return newNotificationWrapperExecuter;
        }

        public void ClearAllInternalProcessState()
        {
            RootNotificationStateItem.InternalNotificationStateItem = null;
        }

    }
}
