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

    public abstract class NotificationEngine<TEngineSettings, TProcessState> : INotificationEngine
        where TEngineSettings : EngineSettings
        where TProcessState : ProcessStateBase, new()
    {
        private EngineSettings _engineSettings;
        private StepsExecuter _stepsExecuter;

        public NotificationEngine(EngineSettings engineSettings,
                                    StepsExecuter stepsExecuter)
        {
            _engineSettings = engineSettings;
            _stepsExecuter = stepsExecuter;
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
            ProcessTrace processTraceResults;

            TProcessState processState = new TProcessState()
            {
                ExecutionParams = executionParams,

                StartProcessDateTime = DateTime.Now
            };

            processState.SetEngineSettings(_engineSettings);

            //RaiseInitiated(notificationableEngineConfig, processState);

            ProcessTrace processTrace = new ProcessTrace(_engineSettings.EngineTypeName, onNotificationStateChanged);

            _stepsExecuter.SetProcessProperty(processTrace, _engineSettings.RollbackStep);

            if (_engineSettings.RollbackStep != null)
            {
                _engineSettings.RollbackStep.SetStepsExecuter(_stepsExecuter);
            }

            _stepsExecuter.ExecuteSteps(_engineSettings.ProcessSteps, processState, false);


            if (!processState.EndProcessDateTime.HasValue)
            {
                processState.EndProcessDateTime = DateTime.Now;
            }


            return processTrace;
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
                                    StepsExecuter stepsExecuter)
            : base(engineSettings, stepsExecuter)
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


