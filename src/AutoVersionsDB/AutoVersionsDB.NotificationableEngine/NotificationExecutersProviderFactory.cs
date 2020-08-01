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

        public NotificationExecutersProvider Create()
        {
            NotifictionStatesHistory notifictionStatesHistory = new NotifictionStatesHistory();

            NotificationExecutersProvider notificationExecutersProvider = new NotificationExecutersProvider(notifictionStatesHistory);

            return notificationExecutersProvider;
        }
    }
}
