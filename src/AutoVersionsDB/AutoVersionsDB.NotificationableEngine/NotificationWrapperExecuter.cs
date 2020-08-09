using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationWrapperExecuter : IDisposable
    {
        private readonly NotificationExecutersProvider _notificationExecutersProvider;
        private readonly NotificationStateItem _parentNotificationStateItem;
        public NotificationStateItem CurrentNotificationStateItem { get; set; }


        public NotificationWrapperExecuter(NotificationExecutersProvider notificationExecutersProvider,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep)
        {
            _notificationExecutersProvider = notificationExecutersProvider;
            _parentNotificationStateItem = parentNotificationStateItem;

            CurrentNotificationStateItem = new NotificationStateItem(numOfStep);

            if (_parentNotificationStateItem != null)
            {
                _parentNotificationStateItem.InternalNotificationStateItem = CurrentNotificationStateItem;
            }
        }


        public void ExecuteStep(NotificationableActionStepBase step, NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        {
            step.ThrowIfNull(nameof(step));

            //Comment: we do not check here: if (!_notifictionStatesHistoryManager.HasError)
            //          because then the engine will not run the rollback process

            try
            {
                int numOfInternalStep = step.GetNumOfInternalSteps(notificationableEngineConfig,processState);

                _notificationExecutersProvider.NotifictionStateChangeHandler.StepStart(CurrentNotificationStateItem, step.StepName, step.HasInternalStep);

                step.Execute(notificationableEngineConfig, _notificationExecutersProvider, processState);

                _notificationExecutersProvider.NotifictionStateChangeHandler.StepEnd(CurrentNotificationStateItem, step.HasInternalStep);

            }
            catch (NotificationEngineException ex)
            {
                _notificationExecutersProvider.NotifictionStateChangeHandler.StepError(CurrentNotificationStateItem, ex.ErrorCode, ex.Message, ex.InstructionsMessage);

            }
            catch (Exception ex)
            {
                _notificationExecutersProvider.NotifictionStateChangeHandler.StepError(CurrentNotificationStateItem, step.StepName, ex.Message, "Error occurred during the process.");

            }

        }


        public void SetStepStartManually(string stepName)
        {
            _notificationExecutersProvider.NotifictionStateChangeHandler.StepStart(CurrentNotificationStateItem, stepName, false);
        }


        public void ForceStepProgress(int stepNumber)
        {
            _notificationExecutersProvider.NotifictionStateChangeHandler.ForceStepProgress(CurrentNotificationStateItem, stepNumber);
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

        public NotificationWrapperExecuter(NotificationExecutersProvider notificationExecutersProvider,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep)
            : base(notificationExecutersProvider, parentNotificationStateItem, numOfStep)
        {
        }


        public void ExecuteStep(NotificationableActionStepBase step, NotificationableEngineConfig notificationableEngineConfig, TProcessState processState)
        {
            base.ExecuteStep(step, notificationableEngineConfig, processState);
        }
    }
}
