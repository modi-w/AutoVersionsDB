using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationExecutersProviderFactory
    {
        public NotificationExecutersProviderFactory()
        {

        }

        public NotificationExecutersProvider Create(Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStateChangeHandler notifictionStateChangeHandler = new NotifictionStateChangeHandler(onNotificationStateChanged);

            NotificationExecutersProvider notificationExecutersProvider = new NotificationExecutersProvider(notifictionStateChangeHandler);

            return notificationExecutersProvider;
        }
    }
}
