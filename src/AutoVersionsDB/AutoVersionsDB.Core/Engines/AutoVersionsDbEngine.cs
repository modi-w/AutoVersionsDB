using AutoVersionsDB.NotificationableEngine;


namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDbEngine : FluentNotificationEngineBase<AutoVersionsDbProcessState, AutoVersionsDBExecutionParams>
    {
        public AutoVersionsDbEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
            : base(notificationExecutersFactoryManager)
        {
        }

      
    }
}
