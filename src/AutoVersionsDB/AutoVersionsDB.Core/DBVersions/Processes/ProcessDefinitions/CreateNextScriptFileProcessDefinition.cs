using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class CreateNextScriptFileProcessDefinition<TScriptFileType> : DBVersionsProcessDefinition
      where TScriptFileType : ScriptFileTypeBase
    {
        public override string EngineTypeName => "Create Next Script File";


        public CreateNextScriptFileProcessDefinition(ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                    SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                    ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                    ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                                    CreateScriptFilesStateStep createScriptFilesStateStep,
                                                    ValidationsStep<NextScriptFileNameValidationsFactory<TScriptFileType>> nextScriptFileNameValidations,
                                                    CreateNextScriptFileStep<TScriptFileType> createNextScriptFileStep)
            : base(null, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(nextScriptFileNameValidations);
            AddStep(createNextScriptFileStep);
        }



    }
}
