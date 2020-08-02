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

        public NotificationExecutersProvider Create(Action<NotificationStateItem> onNotificationStateChanged)
        {
            NotifictionStatesHistory notifictionStatesHistory = new NotifictionStatesHistory(onNotificationStateChanged);

            NotificationExecutersProvider notificationExecutersProvider = new NotificationExecutersProvider(notifictionStatesHistory);

            return notificationExecutersProvider;
        }
    }
}
