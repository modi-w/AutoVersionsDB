using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public class StepsExecuter
    {

        private ProcessTrace _processTrace;

        private string _processTraceStateKey;
        private ActionStepBase _rollbackStep;


        public StepsExecuter()
        {
        }


        internal void SetProcessProperty(ProcessTrace processTrace, ActionStepBase rollbackStep)
        {
            _processTrace = processTrace;
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
                _processTrace.SetInternalSteps(wrappedSteps.Count);

                foreach (var step in wrappedSteps)
                {
                    step.Execute(processState);

                    if (_processTrace.HasError && !isContinueOnError)
                    {
                        if (processState.CanRollback)
                        {
                            _processTrace.ClearAllInternalProcessState();

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
            return new NotificationableActionStep(_processTrace, _processTraceStateKey, step);
        }

        private List<NotificationableActionStep> DecorateSteps(IEnumerable<ActionStepBase> steps)
        {
            return steps.Select(e => DecorateStep(e)).ToList();
        }


    }
}
