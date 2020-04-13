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

        public NotificationableActionStepBase InternalNotificationableAction { get; protected set; }

        public abstract int GetNumOfInternalSteps(ProcessStateBase processState, ActionStepArgs actionStepArgs);

        public abstract void Execute(ProcessStateBase processState, ActionStepArgs actionStepArgs);

    }

    public abstract class NotificationableActionStepBase<TProcessState> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
    {
        //protected virtual NotificationableActionStepBase<TProcessState> _internalNotificationableAction { get; set; }
        //public override NotificationableActionStepBase InternalNotificationableAction => _internalNotificationableAction;

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

    public abstract class NotificationableActionStepBase<TProcessState, TActionStepArgs> : NotificationableActionStepBase<TProcessState>
            where TProcessState : ProcessStateBase
            where TActionStepArgs : ActionStepArgs
    {
        //protected NotificationableActionStepBase<TProcessState, TActionStepArgs> _internalNotificationableActionWithArgs { get; set; }
        //protected override NotificationableActionStepBase<TProcessState> _internalNotificationableAction => _internalNotificationableActionWithArgs;
        //public override NotificationableActionStepBase InternalNotificationableAction => _internalNotificationableAction;

        public override int GetNumOfInternalSteps(TProcessState processState, ActionStepArgs actionStepArgs)
        {
            return GetNumOfInternalSteps(processState as TProcessState, actionStepArgs as TActionStepArgs);
        }

        public abstract int GetNumOfInternalSteps(TProcessState processState, TActionStepArgs actionStepArgs);


        public override void Execute(TProcessState processState, ActionStepArgs actionStepArgs)
        {
            Execute(processState as TProcessState, actionStepArgs as TActionStepArgs);
        }

        public abstract void Execute(TProcessState processState, TActionStepArgs actionStepArgs);
    }
}
