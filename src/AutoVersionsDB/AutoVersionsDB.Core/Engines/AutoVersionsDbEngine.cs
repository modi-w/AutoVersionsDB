﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;


namespace AutoVersionsDB.Core.Engines
{
    public abstract class AutoVersionsDbEngine : NotificationEngine<AutoVersionsDbProcessState, AutoVersionsDBExecutionParams, ProjectConfigItem>
    {
        private bool _isVirtualExecution { get; set; }
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

        public AutoVersionsDbEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    NotificationableActionStepBase rollbackStep)
            : base(notificationExecutersFactoryManager, rollbackStep)
        {
        }


    }
}
