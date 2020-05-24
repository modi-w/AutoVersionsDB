using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationExecutersFactoryManager
    {
        public NotificationStateItem RootNotificationStateItem { get; private set; }

        public NotifictionStatesHistoryManager NotifictionStatesHistoryManager { get; private set; }
        public bool HasError
        {
            get
            {
                return NotifictionStatesHistoryManager.HasError;
            }
        }

        public string ErrorCode
        {
            get
            {
                return NotifictionStatesHistoryManager.ErrorCode;
            }
        }
        

        public string InstructionsMessage
        {
            get
            {
                return NotifictionStatesHistoryManager.InstructionsMessage;
            }
        }

        public string InstructionsMessage_StepName
        {
            get
            {
                return NotifictionStatesHistoryManager.InstructionsMessage_StepName;
            }
        }
        




        public NotificationExecutersFactoryManager(NotifictionStatesHistoryManager notifictionStatesHistoryManager)
        {
            NotifictionStatesHistoryManager = notifictionStatesHistoryManager;
        }

        public NotificationWrapperExecuter Reset(int numOfSteps)
        {
            var rootNotificationWrapperExecuter = CreateNotificationWrapperExecuter(numOfSteps);
            RootNotificationStateItem = rootNotificationWrapperExecuter.CurrentNotificationStateItem;

            NotifictionStatesHistoryManager.Reset(RootNotificationStateItem);


            NotifictionStatesHistoryManager.HandleNotificationStateChanged();

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
                new NotificationWrapperExecuter(NotifictionStatesHistoryManager, currentParentNotificationStateItem, numOfSteps);

            return newNotificationWrapperExecuter;
        }

        public void ClearAllInternalProcessState()
        {
            RootNotificationStateItem.InternalNotificationStateItem = null;
        }

    }
}
