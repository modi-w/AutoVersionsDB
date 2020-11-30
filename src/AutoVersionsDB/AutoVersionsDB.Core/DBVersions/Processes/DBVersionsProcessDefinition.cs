using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public abstract class DBVersionsProcessDefinition : ProcessDefinition
    {
        public bool IsVirtualExecution { get; protected set; }

        public DBVersionsProcessDefinition(ActionStepBase rollbackStep,
                                                ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep)
            : base(rollbackStep)
        {
            AddStep(idExistValidationStep);
            AddStep(setProjectConfigInProcessContextStep);
        }
    }
}
