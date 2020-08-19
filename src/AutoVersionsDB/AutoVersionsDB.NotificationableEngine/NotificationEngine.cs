using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{

    public abstract class NotificationEngine<TProcessState> : IDisposable
        where TProcessState : ProcessStateBase, new()
    {
        private NotifictionStateChangeHandler _notifictionStateChangeHandler;
        private NotificationableEngineConfig _notificationableEngineConfig;
        private ProcessStateBase _processState;

        public abstract string EngineTypeName { get; }
        public Dictionary<string, string> EngineMetaData { get; }

        public List<NotificationableActionStepBase> ProcessSteps { get; }
        public NotificationableActionStepBase RollbackStep { get; }

        public event EventHandler<InitiateEngineEventArgs> Initiated;

        public NotificationEngine(NotificationableActionStepBase rollbackStep)
        {
            RollbackStep = rollbackStep;

            ProcessSteps = new List<NotificationableActionStepBase>();

            EngineMetaData = new Dictionary<string, string>
            {
                ["EngineTypeName"] = EngineTypeName
            };

        }


        private void RaiseInitiated()
        {
            OnInitiated(new InitiateEngineEventArgs(_notificationableEngineConfig, _processState));
        }
        protected virtual void OnInitiated(InitiateEngineEventArgs e)
        {
            this.Initiated?.Invoke(this, e);
        }


        public ProcessTrace Run(NotificationableEngineConfig notificationableEngineConfig, ExecutionParams executionParams, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            _notificationableEngineConfig = notificationableEngineConfig;
            _notifictionStateChangeHandler = new NotifictionStateChangeHandler(onNotificationStateChanged);

            _processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            _processState.SetEngineMetaData(this.EngineMetaData);

            RaiseInitiated();


            _notifictionStateChangeHandler.Reset(EngineTypeName);

            ExecuteStepsList(ProcessSteps, false);


            if (!_processState.EndProcessDateTime.HasValue)
            {
                _processState.EndProcessDateTime = DateTime.Now;
            }

            return _notifictionStateChangeHandler.ProcessTrace;
        }




        private void ExecuteStepsList(List<NotificationableActionStepBase> steps, bool isContinueOnError)
        {
            if (!_processState.IsRollbackExecuted)
            {
                _notifictionStateChangeHandler.SetInternalSteps(steps.Count);

                foreach (var step in steps)
                {
                    ExecuteStep(step);

                    if (_notifictionStateChangeHandler.ProcessTrace.HasError && !isContinueOnError)
                    {
                        if (_processState.CanRollback)
                        {
                            _notifictionStateChangeHandler.ClearAllInternalProcessState();

                            ExecuteStep(RollbackStep);
                        }

                        _processState.IsRollbackExecuted = true;

                        break;
                    }
                }
            }


        }

        private void ExecuteStep(NotificationableActionStepBase step)
        {
            if (!_processState.IsRollbackExecuted)
            {

                _notifictionStateChangeHandler.StepStart(step.StepName);

                try
                {
                    step.Execute(_notificationableEngineConfig, _processState, ExecuteStepsList);
                }
                catch (NotificationEngineException ex)
                {
                    _notifictionStateChangeHandler.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage);

                }
                catch (Exception ex)
                {
                    _notifictionStateChangeHandler.StepError(step.StepName, ex.ToString(), "Error occurred during the process.");
                }

                _notifictionStateChangeHandler.StepEnd();
            }

        }






        #region IDisposable

        private bool _disposed = false;

        ~NotificationEngine() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                if (RollbackStep != null)
                {
                    if (RollbackStep is IDisposable)
                    {
                        (RollbackStep as IDisposable).Dispose();
                    }
                }


                disposStepsList(this.ProcessSteps);
            }

            _disposed = true;
        }

        private static void disposStepsList(IEnumerable<NotificationableActionStepBase> processStepsToDispose)
        {
            foreach (var processStep in processStepsToDispose)
            {
                disposStepsList(processStep.InternalSteps);

                IDisposable disposeStep =processStep as IDisposable;
                if (disposeStep != null)
                {
                    disposeStep.Dispose();
                }
            }
        }

        #endregion


    }


    public abstract class NotificationEngine<TProcessState, TExecutionParams, TNotificationableEngineConfig> : NotificationEngine<TProcessState>
        where TProcessState : ProcessStateBase, new()
        where TExecutionParams : ExecutionParams
        where TNotificationableEngineConfig : NotificationableEngineConfig
    {

        public NotificationEngine(NotificationableActionStepBase rollbackStep)
            : base(rollbackStep)
        {
        }

        //public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
        //{
        //    base.Prepare(notificationableEngineConfig);
        //}



        public ProcessTrace Run(TNotificationableEngineConfig notificationableEngineConfig, TExecutionParams executionParams, Action<ProcessTrace, NotificationStateItem> onNotificationStateChanged)
        {
            return base.Run(notificationableEngineConfig as NotificationableEngineConfig, executionParams, onNotificationStateChanged);
        }
    }

}


