using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationWrapperExecuter : IDisposable
    {
        private NotifictionStatesHistoryManager _notifictionStatesHistoryManager;
        private NotificationStateItem _parentNotificationStateItem;
        public NotificationStateItem CurrentNotificationStateItem;

        private double _minPrecentChangeToNotify;

        private double _prevNotifyPrecent;

        public NotificationWrapperExecuter(NotifictionStatesHistoryManager notifictionStatesHistoryManager,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep,
                                            double minPrecentChangeToNotify = 1)
        {
            _notifictionStatesHistoryManager = notifictionStatesHistoryManager;
            _parentNotificationStateItem = parentNotificationStateItem;
            _minPrecentChangeToNotify = minPrecentChangeToNotify;

            CurrentNotificationStateItem = new NotificationStateItem(numOfStep);

            if (_parentNotificationStateItem != null)
            {
                _parentNotificationStateItem.InternalNotificationStateItem = CurrentNotificationStateItem;
            }
        }


        public void ExecuteStep(NotificationableActionStepBase step, string additionalStepInfo, ProcessStateBase processState, ActionStepArgs actionStepArgs)
        {
            //Comment: we do not check here: if (!_notifictionStatesHistoryManager.HasError)
            //          because then the engine will not run the rollback process

            try
            {
                int numOfInternalStep = step.GetNumOfInternalSteps(processState, actionStepArgs);

                CurrentNotificationStateItem.StepStart(step.StepName, additionalStepInfo);
                CallHandleNotificationStateChanged();

                step.Execute(processState, actionStepArgs);

                CurrentNotificationStateItem.StepEnd();
                CallHandleNotificationStateChanged();
            }
            catch (NotificationEngineException ex)
            {
                CurrentNotificationStateItem.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage);
                _notifictionStatesHistoryManager.HandleNotificationStateChanged();

            }
            catch (Exception ex)
            {
                CurrentNotificationStateItem.StepError(step.StepName, ex.Message, "Error occurred during the process.");
                _notifictionStatesHistoryManager.HandleNotificationStateChanged();

            }

        }

        public void CallHandleNotificationStateChanged()
        {
            if (_prevNotifyPrecent == 0
                || CurrentNotificationStateItem.Precents - _prevNotifyPrecent > _minPrecentChangeToNotify)
            {
                _prevNotifyPrecent = CurrentNotificationStateItem.Precents;
                _notifictionStatesHistoryManager.HandleNotificationStateChanged();
            }
        }



        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NotificationWrapperExecuter()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources

                CurrentNotificationStateItem = null;

                if (_parentNotificationStateItem != null)
                {
                    _parentNotificationStateItem.InternalNotificationStateItem = null;
                }
            }
            // free native resources here if there are any
        }

        #endregion

    }

    public class NotificationWrapperExecuter<TProcessState> : NotificationWrapperExecuter
        where TProcessState : ProcessStateBase
    {

        public NotificationWrapperExecuter(NotifictionStatesHistoryManager notifictionStatesHistoryManager,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep,
                                            double minPrecentChangeToNotify = 1)
            : base(notifictionStatesHistoryManager, parentNotificationStateItem, numOfStep, minPrecentChangeToNotify)
        {
        }


        public void ExecuteStep(NotificationableActionStepBase step, string additionalStepInfo, TProcessState processState, ActionStepArgs actionStepArgs)
        {
            base.ExecuteStep(step, additionalStepInfo, processState, actionStepArgs);
        }
    }
}
