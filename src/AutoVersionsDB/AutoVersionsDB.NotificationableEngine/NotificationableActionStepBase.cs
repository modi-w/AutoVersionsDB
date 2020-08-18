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


        public abstract int GetNumOfInternalSteps(NotificationableEngineConfig notificationableEngineConfig, ProcessStateBase processState);

        public abstract void Execute(NotificationableEngineConfig notificationableEngineConfig, NotificationExecutersProvider notificationExecutersProvider, ProcessStateBase processState);

    }

    public abstract class NotificationableActionStepBase<TProcessState, TNotificationableEngineConfig> : NotificationableActionStepBase
            where TProcessState : ProcessStateBase
            where TNotificationableEngineConfig : NotificationableEngineConfig
    {

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

 
}
