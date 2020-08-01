﻿using AutoVersionsDB.NotificationableEngine.Utils;
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

        private readonly double _minPrecentChangeToNotify;

        private double _prevNotifyPrecent;

        public NotificationWrapperExecuter(NotificationExecutersProvider notificationExecutersProvider,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep,
                                            double minPrecentChangeToNotify = 1)
        {
            _notificationExecutersProvider = notificationExecutersProvider;
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
            step.ThrowIfNull(nameof(step));

            //Comment: we do not check here: if (!_notifictionStatesHistoryManager.HasError)
            //          because then the engine will not run the rollback process

            try
            {
                int numOfInternalStep = step.GetNumOfInternalSteps(processState, actionStepArgs);

                CurrentNotificationStateItem.StepStart(step.StepName, additionalStepInfo);
                if (step.InternalNotificationableAction == null)
                {
                    CallHandleNotificationStateChanged();
                }

                step.Execute(_notificationExecutersProvider, processState, actionStepArgs);

                CurrentNotificationStateItem.StepEnd();
                if (step.InternalNotificationableAction == null)
                {
                    CallHandleNotificationStateChanged();
                }
            }
            catch (NotificationEngineException ex)
            {
                CurrentNotificationStateItem.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage);
                _notificationExecutersProvider.NotifictionStatesHistory.HandleNotificationStateChanged();

            }
            catch (Exception ex)
            {
                CurrentNotificationStateItem.StepError(step.StepName, ex.Message, "Error occurred during the process.");
                _notificationExecutersProvider.NotifictionStatesHistory.HandleNotificationStateChanged();

            }

        }

        public void CallHandleNotificationStateChanged()
        {
            if (_prevNotifyPrecent == 0
                || CurrentNotificationStateItem.Precents - _prevNotifyPrecent > _minPrecentChangeToNotify)
            {
                _prevNotifyPrecent = CurrentNotificationStateItem.Precents;
                _notificationExecutersProvider.NotifictionStatesHistory.HandleNotificationStateChanged();
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

        public NotificationWrapperExecuter(NotificationExecutersProvider notificationExecutersProvider,
                                            NotificationStateItem parentNotificationStateItem,
                                            int numOfStep,
                                            double minPrecentChangeToNotify = 1)
            : base(notificationExecutersProvider, parentNotificationStateItem, numOfStep, minPrecentChangeToNotify)
        {
        }


        public void ExecuteStep(NotificationableActionStepBase step, string additionalStepInfo, TProcessState processState, ActionStepArgs actionStepArgs)
        {
            base.ExecuteStep(step, additionalStepInfo, processState, actionStepArgs);
        }
    }
}
