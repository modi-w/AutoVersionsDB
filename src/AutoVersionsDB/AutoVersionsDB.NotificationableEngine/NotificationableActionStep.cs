using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    internal class NotificationableActionStep : ActionStepBase
    {
        private readonly ActionStepBase _internalStep;
        private readonly ProcessTrace _processTrace;
        private readonly string _processTraceStateKey;

        public override string StepName => _internalStep.StepName;


        public NotificationableActionStep(ProcessTrace processTrace,
                                            string processTraceStateKey,
                                            ActionStepBase internalStep)
        {
            _processTrace = processTrace;
            _processTraceStateKey = processTraceStateKey;
            _internalStep = internalStep;
        }


        public override void Execute(ProcessStateBase processState)
        {
            if (!processState.IsRollbackExecuted)
            {

                _processTrace.StepStart(_internalStep.StepName);

                try
                {
                    _internalStep.Execute(processState);
                }
                catch (NotificationEngineException ex)
                {
                    _processTrace.StepError(ex.ErrorCode, ex.Message, ex.InstructionsMessage);

                }
                catch (Exception ex)
                {
                    _processTrace.StepError(_internalStep.StepName, ex.ToString(), "Error occurred during the process.");
                }

                _processTrace.StepEnd();
            }
        }
    }
}
