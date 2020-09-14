using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public abstract class DBVersionsProcessDefinition : ProcessDefinition
    {
        public bool IsVirtualExecution { get; protected set; }

        public DBVersionsProcessDefinition(ActionStepBase rollbackStep,
                                                ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep)
            : base(rollbackStep)
        {
            AddStep(projectCodeExistValidationStep);
            AddStep(setProjectConfigInProcessContextStep);
        }
    }
}
