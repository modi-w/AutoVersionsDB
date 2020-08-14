using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationExecutersProvider
    {
        internal NotificationStateItem RootNotificationStateItem { get; private set; }

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


        internal NotificationWrapperExecuter Reset(int numOfSteps)
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

        internal void ClearAllInternalProcessState()
        {
            RootNotificationStateItem.InternalNotificationStateItem = null;
        }

    }
}
