using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class SyncDBProcessDefinition : DBVersionsProcessDefinition
    {
        public const string Name = "Sync DB";
        public override string EngineTypeName => Name;



        public SyncDBProcessDefinition(RestoreDatabaseStep rollbackStep,
                                        ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                        CreateBackupStep createBackupStep,
                                        ExecuteAllScriptsStep executeScriptsStep,
                                        FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(artifactFileValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
            AddStep(createBackupStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);
        }



    }
}
