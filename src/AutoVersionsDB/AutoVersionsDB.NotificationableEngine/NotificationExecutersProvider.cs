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

        public NotifictionStateChangeHandler NotifictionStateChangeHandler { get; private set; }
        public ProcessTrace NotifictionStatesHistory
        {
            get
            {
                return NotifictionStateChangeHandler.NotifictionStatesHistory;
            }
        }



        public NotificationExecutersProvider(NotifictionStateChangeHandler notifictionStateChangeHandler)
        {
            NotifictionStateChangeHandler = notifictionStateChangeHandler;
        }


        public NotificationWrapperExecuter Reset(int numOfSteps)
        {
            var rootNotificationWrapperExecuter = CreateNotificationWrapperExecuter(numOfSteps);
            RootNotificationStateItem = rootNotificationWrapperExecuter.CurrentNotificationStateItem;

            NotifictionStateChangeHandler.Reset(RootNotificationStateItem);

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
