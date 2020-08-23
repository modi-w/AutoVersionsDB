using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    internal class NotificationableActionStep : ActionStepBase
    {
        private readonly ActionStepBase _internalStep;
        private readonly ProcessTraceStateChangeHandler _notifictionStateChangeHandler;
        private readonly string _processTraceStateKey;

        public override string StepName => _internalStep.StepName;


        public NotificationableActionStep(ProcessTraceStateChangeHandler notifictionStateChangeHandler,
                                            string processTraceStateKey,
                                            ActionStepBase internalStep)
        {
            _notifictionStateChangeHandler = notifictionStateChangeHandler;
            _processTraceStateKey = processTraceStateKey;
            _internalStep = internalStep;
        }


        public override void Execute(ProcessStateBase processState)
        {
            if (!processState.IsRollbackExecuted)
            {

                _notifictionStateChangeHandler.StepStart(_processTraceStateKey,_internalStep.StepName);

                try
                {
                    _internalStep.Execute(processState);
                }
                catch (NotificationEngineException ex)
                {
                    _notifictionStateChangeHandler.StepError(_processTraceStateKey, ex.ErrorCode, ex.Message, ex.InstructionsMessage);

                }
                catch (Exception ex)
                {
                    _notifictionStateChangeHandler.StepError(_processTraceStateKey, _internalStep.StepName, ex.ToString(), "Error occurred during the process.");
                }

                _notifictionStateChangeHandler.StepEnd(_processTraceStateKey);
            }
        }
    }
}
