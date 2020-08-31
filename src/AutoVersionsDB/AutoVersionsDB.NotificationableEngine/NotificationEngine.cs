using AutoVersionsDB.NotificationableEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{

    internal sealed class NotificationEngine : IStepsExecuter, IDisposable
    {
        private ProcessTraceHandler _processTraceHandler;
        private EngineContext _engineContext;

        internal NotificationEngine(ProcessTraceHandler processTraceHandler,
                                    EngineContext engineContext)
        {
            _processTraceHandler = processTraceHandler;
            _engineContext = engineContext;
        }



        internal ProcessTrace Run(ExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {

            _engineContext.ExecutionParams = executionParams;

            _engineContext.StartProcessDateTime = DateTime.Now;


            _processTraceHandler.StartProcess(_engineContext.EngineSettings.EngineTypeName, onNotificationStateChanged);

            if (_engineContext.EngineSettings.RollbackStep != null)
            {
                _engineContext.EngineSettings.RollbackStep.SetStepsExecuter(this);
            }

            ExecuteSteps(_engineContext.EngineSettings.ProcessSteps, _engineContext, false);


            if (!_engineContext.EndProcessDateTime.HasValue)
            {
                _engineContext.EndProcessDateTime = DateTime.Now;
            }


            return _processTraceHandler.ProcessTrace;
        }


        public void ExecuteSteps(IEnumerable<ActionStepBase> steps,
                                EngineContext _engineContext,
                                bool isContinueOnError)
        {
            foreach (var step in steps)
            {
                step.SetStepsExecuter(this);
            }

            if (!_engineContext.IsRollbackExecuted)
            {
                _processTraceHandler.SetNumOfInternalSteps(steps.Count());

                foreach (var step in steps)
                {
                    ExecuteStep(step);

                    if (_processTraceHandler.HasError && !isContinueOnError)
                    {
                        if (_engineContext.CanRollback)
                        {
                            _processTraceHandler.ClearAllInternalProcessState();

                            if (_engineContext.EngineSettings.RollbackStep != null)
                            {
                                ExecuteStep(_engineContext.EngineSettings.RollbackStep);
                            }
                        }

                        _engineContext.IsRollbackExecuted = true;

                        break;
                    }
                }
            }


        }




        private void ExecuteStep(ActionStepBase step)
        {
            if (!_engineContext.IsRollbackExecuted)
            {

                _processTraceHandler.StepStart(step.StepName);

                try
                {
                    step.Execute(_engineContext);
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
        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                if (_engineContext.EngineSettings != null)
                {
                    _engineContext.EngineSettings.Dispose();
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


    //public class NotificationEngine<TEngineSettings, TProcessState, TExecutionParams> : NotificationEngine<TEngineSettings, TProcessState>
    //    where TEngineSettings : EngineSettings
    //    where TProcessState : ProcessStateBase, new()
    //    where TExecutionParams : ExecutionParams
    //{

    //    public NotificationEngine(ProcessTraceHandler processTraceHandler)
    //        : base(engineSettings, processTraceHandler)
    //    {
    //    }

    //    //public virtual void Prepare(TNotificationableEngineConfig notificationableEngineConfig)
    //    //{
    //    //    base.Prepare(notificationableEngineConfig);
    //    //}



    //    public ProcessTrace Run(TExecutionParams executionParams, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
    //    {
    //        return base.Run(executionParams, onNotificationStateChanged);
    //    }
    //}

}


