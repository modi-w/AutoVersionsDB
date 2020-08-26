//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class ProcessTraceStateChangeHandler
//    {
//        private ProcessTraceStateProvider _processTraceStateProvider;

//        public ProcessTraceStateChangeHandler(ProcessTraceStateProvider processTraceStateProvider)
//        {
//            _processTraceStateProvider = processTraceStateProvider;
//        }


//        internal string CreateNew(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
//        {
//            string processTraceStateKey = _processTraceStateProvider.CreateNew(processName, onStepNotificationStateChanged);

//            RiseNotificationStateChanged(processTraceStateKey);

//            return processTraceStateKey;
//        }






//        internal ProcessTrace ProcessTrace(string processTraceStateKey)
//        {
//            return _processTraceStateProvider
//                .Get(processTraceStateKey)
//                .ProcessTrace;
//        }

//        internal bool HasError(string processTraceStateKey)
//        {
//            return _processTraceStateProvider
//                .Get(processTraceStateKey)
//            .ProcessTrace.HasError;
//        }



//        //internal void ForceStepProgress(StepNotificationState stepNotificationState, int forceSecondaryProcessStepNumber)
//        //{
//        //    stepNotificationState.StepNumber = forceSecondaryProcessStepNumber;

//        //    if (stepNotificationState.IsPrecentsAboveMin)
//        //    {
//        //        stepNotificationState.LastNotifyPrecents = stepNotificationState.Precents;

//        //        RiseNotificationStateChanged();
//        //    }
//        //}




//        internal void Release(string processTraceStateKey)
//        {
//            _processTraceStateProvider.Release(processTraceStateKey);
//        }






//    }
//}
