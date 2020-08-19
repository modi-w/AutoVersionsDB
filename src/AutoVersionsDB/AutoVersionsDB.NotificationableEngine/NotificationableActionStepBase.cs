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

        public List<NotificationableActionStepBase>  InternalSteps { get; }
        public bool IsContinueOnErrorInIternalStep { get; }

        //    public abstract int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState);

        public abstract void Execute(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState, Action<List<NotificationableActionStepBase>,bool> onExecuteStepsList);


        protected NotificationableActionStepBase()
        {
            InternalSteps = new List<NotificationableActionStepBase>();
        }
    }

    public abstract class NotificationableActionStepBase<TProcessState, TNotificationableEngineConfig> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
            where TNotificationableEngineConfig : NotificationableEngineConfig
    {

        //public override int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState)
        //{
        //    return GetNumOfInternalSteps(notificationableEngineConfig as TNotificationableEngineConfig, processState as TProcessState);
        //}
        //public abstract int GetNumOfInternalSteps(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState);


        public override void Execute(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            Execute(notificationableEngineConfig as TNotificationableEngineConfig, processState as TProcessState, onExecuteStepsList);
        }

        public abstract void Execute(TNotificationableEngineConfig notificationableEngineConfig, TProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList);
    }

 
}
