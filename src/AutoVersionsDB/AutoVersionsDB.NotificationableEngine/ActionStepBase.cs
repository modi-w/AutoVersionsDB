using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ActionStepBase
    {
        public abstract string StepName { get; }

        public List<ActionStepBase> InternalSteps { get; }
        public bool IsContinueOnErrorInIternalStep { get; }

        private IStepsExecuter _stepsExecuter;

        public abstract void Execute(ProcessStateBase processState);


        protected ActionStepBase()
        {
            InternalSteps = new List<ActionStepBase>();
        }


        internal void SetStepsExecuter(IStepsExecuter stepsExecuter)
        {
            _stepsExecuter = stepsExecuter;
        }


        protected void ExecuteInternalSteps(ProcessStateBase processState, bool isContinueOnError)
        {
            _stepsExecuter.ExecuteSteps(InternalSteps, processState, isContinueOnError);
        }

    }

    public abstract class ActionStepBase<TProcessState> : ActionStepBase
            where TProcessState : ProcessStateBase
    {

        //public override int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        //{
        //    return GetNumOfInternalSteps(notificationableEngineConfig as TNotificationableEngineConfig, processState as TProcessState);
        //}
        //public abstract int GetNumOfInternalSteps(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState);


        public override void Execute(ProcessStateBase processState)
        {
            Execute(processState as TProcessState);
        }

        public abstract void Execute(TProcessState processState);
    }


}
