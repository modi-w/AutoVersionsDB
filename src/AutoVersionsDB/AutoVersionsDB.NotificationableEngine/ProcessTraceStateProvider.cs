//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Text;
//using System.Threading;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class ProcessTraceStateProvider
//    {
//        private static ConcurrentDictionary<string, ProcessTraceState> _processTraceStateCache;

//        public ProcessTraceStateProvider()
//        {
//            _processTraceStateCache = new ConcurrentDictionary<string, ProcessTraceState>();
//        }

        

//        public virtual string CreateNew(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
//        {
//            ProcessTraceState newProcessTraceState = new ProcessTraceState(processName, onStepNotificationStateChanged);

//            _processTraceStateCache[newProcessTraceState.Key] = newProcessTraceState;

//            return newProcessTraceState.Key;
//        }


//        public virtual ProcessTraceState Get(string key)
//        {
//            return _processTraceStateCache[key];
//        }

//        public virtual void Release(string key)
//        {
//            _processTraceStateCache.TryRemove(key, out ProcessTraceState removedProcessTraceState);
//        }


//    }
//}
