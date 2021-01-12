using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoVersionsDB.NotificationableEngine
{

    internal sealed class NotificationEngine : IStepsExecuter, IDisposable
    {
        private readonly ProcessTraceHandler _processTraceHandler;
        private readonly ProcessContext _processContext;

        internal NotificationEngine(ProcessTraceHandler processTraceHandler,
                                    ProcessContext processContext)
        {
            _processTraceHandler = processTraceHandler;
            _processContext = processContext;
        }



        internal ProcessResults Run(ProcessArgs processArgs, Action<ProcessTrace, StepNotificationState> onNotificationStateChanged)
        {

            _processContext.ProcessArgs = processArgs;

            _processContext.StartProcessDateTime = DateTime.Now;


            _processTraceHandler.StartProcess(_processContext.ProcessDefinition.EngineTypeName, onNotificationStateChanged);

            if (_processContext.ProcessDefinition.RollbackStep != null)
            {
                _processContext.ProcessDefinition.RollbackStep.SetStepsExecuter(this);
            }

            ExecuteSteps(_processContext.ProcessDefinition.ProcessSteps, false);


            if (!_processContext.EndProcessDateTime.HasValue)
            {
                _processContext.EndProcessDateTime = DateTime.Now;
            }

            _processTraceHandler.WaitForStepNotificationStateChangesComplete();

            return new ProcessResults(_processTraceHandler.ProcessTrace, _processContext.Results);
        }


        public void ExecuteSteps(IEnumerable<ActionStepBase> steps,
                                bool isContinueOnError)
        {
            foreach (var step in steps)
            {
                step.SetStepsExecuter(this);
            }

            if (!_processContext.IsRollbackExecuted)
            {
                _processTraceHandler.SetNumOfInternalSteps(steps.Count());

                foreach (var step in steps)
                {
                    ExecuteStep(step);

                    if (_processTraceHandler.HasError && !isContinueOnError)
                    {
                        if (_processContext.CanRollback)
                        {
                            _processTraceHandler.ClearAllStepNotificationState();

                            if (_processContext.ProcessDefinition.RollbackStep != null)
                            {
                                ExecuteStep(_processContext.ProcessDefinition.RollbackStep);
                            }
                        }

                        _processContext.IsRollbackExecuted = true;

                        break;
                    }
                }
            }


        }




        private void ExecuteStep(ActionStepBase step)
        {
            if (!_processContext.IsRollbackExecuted)
            {

                _processTraceHandler.StepStart(step.StepName);

                try
                {
                    step.Execute(_processContext);
                }
                catch (NotificationProcessException ex)
                {
                    _processTraceHandler.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage, ex.NotificationErrorType);

                }
                catch (Exception ex)
                {
                    _processTraceHandler.StepError(step.StepName, ex.ToString(), "Error occurred during the process.", NotificationErrorType.Error);
                }

                _processTraceHandler.StepEnd();
            }
        }




        #region IDisposable

        private bool _disposed;

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

                if (_processContext.ProcessDefinition != null)
                {
                    _processContext.ProcessDefinition.Dispose();
                }

                _processTraceHandler.Dispose();
            }

            _disposed = true;
        }



        #endregion


    }



}


