using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public abstract class DBVersionsProcessDefinition : ProcessDefinition
    {
        public bool IsVirtualExecution { get; protected set; }

        public DBVersionsProcessDefinition(ActionStepBase rollbackStep,
                                                ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep)
            : base(rollbackStep)
        {
            AddStep(projectCodeExistValidationStep);
            AddStep(setProjectConfigInProcessContextStep);
        }
    }
}
