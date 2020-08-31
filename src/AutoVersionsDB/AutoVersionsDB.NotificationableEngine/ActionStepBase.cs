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

        public abstract void Execute(EngineContext processState);


        protected ActionStepBase()
        {
            InternalSteps = new List<ActionStepBase>();
        }


        internal void SetStepsExecuter(IStepsExecuter stepsExecuter)
        {
            _stepsExecuter = stepsExecuter;
        }


        protected void ExecuteInternalSteps(EngineContext processState, bool isContinueOnError)
        {
            _stepsExecuter.ExecuteSteps(InternalSteps, processState, isContinueOnError);
        }

    }

    public abstract class ActionStepBase<TEngineContext> : ActionStepBase
            where TEngineContext : EngineContext
    {


        public override void Execute(EngineContext engineContext)
        {
            Execute(engineContext as TEngineContext);
        }

        public abstract void Execute(TEngineContext engineContext);
    }


}
