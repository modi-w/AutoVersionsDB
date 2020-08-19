//using AutoVersionsDB.NotificationableEngine.Utils;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class NotificationWrapperExecuter : NotificationableActionStepBase, IDisposable
//    {
//        private readonly NotificationStateItem _parentNotificationStateItem;
//        private readonly List<NotificationableActionStepBase> _internalSteps;
//        private readonly bool _isContinueOnError;

//        private string _parentStepName;
//        public override string StepName
//        {
//            get
//            {
//                return _parentStepName;
//            }
//        }
//        public override bool HasInternalStep
//        {
//            get
//            {
//                return _internalSteps.Count > 0;
//            }
//        }



//        public NotificationStateItem CurrentNotificationStateItem { get; set; }



//        public NotificationWrapperExecuter(NotificationStateItem parentNotificationStateItem,
//                                            string parentStepName,
//                                            IEnumerable<NotificationableActionStepBase> internalSteps,
//                                            bool isContinueOnError)
//        {
//            _parentNotificationStateItem = parentNotificationStateItem;

//            _parentStepName = parentStepName;
//            _internalSteps = internalSteps.ToList();
//            _isContinueOnError = isContinueOnError;

//            CurrentNotificationStateItem = new NotificationStateItem(_internalSteps.Count);

//            if (_parentNotificationStateItem != null)
//            {
//                _parentNotificationStateItem.InternalNotificationStateItem = CurrentNotificationStateItem;
//            }
//        }


//        public override int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
//        {
//            return _internalSteps.Count;
//        }



//        public override void Execute(NotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, ProcessStateBase processState)
//        {
//            foreach (var step in _internalSteps)
//            {
//                try
//                {
//                    int numOfInternalStep = step.GetNumOfInternalSteps(notificationableEngineConfig, processState);

//                    notificationExecutersProvider.NotifictionStateChangeHandler.StepStart(CurrentNotificationStateItem, step.StepName, step.HasInternalStep);

//                    step.Execute(notificationableEngineConfig, notificationExecutersProvider, processState);

//                    notificationExecutersProvider.NotifictionStateChangeHandler.StepEnd(CurrentNotificationStateItem, step.HasInternalStep);

//                }
//                catch (NotificationEngineException ex)
//                {
//                    notificationExecutersProvider.NotifictionStateChangeHandler.StepError(CurrentNotificationStateItem, ex.ErrorCode, ex.Message, ex.InstructionsMessage);

//                }
//                catch (Exception ex)
//                {
//                    notificationExecutersProvider.NotifictionStateChangeHandler.StepError(CurrentNotificationStateItem, step.StepName, ex.Message, "Error occurred during the process.");
//                }

//                if (notificationExecutersProvider.ProcessTrace.HasError && !_isContinueOnError)
//                {
//                    break;
//                }

//            }
//        }





//        #region IDisposable
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        ~NotificationWrapperExecuter()
//        {
//            Dispose(false);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                // free managed resources

//                CurrentNotificationStateItem = null;

//                if (_parentNotificationStateItem != null)
//                {
//                    _parentNotificationStateItem.InternalNotificationStateItem = null;
//                }
//            }
//            // free native resources here if there are any
//        }

//        #endregion

//    }

//    //public class NotificationWrapperExecuter<TProcessState> : NotificationWrapperExecuter
//    //    where TProcessState : ProcessStateBase
//    //{

//    //    public NotificationWrapperExecuter(NotificationExecutersProvider notificationExecutersProvider,
//    //                                        NotificationStateItem parentNotificationStateItem,
//    //                                        int numOfStep)
//    //        : base(notificationExecutersProvider, parentNotificationStateItem, numOfStep)
//    //    {
//    //    }


//    //    public void ExecuteStep(NotificationableActionStepBase step, NotificationableEngineConfig notificationableEngineConfig, TProcessState processState)
//    //    {
//    //        base.ExecuteStep(step, notificationableEngineConfig, processState);
//    //    }
//    //}
//}
