using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class TargetStateScriptFileValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationProcessDefinition(ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                                ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(null, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(targetStateScriptFileValidationStep);
        }
    }
}
