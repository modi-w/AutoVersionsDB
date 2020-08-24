using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public class StepsExecuter
    {

        private ProcessTraceStateChangeHandler _notifictionStateChangeHandler;

        private string _processTraceStateKey;
        private ActionStepBase _rollbackStep;


        public StepsExecuter(ProcessTraceStateChangeHandler notifictionStateChangeHandler)
        {
            _notifictionStateChangeHandler = notifictionStateChangeHandler;
        }


        internal void SetProcessProperty(string processTraceStateKey, ActionStepBase rollbackStep)
        {
            _processTraceStateKey = processTraceStateKey;
            _rollbackStep = rollbackStep;
        }

        public void ExecuteSteps(IEnumerable<ActionStepBase> steps,
                                ProcessStateBase processState,
                                bool isContinueOnError)
        {
            foreach (var step in steps)
            {
                step.SetStepsExecuter(this);
            }

            List<NotificationableActionStep> wrappedSteps = DecorateSteps(steps);

            if (!processState.IsRollbackExecuted)
            {
                _notifictionStateChangeHandler.SetInternalSteps(_processTraceStateKey, wrappedSteps.Count);

                foreach (var step in wrappedSteps)
                {
                    step.Execute(processState);

                    if (_notifictionStateChangeHandler.HasError(_processTraceStateKey) && !isContinueOnError)
                    {
                        if (processState.CanRollback)
                        {
                            _notifictionStateChangeHandler.ClearAllInternalProcessState(_processTraceStateKey);

                            if (_rollbackStep != null)
                            {
                                NotificationableActionStep wrappedRollbackStep = DecorateStep(_rollbackStep);
                                wrappedRollbackStep.Execute(processState);
                            }
                        }

                        processState.IsRollbackExecuted = true;

                        break;
                    }
                }
            }


        }



        private NotificationableActionStep DecorateStep(ActionStepBase step)
        {
            return new NotificationableActionStep(_notifictionStateChangeHandler, _processTraceStateKey, step);
        }

        private List<NotificationableActionStep> DecorateSteps(IEnumerable<ActionStepBase> steps)
        {
            return steps.Select(e => DecorateStep(e)).ToList();
        }


    }
}
