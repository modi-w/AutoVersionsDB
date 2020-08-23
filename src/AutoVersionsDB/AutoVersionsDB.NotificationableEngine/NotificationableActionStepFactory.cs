//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class NotificationableActionStepFactory
//    {
//        private NotifictionStateChangeHandler _notifictionStateChangeHandler;

//        public NotificationableActionStepFactory()
//        {

//        }

//        internal void SetNotifictionStateChangeHandler(NotifictionStateChangeHandler notifictionStateChangeHandler)
//        {
//            _notifictionStateChangeHandler = notifictionStateChangeHandler;
//        }

//        internal NotificationableActionStep DecorateStep(ActionStepBase step)
//        {
//            return new NotificationableActionStep(_notifictionStateChangeHandler, step);
//        }

//        internal List<NotificationableActionStep> DecorateSteps(IEnumerable<ActionStepBase> steps)
//        {
//            return steps.Select(e => DecorateStep(e)).ToList();
//        }
//    }
//}
