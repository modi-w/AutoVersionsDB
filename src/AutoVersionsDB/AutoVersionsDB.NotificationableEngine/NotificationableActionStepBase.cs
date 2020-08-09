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

        public abstract bool HasInternalStep { get; }

        //public NotificationableActionStepBase InternalNotificationableAction { get; protected set; }
        //public bool HasInternalStep
        //{
        //    get
        //    {
        //        return InternalNotificationableAction != null;
        //    }
        //}


        public abstract int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState);

        public abstract void Execute(NotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, ProcessStateBase processState);

    }

    public abstract class NotificationableActionStepBase<TProcessState, TNotificationableEngineConfig> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
            where TNotificationableEngineConfig : NotificationableEngineConfig
    {
        //protected virtual NotificationableActionStepBase<TProcessState> _internalNotificationableAction { get; set; }
        //public override NotificationableActionStepBase InternalNotificationableAction => _internalNotificationableAction;

        //public override void Prepare(NotificationableEngineConfig notificationableEngineConfig)
        //{
        //    this.Prepare(notificationableEngineConfig as TNotificationableEngineConfig);
        //}
        //public abstract void Prepare(TNotificationableEngineConfig notificationableEngineConfig);


        public override int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        {
            return GetNumOfInternalSteps(notificationableEngineConfig as TNotificationableEngineConfig, processState as TProcessState);
        }
        public abstract int GetNumOfInternalSteps(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState);


        public override void Execute(NotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, ProcessStateBase processState)
        {
            Execute(notificationableEngineConfig as TNotificationableEngineConfig, notificationExecutersProvider, processState as TProcessState);
        }

        public abstract void Execute(TNotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, TProcessState processState);
    }

    //public abstract class NotificationableActionStepBase<TProcessState, TNotificationableEngineConfig, TActionStepArgs> : NotificationableActionStepBase<TProcessState, TNotificationableEngineConfig>
    //        where TProcessState : ProcessStateBase
    //        where TNotificationableEngineConfig : NotificationableEngineConfig
    //        where TActionStepArgs : ActionStepArgs
    //{
    //    //protected NotificationableActionStepBase<TProcessState, TActionStepArgs> _internalNotificationableActionWithArgs { get; set; }
    //    //protected override NotificationableActionStepBase<TProcessState> _internalNotificationableAction => _internalNotificationableActionWithArgs;
    //    //public override NotificationableActionStepBase InternalNotificationableAction => _internalNotificationableAction;

    //    public override int GetNumOfInternalSteps(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState, ActionStepArgs actionStepArgs)
    //    {
    //        return GetNumOfInternalSteps(notificationableEngineConfig as TNotificationableEngineConfig, processState as TProcessState, actionStepArgs as TActionStepArgs);
    //    }

    //    public abstract int GetNumOfInternalSteps(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState, TActionStepArgs actionStepArgs);


    //    public override void Execute(TNotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, TProcessState processState, ActionStepArgs actionStepArgs)
    //    {
    //        Execute(notificationableEngineConfig, notificationExecutersProvider, processState as TProcessState, actionStepArgs as TActionStepArgs);
    //    }

    //    public abstract void Execute(TNotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, TProcessState processState, TActionStepArgs actionStepArgs);
    //}
}
