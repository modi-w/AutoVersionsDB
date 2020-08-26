//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class ProcessTraceState
//    {
//        public Action<ProcessTrace, StepNotificationState> OnStepNotificationStateChanged { get; }


//        public string Key { get; }

//        public ProcessTrace ProcessTrace { get; }
//        public StepNotificationState RootStepNotificationState { get; }


//        public StepNotificationState ParentStepNotificationState
//        {
//            get
//            {
//                StepNotificationState parentStepNotificationState = this.RootStepNotificationState;
//                StepNotificationState prevParentStepNotificationState = parentStepNotificationState;

//                while (parentStepNotificationState != null
//                        && parentStepNotificationState.InternalStepNotificationState != null)
//                {
//                    prevParentStepNotificationState = parentStepNotificationState;
//                    parentStepNotificationState = parentStepNotificationState.InternalStepNotificationState;
//                }

//                return prevParentStepNotificationState;
//            }
//        }

//        public StepNotificationState CurrentStepNotificationState
//        {
//            get
//            {
//                StepNotificationState parentStepNotificationState = ParentStepNotificationState;

//                if (parentStepNotificationState.InternalStepNotificationState == null)
//                {
//                    return parentStepNotificationState;
//                }
//                else
//                {
//                    return parentStepNotificationState.InternalStepNotificationState;
//                }

//            }
//        }




//        public ProcessTraceState(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
//        {
//            OnStepNotificationStateChanged = onStepNotificationStateChanged;

//            RootStepNotificationState = new StepNotificationState(processName);
//            ProcessTrace = new ProcessTrace();

//            Key = Guid.NewGuid().ToString();
//        }



      

//    }
//}
