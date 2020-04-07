using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class NotificationableActionStepBase
    {
        public abstract string StepName { get; }

        public abstract int GetNumOfInternalSteps(ProcessStateBase processState, ActionStepArgs actionStepArgs);

        public abstract void Execute(ProcessStateBase processState, ActionStepArgs actionStepArgs);

    }

    public abstract class NotificationableActionStepBase<TProcessState> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
    {
        public override int GetNumOfInternalSteps(ProcessStateBase processState, ActionStepArgs actionStepArgs)
        {
            return GetNumOfInternalSteps(processState as TProcessState, actionStepArgs);
        }

        public abstract int GetNumOfInternalSteps(TProcessState processState, ActionStepArgs actionStepArgs);


        public override void Execute(ProcessStateBase processState, ActionStepArgs actionStepArgs)
        {
            Execute(processState as TProcessState, actionStepArgs);
        }

        public abstract void Execute(TProcessState processState, ActionStepArgs actionStepArgs);
    }

    public abstract class NotificationableActionStepBase<TProcessState, TActionStepArgs> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
            where TActionStepArgs : ActionStepArgs
    {
        public override int GetNumOfInternalSteps(ProcessStateBase processState, ActionStepArgs actionStepArgs)
        {
            return GetNumOfInternalSteps(processState as TProcessState, actionStepArgs as TActionStepArgs);
        }

        public abstract int GetNumOfInternalSteps(TProcessState processState, TActionStepArgs actionStepArgs);


        public override void Execute(ProcessStateBase processState, ActionStepArgs actionStepArgs)
        {
            Execute(processState as TProcessState, actionStepArgs as TActionStepArgs);
        }

        public abstract void Execute(TProcessState processState, TActionStepArgs actionStepArgs);
    }
}
