//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace AutoVersionsDB.NotificationableEngine
//{
//    public class StepsExecuter
//    {

//        private ProcessTrace _processTrace;

//        private string _processTraceStateKey;
//        private ActionStepBase _rollbackStep;


//        public StepsExecuter()
//        {
//        }


//        internal void SetProcessProperty(ProcessTrace processTrace, ActionStepBase rollbackStep)
//        {
//            _processTrace = processTrace;
//            _rollbackStep = rollbackStep;
//        }



//        private NotificationableActionStep DecorateStep(ActionStepBase step)
//        {
//            return new NotificationableActionStep(_processTrace, _processTraceStateKey, step);
//        }

//        private List<NotificationableActionStep> DecorateSteps(IEnumerable<ActionStepBase> steps)
//        {
//            return steps.Select(e => DecorateStep(e)).ToList();
//        }


//    }
//}
