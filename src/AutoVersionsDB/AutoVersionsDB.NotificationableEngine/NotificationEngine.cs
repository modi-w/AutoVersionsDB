using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{

    public interface INotificationEngine : IDisposable
    {
        ProcessTrace Run(ExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged);
    }

    public interface IStepsExecuter
    {
        void ExecuteSteps(IEnumerable<ActionStepBase> steps,
                                ProcessStateBase processState,
                                bool isContinueOnError);
    }

    public abstract class NotificationEngine<TEngineSettings, TProcessState> : INotificationEngine, IStepsExecuter
        where TEngineSettings : EngineSettings
        where TProcessState : ProcessStateBase, new()
    {
        private EngineSettings _engineSettings;
        private ProcessTraceHandler _processTraceHandler;


        public NotificationEngine(EngineSettings engineSettings,
                                    ProcessTraceHandler processTraceHandler)
        {
            _engineSettings = engineSettings;
            _processTraceHandler = processTraceHandler;
        }


        //private void RaiseInitiated(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        //{
        //    OnInitiated(new InitiateEngineEventArgs(notificationableEngineConfig, processState));
        //}
        //protected virtual void OnInitiated(InitiateEngineEventArgs e)
        //{
        //    this.Initiated?.Invoke(this, e);
        //}


        public ProcessTrace Run(ExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            TProcessState processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            processState.SetEngineSettings(_engineSettings);

            //RaiseInitiated(notificationableEngineConfig, processState);

            _processTraceHandler.StartProcess(_engineSettings.EngineTypeName, onNotificationStateChanged);

            if (_engineSettings.RollbackStep != null)
            {
                _engineSettings.RollbackStep.SetStepsExecuter(this);
            }

            ExecuteSteps(_engineSettings.ProcessSteps, processState, false);


            if (!processState.EndProcessDateTime.HasValue)
            {
                processState.EndProcessDateTime = DateTime.Now;
            }


            return _processTraceHandler.ProcessTrace;
        }


        public void ExecuteSteps(IEnumerable<ActionStepBase> steps,
                                ProcessStateBase processState,
                                bool isContinueOnError)
        {
            foreach (var step in steps)
            {
                step.SetStepsExecuter(this);
            }

            if (!processState.IsRollbackExecuted)
            {
                _processTraceHandler.SetNumOfInternalSteps(steps.Count());

                foreach (var step in steps)
                {
                    ExecuteStep(step,processState);

                    if (_processTraceHandler.HasError && !isContinueOnError)
                    {
                        if (processState.CanRollback)
                        {
                            _processTraceHandler.ClearAllInternalProcessState();

                            if (_engineSettings.RollbackStep != null)
                            {
                                ExecuteStep(_engineSettings.RollbackStep,processState);
                            }
                        }

                        processState.IsRollbackExecuted = true;

                        break;
                    }
                }
            }


        }




        private void ExecuteStep(ActionStepBase step, ProcessStateBase processState)
        {
            if (!processState.IsRollbackExecuted)
            {

                _processTraceHandler.StepStart(step.StepName);

                try
                {
                    step.Execute(processState);
                }
                catch (NotificationEngineException ex)
                {
                    _processTraceHandler.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage);

                }
                catch (Exception ex)
                {
                    _processTraceHandler.StepError(step.StepName, ex.ToString(), "Error occurred during the process.");
                }

                _processTraceHandler.StepEnd();
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

                if (_engineSettings != null)
                {
                    _engineSettings.Dispose();
                }
            }

            _disposed = true;
        }

        private static void disposStepsList(IEnumerable<ActionStepBase> processStepsToDispose)
        {
            foreach (var processStep in processStepsToDispose)
            {
                disposStepsList(processStep.InternalSteps);

                IDisposable disposeStep = processStep as IDisposable;
                if (disposeStep != null)
                {
                    disposeStep.Dispose();
                }
            }
        }

        #endregion


    }


    public abstract class NotificationEngine<TEngineSettings, TProcessState, TExecutionParams> : NotificationEngine<TEngineSettings, TProcessState>
        where TEngineSettings : EngineSettings
        where TProcessState : ProcessStateBase, new()
        where TExecutionParams : ExecutionParams
    {

        public NotificationEngine(TEngineSettings engineSettings,
                                    ProcessTraceHandler processTraceHandler)
            : base(engineSettings, processTraceHandler)
        {
        }

        //public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
        //{
        //    base.Prepare(notificationableEngineConfig);
        //}



        public ProcessTrace Run(TExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {
            return base.Run(executionParams, onNotificationStateChanged);
        }
    }

}


