//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class NotificationExecutersProviderFactory
//    {
//        public NotificationExecutersProviderFactory()
//        {

//        }

//#pragma warning disable CA1822 // Mark members as static
//        public NotificationExecutersProvider Create(Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
//#pragma warning restore CA1822 // Mark members as static
//        {
//            NotifictionStateChangeHandler notifictionStateChangeHandler = new NotifictionStateChangeHandler(onNotificationStateChanged);

//            NotificationExecutersProvider notificationExecutersProvider = new NotificationExecutersProvider(notifictionStateChangeHandler);

//            return notificationExecutersProvider;
//        }
//    }
//}
