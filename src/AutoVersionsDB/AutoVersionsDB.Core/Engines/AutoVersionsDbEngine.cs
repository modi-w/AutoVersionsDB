using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;


namespace AutoVersionsDB.Core.Engines
{
    public abstract class AutoVersionsDbEngine : NotificationEngine<AutoVersionsDbProcessState, AutoVersionsDBExecutionParams, ProjectConfigItem>
    {

        private bool _isVirtualExecution;
        public bool IsVirtualExecution
        {
            get
            {
                return _isVirtualExecution;
            }
            protected set
            {
                _isVirtualExecution = value;

                EngineMetaData["IsVirtualExecution"] = _isVirtualExecution.ToString();
            }
        }

        public AutoVersionsDbEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                    NotificationableActionStepBase rollbackStep)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
            IsVirtualExecution = false;

        }



    }
}
